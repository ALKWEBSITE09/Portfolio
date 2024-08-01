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
    public class ProjectsController : Controller
    {
        private readonly PortDbContext _context;
        private readonly IWebHostEnvironment env;

        public ProjectsController(PortDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";

                var portDbContext = _context.projects.Include(p => p.Techno);
                return View(await portDbContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";

                if (id == null)
                {
                    return NotFound();
                }

                var project = await _context.projects
                    .Include(p => p.Techno)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (project == null)
                {
                    return NotFound();
                }

                return View(project);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";

                ViewData["TechnoId"] = new SelectList(_context.technology, "Id", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectVM project)
        {
            if (ModelState.IsValid)
            {
                string filepath = "";
                if (project.Image != null)
                {
                    string uploadFolder = Path.Combine(env.WebRootPath, "Image/Projects/img/");
                    filepath = Guid.NewGuid().ToString() + "_" + project.Image.FileName;
                    string fullPath = Path.Combine(uploadFolder, filepath);
                    project.Image.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                Project pro = new Project
                {
                    Image = filepath,
                    videourl = project.videourl,
                    ProjectTitle = project.ProjectTitle,
                    ProjectDescription = project.ProjectDescription,
                    TechnoId = project.TechnoId,
                    Code = project.Code
                };

                _context.Add(pro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TechnoId"] = new SelectList(_context.technology, "Id", "Name", project.TechnoId);
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";

                if (id == null)
                {
                    return NotFound();
                }
                ViewData["TechnoId"] = new SelectList(_context.technology, "Id", "Name");

                var project = await _context.projects.FindAsync(id);
                string image = "/jpg";
                byte[] imagebyte = Convert.FromBase64String(image);

                using (MemoryStream stream = new MemoryStream(imagebyte))
                {
                    IFormFile file = new FormFile(stream, 0, imagebyte.Length, "photo", project.Image);

                    ProjectVM data = new ProjectVM()
                    {
                        Image = file,
                        videourl = project.videourl,
                        ProjectTitle = project.ProjectTitle,
                        ProjectDescription = project.ProjectDescription,
                        TechnoId = project.TechnoId,
                        Code = project.Code
                    };

                    return View(data);
                }
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectVM project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _context.projects.FirstOrDefaultAsync(x => x.Id == id);
                    string filepath = "";
                    if (project.Image != null)
                    {
                        string uploadFolder = Path.Combine(env.WebRootPath, "Image/Projects/img/");
                        filepath = Guid.NewGuid().ToString() + "_" + project.Image.FileName;
                        string fullPath = Path.Combine(uploadFolder, filepath);
                        project.Image.CopyTo(new FileStream(fullPath, FileMode.Create));

                        data.Image = filepath;
                        data.ProjectTitle = project.ProjectTitle;
                        data.ProjectDescription = project.ProjectDescription;
                        data.videourl = project.videourl;
                        data.TechnoId = project.TechnoId;
                        data.Code = project.Code;

                        _context.Update(data);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            ViewData["TechnoId"] = new SelectList(_context.technology, "Id", "Name", project.TechnoId);
            return View(project);
        }

        // GET: Projects/Delete/5
        
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _context.projects.FindAsync(id);
            if (project != null)
            {
                _context.projects.Remove(project);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.projects.Any(e => e.Id == id);
        }
    }
}
