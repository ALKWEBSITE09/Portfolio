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
    public class SkillsController : Controller
    {
        private readonly PortDbContext _context;
        private readonly IWebHostEnvironment env;

        public SkillsController(PortDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Skills
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";

                return View(await _context.skills.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        // GET: Skills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";

                if (id == null)
                {
                    return NotFound();
                }

                var skill = await _context.skills
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (skill == null)
                {
                    return NotFound();
                }

                return View(skill);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        // GET: Skills/Create
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

        // POST: Skills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SkillVM skill)
        {
            if (ModelState.IsValid)
            {
                string filepath = "";
                if (skill.Image != null)
                {
                    string uploadFolder = Path.Combine(env.WebRootPath, "Image/Skills/");
                    filepath = Guid.NewGuid().ToString() + "_" + skill.Image.FileName;
                    string fullPath = Path.Combine(uploadFolder, filepath);
                    skill.Image.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                Skill data = new Skill()
                {
                    Image = filepath,
                    Name = skill.Name,
                };
                _context.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skill);
        }

        // GET: Skills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";

                if (id == null)
                {
                    return NotFound();
                }

                var skill = await _context.skills.FindAsync(id);
                string image = "/jpg";
                byte[] imagebyte = Convert.FromBase64String(image);

                using (MemoryStream stream = new MemoryStream(imagebyte))
                {
                    IFormFile file = new FormFile(stream, 0, imagebyte.Length, "photo", skill.Image);

                    SkillVM data = new SkillVM()
                    {
                        Image = file,
                        Name = skill.Name
                    };

                    return View(data);
                }
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
                
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,SkillVM skill)
        {
            if (id != skill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _context.skills.FirstOrDefaultAsync(x => x.Id == id);
                    string filepath = "";
                    if (skill.Image != null)
                    {
                        string uploadFolder = Path.Combine(env.WebRootPath, "Image/Skills/");
                        filepath = Guid.NewGuid().ToString() + "_" + skill.Image.FileName;
                        string fullPath = Path.Combine(uploadFolder, filepath);
                        skill.Image.CopyTo(new FileStream(fullPath, FileMode.Create));


                        data.Image = filepath;
                        data.Name = skill.Name;

                        _context.Update(data);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillExists(skill.Id))
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
            return View(skill);
        }

        // GET: Skills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.skills.FindAsync(id);
            if (skill != null)
            {
                _context.skills.Remove(skill);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        

        private bool SkillExists(int id)
        {
            return _context.skills.Any(e => e.Id == id);
        }
    }
}
