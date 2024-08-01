using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Website.Models;
using Website.Models.ModelVM;

namespace Website.Controllers
{
    public class CoursesController : Controller
    {
        private readonly PortDbContext _context;
        private readonly IWebHostEnvironment env;

        public CoursesController(PortDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";

                return View(await _context.Course.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
           
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";

                if (id == null)
                {

                    return NotFound();
                }
                var course = await _context.Course
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (course == null)
                {
                    return NotFound();
                }
                return View(course);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseVM course)
        {
            if (ModelState.IsValid)
            {
                string filepath = "";
                if (course.Image != null)
                {
                    string uploadFolder = Path.Combine(env.WebRootPath, "Image/Courses/");
                    filepath = Guid.NewGuid().ToString() + "_" + course.Image.FileName;
                    string fullPath = Path.Combine(uploadFolder, filepath);
                    course.Image.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                Course data = new Course()
                {
                    Image = filepath,
                    Title = course.Title,
                    Description = course.Description,
                    Date = course.Date,
                    Price = course.Price,
                    Certificate = course.Certificate,
                    Name = course.Name
                };  
                _context.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";

                if (id == null)
                {
                    return NotFound();
                }
                var course = await _context.Course.FindAsync(id);
                string image = "/jpg";
                byte[] imagebyte = Convert.FromBase64String(image);

                using (MemoryStream stream = new MemoryStream(imagebyte))
                {
                    IFormFile file = new FormFile(stream, 0, imagebyte.Length, "photo", course.Image);

                    CourseVM data = new CourseVM()
                    {
                        Image = (FormFile)file,
                        Title = course.Title,
                        Description = course.Description,
                        Date = course.Date,
                        Price = course.Price,
                        Certificate=course.Certificate,
                        Name = course.Name
                    };

                    return View(data);
                }
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
            
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseVM course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _context.Course.FirstOrDefaultAsync(x => x.Id == id);
                    string filepath = "";
                    if (course.Image != null)
                    {
                        string uploadFolder = Path.Combine(env.WebRootPath, "Image/Courses/");
                        filepath = Guid.NewGuid().ToString() + "_" + course.Image.FileName;
                        string fullPath = Path.Combine(uploadFolder, filepath);
                        course.Image.CopyTo(new FileStream(fullPath, FileMode.Create));


                        data.Image = filepath;
                        data.Title = course.Title;
                        data.Description = course.Description;
                        data.Date = course.Date;
                        data.Name = course.Name;
                        data.Certificate = course.Certificate;
                        data.Price = course.Price;

                        _context.Update(data);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course != null)
            {
                _context.Course.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }
    }
}
