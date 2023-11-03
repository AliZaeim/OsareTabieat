using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Permissions;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using SmsIrRestfulNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly MyContext _context;
        public UserService(MyContext context)
        {
            _context = context;
        }
        #region Generel
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public bool SendVerificationCode(string code, string phoneNumber)
        {
            string token = new Token().GetToken("2027ea4381a2e4def2bf654", "@#rth@123456#");

            RestVerificationCode restVerificationCode = new()
            {
                Code = code,
                MobileNumber = phoneNumber
            };

            RestVerificationCodeRespone restVerificationCodeRespone = new VerificationCode().Send(token, restVerificationCode);
            if (restVerificationCode != null)
            {
                return restVerificationCodeRespone.IsSuccessful;
            }
            else
            {
                return false;
            }

        }
        public bool SendUserName_and_Password(string userName, string password, string phoneNumber)
        {
            string token = new Token().GetToken("2027ea4381a2e4def2bf654", "@#rth@123456#");

            UltraFastSend ultraFastSend = new()
            {
                Mobile = long.Parse(phoneNumber),
                TemplateId = 30030,
                ParameterArray = new List<UltraFastParameters>()
            {
                new UltraFastParameters()
                {
                    Parameter="username", ParameterValue=userName

                },
                new UltraFastParameters()
                {
                     Parameter = "password" , ParameterValue = password
                }
            }.ToArray()

            };

            UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

            return ultraFastSendRespone.IsSuccessful;
        }
        public bool SendPassword(string pass, string phoneNumber)
        {
            string token = new Token().GetToken("2027ea4381a2e4def2bf654", "@#rth@123456#");

            UltraFastSend ultraFastSend = new()
            {
                Mobile = long.Parse(phoneNumber),
                TemplateId = 22819,
                ParameterArray = new List<UltraFastParameters>()
            {
                new UltraFastParameters()
                {
                    Parameter = "password" , ParameterValue = pass
                }
            }.ToArray()

            };

            UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

            return ultraFastSendRespone.IsSuccessful;
        }
        public bool SendVerification(string code, string phoneNumber)
        {
            string token = new Token().GetToken("2027ea4381a2e4def2bf654", "@#rth@123456#");

            UltraFastSend ultraFastSend = new()
            {
                Mobile = long.Parse(phoneNumber),
                TemplateId = 46669,
                ParameterArray = new List<UltraFastParameters>()
            {
                new UltraFastParameters()
                {
                     Parameter= "RegisterCode" , ParameterValue = code
                }
            }.ToArray()

            };

            UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

            return ultraFastSendRespone.IsSuccessful;
        }
        #endregion General
        #region User
        public void CreateUser(User user)
        {
            _context.Users.Add(user);
        }

        public void DeleteUser(int id)
        {
            User? user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Remove(user);
            }
        }

        public async Task<bool> ExistUserByCellphoneAsync(string Cellphone)
        {
            return await _context.Users.Include(x => x.UserRoles).AnyAsync(x => x.Cellphone == Cellphone);
        }

        public async Task<bool> ExistUserbyCellphonePasswordAsync(string? Cellphone, string? Password)
        {
            return await _context.Users.Include(x => x.UserRoles).AnyAsync(predicate: x => x.Cellphone == Cellphone!.ToLower() && x.Password == Password);
        }

        public bool ExistUserById(int id)
        {
            return _context.Users.Include(x => x.UserRoles).Any(x => x.Id == id);
        }

        public async Task<bool> ExistUserByIdAsync(int id)
        {
            return await _context.Users.Include(x => x.UserRoles).AnyAsync(x => x.Id == id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.Include(x => x.UserRoles).ToListAsync();
        }

        public async Task<User> GetUserByCellhoneAsync(string? Cellphone)
        {
            return await _context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.Cellphone == Cellphone);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByuserNameAsync(string userName)
        {
            return await _context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.UserName == userName);
        }
        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }


        #endregion User
        #region UserRole
        public void CreateUserRole(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
        }

        public bool CheckPermissionByName(string PermissionName, string UserName)
        {
            try
            {
                User? user = _context.Users.SingleOrDefault(u => u.UserName == UserName && u.IsActive);
                //Permission permission = _context.Permissions.SingleOrDefault(x => x.)

                if (user == null)
                {
                    return false;
                }

                List<int> rolesOfuser = _context.UserRoles.Include(r => r.User).Include(r => r.Role)
                    .Where(w => w.User!.UserName == UserName && w.IsActive).Select(w => w.Role!.RoleId).ToList();

                if (!rolesOfuser.Any())
                {
                    return false;
                }
                List<RolePermission> rolePermissions = _context.RolePermissions.Include(x => x.Permission).Include(x => x.Role).ToList();                
                rolePermissions = rolePermissions.Where(w => w.Permission!.PermissionName == PermissionName).ToList();
                List<int> RolesofPermission = rolePermissions.Select(x => x.RoleId.GetValueOrDefault()).ToList();                    

                return RolesofPermission.Intersect(rolesOfuser).Any();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException!.Message;
                return false;
            }
        }
        #endregion

    }
}
