using AzTUChat.DAL;
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
        private readonly AppDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
                                   IWebHostEnvironment env,
                                   AppDbContext context,
                                   SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _env = env;
            _context = context;
            _signInManager = signInManager;
        } 
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            AppUser user = _context.Users.SingleOrDefault(u => u.UserName == login.UserName);
           var result= await _signInManager.PasswordSignInAsync(user, login.Password, true, true);
            if (!result.Succeeded)
            {
                return View(login);
            }
            return RedirectToAction("Index", "Home");
        }

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
                register.Image.CopyTo(fs);
            }
            UserImage ui = new UserImage
            {
                AppUser = user,
                ImageUrl = fileName

            };
            await _context.UserImages.AddAsync(ui);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
