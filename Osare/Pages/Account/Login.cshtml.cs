using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Services.Interfaces;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }
        public class LoginVM
        {
            public LoginVM()
            {

            }
            [Display(Name = "تلفن همراه")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
            public string? Cellphone { get; set; }
            [Display(Name = "رمز عبور")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            public string? Password { get; set; }
            [Display(Name = "مرا به خاطر بسپار")]
            public bool RememberMe { get; set; }

            public string? RetUrl { get; set; }
        }
        [BindProperty]
        public LoginVM Loginvm { get; set; } = new();
        [BindProperty]
        public bool Logout { get; set; }
        public void OnGet(string ReturnUrl)
        {
            if (!string.IsNullOrEmpty(ReturnUrl))
            {
                Loginvm.RetUrl = ReturnUrl;
            }           


        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (Logout == true)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToPage("/Index");
            }
            var er = ModelState.Values.SelectMany(x => x.Errors).ToList();

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!await _userService.ExistUserbyCellphonePasswordAsync(Loginvm.Cellphone, Loginvm.Password))
            {
                ModelState.AddModelError("Loginvm.Cellphone", "تلفن همراه یا رمز ورود اشتباه است !");
                return Page();
            }
            User user = await _userService.GetUserByCellhoneAsync(Loginvm.Cellphone);

            if (user.IsActive)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim("fullname", user.FullName!)

                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = Loginvm.RememberMe
                };
                await HttpContext.SignInAsync(principal, properties);
                if (!string.IsNullOrEmpty(Loginvm.RetUrl))
                {
                    return Redirect("/" + Loginvm.RetUrl);
                }
                else
                {
                    return Redirect("/UsersPanel/Home/Index");
                }
                

            }
            else
            {
                ModelState.AddModelError("Loginvm.Cellphone", "کاربری شما در سایت غیرفعال است، به مدیر سایت اطلاع دهید !");
                return Page();
            }
        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }

    }
}
