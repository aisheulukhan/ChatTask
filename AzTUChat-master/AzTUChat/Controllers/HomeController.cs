using AzTUChat.DAL;
using AzTUChat.Models;
using AzTUChat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AzTUChat.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(AppDbContext context,
                              UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Users = _context.Users.Where(u => u.UserName != User.Identity.Name).Include(u => u.Image),
                CurrentUser =  _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name)
            };
            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
