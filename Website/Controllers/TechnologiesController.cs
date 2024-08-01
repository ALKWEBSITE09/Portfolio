using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Website.Models;
using Website.Models.ModelVM;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Website.Controllers
{
    public class TechnologiesController : Controller
    {
        private readonly PortDbContext _context;
        private readonly IWebHostEnvironment env;

        public TechnologiesController(PortDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Technologies
        public async Task<IActionResult> Index()
        {

            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";

                return View(await _context.technology.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        // GET: Technologies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";

                if (id == null)
                {
                    return NotFound();
                }

                var technology = await _context.technology
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (technology == null)
                {
                    return NotFound();
                }

                return View(technology);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        // GET: Technologies/Create
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

        // POST: Technologies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TechnologyVM technology)
        {
            if (ModelState.IsValid)
            {

                string filepath = "";
                if (technology.image != null)
                {
                    string uploadFolder = Path.Combine(env.WebRootPath, "Image/Technologys/");
                    filepath = Guid.NewGuid().ToString() + "_" + technology.image.FileName;
                    string fullPath = Path.Combine(uploadFolder, filepath);
                    technology.image.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                Technology techno = new Technology()
                {
                    Name = technology.Name,
                    image = filepath,
                    Description = technology.Description
                };

                _context.Add(techno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(technology);
        }

        // GET: Technologies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";

                if (id == null)
                {
                    return NotFound();
                }

                var technology = await _context.technology.FindAsync(id);
                string image = "/jpg";
                byte[] imagebyte = Convert.FromBase64String(image);

                using (MemoryStream stream = new MemoryStream(imagebyte))
                {
                    IFormFile file = new FormFile(stream, 0, imagebyte.Length, "photo", technology.image);

                    TechnologyVM data = new TechnologyVM();
                    data.Name = technology.Name;
                    data.Description = technology.Description;
                    data.image = file;

                    return View(data);
                }
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
            
        }

        // POST: Technologies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TechnologyVM technology)
        {
            if (id != technology.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _context.technology.FirstOrDefaultAsync(x => x.Id == id);
                    string filepath = "";
                    if (technology.image != null)
                    {
                        string uploadFolder = Path.Combine(env.WebRootPath, "Image/Technologys/");
                        filepath = Guid.NewGuid().ToString() + "_" + technology.image.FileName;
                        string fullPath = Path.Combine(uploadFolder, filepath);
                        technology.image.CopyTo(new FileStream(fullPath, FileMode.Create));


                        data.image = filepath;
                        data.Name = technology.Name;
                        data.Description = technology.Description;

                        _context.Update(data);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechnologyExists(technology.Id))
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
            return View(technology);
        }

        // GET: Technologies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var technology = await _context.technology.FindAsync(id);
            if (technology != null)
            {
                _context.technology.Remove(technology);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }


        private bool TechnologyExists(int id)
        {
            return _context.technology.Any(e => e.Id == id);
        }
    }
}
