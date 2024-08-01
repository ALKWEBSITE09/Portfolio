using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Website.Controllers
{
    public class AdminController : Controller
    {
        private readonly PortDbContext dbContext;

        public AdminController(PortDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Admin admin)
        {
            var data = await dbContext.admin.FirstOrDefaultAsync(x => x.Username == admin.Username && x.Password == admin.Password);
            if (data != null)
            {
                HttpContext.Session.SetString("admin", data.Username);
                return RedirectToAction("Dashboard");
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                HttpContext.Session.Remove("admin");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";
                var skill = dbContext.skills.ToList();
                var service = dbContext.technology.ToList();
                var project = dbContext.projects.ToList();
                var course = dbContext.Course.ToList();
                var exp = dbContext.Experience.ToList();
                var cont = dbContext.Contact.ToList();

                TempData["skill"] = skill.Count();
                TempData["service"] = service.Count();
                TempData["project"] = project.Count();
                TempData["course"] = course.Count();
                TempData["exp"] = exp.Count();
                TempData["cont"] = cont.Count();

                return View();
            }
            else
            {
                return RedirectToAction("Index","Admin");
            }
        }
    }
}
