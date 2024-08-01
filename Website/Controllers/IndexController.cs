using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Website.Models;

namespace Website.Controllers
{
    public class IndexController : Controller
    {
        private readonly PortDbContext db;

        public IndexController(PortDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Skill()
        {
            var profile = db.profile.FirstOrDefault(x => x.Id == 1);
            TempData["phone2"] = profile.Phone;
            TempData["email2"] = profile.Email;
            TempData["add2"] = profile.Address;
            TempData["desc2"] = profile.ProfileDesc;

            TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";
            var data = await db.skills.ToListAsync();
            return View(data);
        }
        public async Task<IActionResult> Course(string coursesearch)
        {
            if (coursesearch != null)
            {
                var profile = db.profile.FirstOrDefault(x => x.Id == 1);
                TempData["phone2"] = profile.Phone;
                TempData["email2"] = profile.Email;
                TempData["add2"] = profile.Address;
                TempData["desc2"] = profile.ProfileDesc;
                TempData["cvalue"] = coursesearch;
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";
                var data = await db.Course.Where(x => x.Name.Contains(coursesearch) || x.Title.Contains(coursesearch) ).ToListAsync();
                return View(data);

            }
            else
            {
                var profile = db.profile.FirstOrDefault(x => x.Id == 1);
                TempData["phone2"] = profile.Phone;
                TempData["email2"] = profile.Email;
                TempData["add2"] = profile.Address;
                TempData["desc2"] = profile.ProfileDesc;

                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";
                var data = await db.Course.ToListAsync();
                return View(data);
            }
        }
        public async Task<IActionResult> Service()
        {
            var profile = db.profile.FirstOrDefault(x => x.Id == 1);
            TempData["phone2"] = profile.Phone;
            TempData["email2"] = profile.Email;
            TempData["add2"] = profile.Address;
            TempData["desc2"] = profile.ProfileDesc;

            TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";
            var data = await db.technology.ToListAsync();
            return View(data);
        }
        public async Task<IActionResult> ProjectVideo(int id)
        {
            var profile = db.profile.FirstOrDefault(x => x.Id == 1);
            TempData["phone2"] = profile.Phone;
            TempData["email2"] = profile.Email;
            TempData["add2"] = profile.Address;
            TempData["desc2"] = profile.ProfileDesc;

            TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";
            var data = await db.projects.Include(x => x.Techno).FirstOrDefaultAsync(x => x.Id == id);
            return View(data);
        }
        public async Task<IActionResult> Project(string projectsearch)
        {
            if (projectsearch != null)
            {
                var profile = db.profile.FirstOrDefault(x => x.Id == 1);
                TempData["phone2"] = profile.Phone;
                TempData["email2"] = profile.Email;
                TempData["add2"] = profile.Address;
                TempData["desc2"] = profile.ProfileDesc;

                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";
                var data = await db.projects.Include(x => x.Techno).Where(x => x.ProjectTitle.Contains(projectsearch) || x.Techno.Name.Contains(projectsearch)).ToListAsync();
                TempData["value"] = projectsearch;
                return View(data);
            }
            else
            {
                var profile = db.profile.FirstOrDefault(x => x.Id == 1);
                TempData["phone2"] = profile.Phone;
                TempData["email2"] = profile.Email;
                TempData["add2"] = profile.Address;
                TempData["desc2"] = profile.ProfileDesc;

                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";
                var data = await db.projects.Include(x => x.Techno).ToListAsync();
                return View(data);
            }
        }
        public async Task<IActionResult> About()
        {
            var proj = db.projects.ToList();
            TempData["about"] = proj.Count();

            var resume = db.Resume.FirstOrDefault(x => x.Id == 1);

            TempData["Resume"] = resume.url;
            var profile = db.profile.FirstOrDefault(x => x.Id == 1);

            TempData["a_add"] = profile.Address;
            TempData["a_zip"] = profile.zipcode;
            TempData["a_phone"] = profile.Phone;
            TempData["a_desc"] = profile.ProfileDesc;
            TempData["a_email"] = profile.Email;
            TempData["a_photo1"] = profile.aboutphoto;

            TempData["phone2"] = profile.Phone;
            TempData["email2"] = profile.Email;
            TempData["add2"] = profile.Address;
            TempData["desc2"] = profile.ProfileDesc;

            TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";
            return View(profile);
        }
        public async Task<IActionResult> Resume()
        {
            var profile = db.profile.FirstOrDefault(x => x.Id == 1);
            TempData["phone2"] = profile.Phone;
            TempData["email2"] = profile.Email;
            TempData["add2"] = profile.Address;
            TempData["desc2"] = profile.ProfileDesc;

            TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";
            var data = await db.Experience.ToListAsync();
            return View(data);
        }
        public async Task<IActionResult> CourseDetail(int? id)
        {
            var profile = db.profile.FirstOrDefault(x => x.Id == 1);
            TempData["phone2"] = profile.Phone;
            TempData["email2"] = profile.Email;
            TempData["add2"] = profile.Address;
            TempData["desc2"] = profile.ProfileDesc;

            TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";
            if (id == null)
            {
                return NotFound();
            }
            var course = await db.Course.FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);

        }
    }
}
