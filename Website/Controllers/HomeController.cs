using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Website.Models;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PortDbContext db;

        public HomeController(ILogger<HomeController> logger,PortDbContext db)
        {
            _logger = logger;
            this.db = db;
        }
        

        public IActionResult Index()
        {
            
            var skill = db.skills.ToList();
            var project = db.projects.ToList();
            var course = db.Course.ToList();
            var service = db.technology.ToList();
            var resume = db.Resume.FirstOrDefault(x => x.Id==1);

            var cours1 = db.Course.FirstOrDefault(x => x.Id==1);
            var cours2= db.Course.FirstOrDefault(x => x.Id == 2);
            var cours3 = db.Course.FirstOrDefault(x => x.Id == 3);

            TempData["cimage1"] = cours1.Image;
            TempData["cimage2"] = cours2.Image;
            TempData["cimage3"] = cours3.Image;

            var pro1 = db.projects.FirstOrDefault(x => x.Id == 1);
            var pro2 = db.projects.FirstOrDefault(x => x.Id == 2);
            var pro3 = db.projects.FirstOrDefault(x => x.Id == 3);

            TempData["pro1"] = pro1.Image;
            TempData["pro2"] = pro2.Image;
            TempData["pro3"] = pro3.Image;



            TempData["Resume"] = resume.url;

            TempData["about"] = project.Count();
            TempData["skill"] = skill.Count();
            TempData["project"] = project.Count();
            TempData["course"] = course.Count();
            TempData["services"] = service.Count();

            var profile = db.profile.FirstOrDefault(x => x.Id == 1);
            TempData["phone2"] = profile.Phone;
            TempData["email2"] = profile.Email;
            TempData["add2"] = profile.Address;
            TempData["desc2"] = profile.ProfileDesc;

            TempData["add"] = profile.Address;
            TempData["phone"] = profile.Phone;
            TempData["photo1"] = profile.aboutphoto;
            TempData["email"] = profile.Email;
            TempData["zip"] = profile.zipcode;
            TempData["desc"] = profile.ProfileDesc;
            TempData["proName"] = profile.Name;
            TempData["service"] = profile.MainDomain;
            TempData["logo"] = profile.LogoName;
            TempData["country"] = profile.Country;
            TempData["photo"] = profile.Photo;

            TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
