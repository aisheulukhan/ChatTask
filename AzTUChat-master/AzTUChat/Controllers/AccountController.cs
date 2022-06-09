using AzTUChat.Models;
using AzTUChat.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AzTUChat.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public AccountController(UserManager<AppUser> userManager,
                                   IWebHostEnvironment env)
        {
            _userManager = userManager;
            _env = env;
        } 
        public IActionResult Login()
        {
            return View();
        }
        //public async Task<IActionResult> Login()
        //{
        //}
        
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            AppUser user = new AppUser
            {
                FullName = register.FullName,
                UserName = register.UserName
            };
            await _userManager.CreateAsync(user, register.Password);
            string fileName = register.UserName+ register.Image.FileName;
            using (FileStream fs = new FileStream(Path.Combine(_env.WebRootPath,"img",fileName),FileMode.Create))
            {

            }
            UserImage ui = new UserImage
            {
                AppUser =  user,

            };   
            return View();
        }
    }
}
