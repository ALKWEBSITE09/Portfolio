using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Website.Models;
using Website.Models.ModelVM;

namespace Website.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly PortDbContext _context;
        private readonly IWebHostEnvironment env;

        public ProfilesController(PortDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Profiles
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                return View(await _context.profile.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        // GET: Profiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";

                if (id == null)
                {
                    return NotFound();
                }

                var profile = await _context.profile
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (profile == null)
                {
                    return NotFound();
                }

                return View(profile);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        // GET: Profiles/Create
        

        // GET: Profiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";

                if (id == null)
                {
                    return NotFound();
                }

                var profile = await _context.profile.FindAsync(id);
                string image = "/jpg";
                byte[] imagebyte = Convert.FromBase64String(image);

                using (MemoryStream stream = new MemoryStream(imagebyte))
                {
                    IFormFile file = new FormFile(stream, 0, imagebyte.Length, "photo", profile.Photo);
                    IFormFile file2 = new FormFile(stream, 0, imagebyte.Length, "photo", profile.aboutphoto);
                    ProfileVM vm = new ProfileVM()
                    {
                        Name = profile.Name,
                        Photo = file,
                        aboutphoto = file2,
                        Address = profile.Address,
                        Phone = profile.Phone,
                        MainDomain = profile.MainDomain,
                        Country = profile.Country,
                        state = profile.state,
                        Dob = profile.Dob,
                        district = profile.district,
                        Email = profile.Email,
                        ProfileDesc = profile.ProfileDesc
                        
                    };

                    return View(vm);
                }
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
            
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProfileVM profile)
        {
            if (id != profile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _context.profile.FirstOrDefaultAsync(x => x.Id == id);

                    string filepath = "";
                    string filepath2 = "";
                   
                    if (profile.Photo != null)
                    {
                        string uploadFolder = Path.Combine(env.WebRootPath, "Image/Profile/");
                        filepath = Guid.NewGuid().ToString() + "_" + profile.Photo.FileName;
                        string fullPath = Path.Combine(uploadFolder, filepath);
                        profile.Photo.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }
                    if (profile.aboutphoto != null)
                    {
                        string uploadFolder2 = Path.Combine(env.WebRootPath, "Image/Profile/");
                        filepath2 = Guid.NewGuid().ToString() + "_" + profile.aboutphoto.FileName;
                        string fullPath2 = Path.Combine(uploadFolder2, filepath2);
                        profile.aboutphoto.CopyTo(new FileStream(fullPath2, FileMode.Create));
                    }
                    if (profile.Photo == profile.Photo & profile.aboutphoto == profile.aboutphoto)
                    {
                        data.Address = profile.Address;
                        data.Email = profile.Email;
                        data.ProfileDesc = profile.ProfileDesc;
                        data.Phone = profile.Phone;
                        data.state = profile.state;
                        data.district = profile.district;
                        data.Country = profile.Country;
                        data.MainDomain = profile.MainDomain;
                        data.Dob = profile.Dob;
                        data.Photo = profile.Photo.FileName;
                        data.aboutphoto = profile.aboutphoto.FileName;
                        data.Name = profile.Name;

                        _context.Update(data);
                        await _context.SaveChangesAsync();

                    }
                        data.Address = profile.Address;
                        data.Email = profile.Email;
                        data.ProfileDesc = profile.ProfileDesc;
                        data.Phone = profile.Phone;
                        data.state = profile.state;
                        data.district = profile.district;
                        data.Country = profile.Country;
                        data.MainDomain = profile.MainDomain;
                        data.Dob = profile.Dob;
                        data.Photo = filepath;
                        data.aboutphoto = filepath2;
                        data.Name = profile.Name;

                        _context.Update(data);
                        await _context.SaveChangesAsync();
                    
                    
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details",new {id = 1});
            }
            return View(profile);
        }

        

        private bool ProfileExists(int id)
        {
            return _context.profile.Any(e => e.Id == id);
        }
    }
}
