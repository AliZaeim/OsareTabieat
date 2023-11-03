using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Core.Services.Interfaces;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;
        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }
        public class RegisterVM
        {
            [Display(Name = "نام")]
            [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]            
            public string User_Name { get; set; }
            [Display(Name = "نام خانوادگی")]
            [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]           
            public string User_Family { get; set; }
            [Display(Name = "تلفن همراه")]
            [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]            
            public string User_Cellphone { get; set; }
            
            public string RetUrl { get; set; }
            public bool SaveIsSuccess { get; set; }

        }
        [BindProperty]
        public RegisterVM Registervm { get; set; } = new();
        public void OnGet(string ReturnUrl)
        {
            if(!string.IsNullOrEmpty(ReturnUrl))
            {
                Registervm.RetUrl = ReturnUrl;
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            if(await _userService.ExistUserByCellphoneAsync(Registervm.User_Cellphone))
            {
                ModelState.AddModelError("registerVM.User_Cellphone", "شماره همراه قبلا ثبت شده است !");
                return Page();
            }
            string pass = Core.Prodocers.Generators.GenerateUniqueString(3, 0, 2, 2);
            
            
            UserRole userRole = new()
            {
                User = new()
                {
                    Name = Registervm.User_Name,
                    Family = Registervm.User_Family,
                    Cellphone = Registervm.User_Cellphone,
                    RegDate = DateTime.Now,
                    IsActive = true,
                    Password = pass
                },
                RoleId = 2
            };
            _userService.CreateUserRole(userRole);
            await _userService.SaveChangesAsync();
            TempData["Success"] = true;
            Registervm.SaveIsSuccess = true;
            Registervm.User_Name = "";
            Registervm.User_Family = "";
            Registervm.User_Cellphone = "";
            ModelState.Clear();
            return Page();
        }
    }
}
