using DataLayer.Context;
using DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {
        #region General
        Task SaveChangesAsync();
        void SaveChanges();
        public bool SendVerificationCode(string code, string phoneNumber);
        public bool SendPassword(string pass, string phoneNumber);
        public bool SendVerification(string code, string phoneNumber);
        public bool SendUserName_and_Password(string userName, string password, string phoneNumber);
        bool CheckPermissionByName(string PermissionName, string UserName);
        #endregion
        #region Users
        Task<User> GetUserByCellhoneAsync(string? Cellphone);
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByuserNameAsync(string userName);
        Task<bool> ExistUserByCellphoneAsync(string Cellphone);
        Task<bool> ExistUserbyCellphonePasswordAsync(string? Cellphone, string? Password);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        Task<bool> ExistUserByIdAsync(int id);
        bool ExistUserById(int id);
        #endregion Users
        #region UserRole
        void CreateUserRole(UserRole userRole);
        #endregion

    }
}
