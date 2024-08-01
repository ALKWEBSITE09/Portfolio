using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Website.Models;

namespace Website.Controllers
{
    public class ContactsController : Controller
    {
        private readonly PortDbContext _context;

        public ContactsController(PortDbContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";

                return View(await _context.Contact.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";

                if (id == null)
                {
                    return NotFound();
                }

                var contact = await _context.Contact
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (contact == null)
                {
                    return NotFound();
                }

                return View(contact);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            var data = _context.profile.FirstOrDefault(x => x.Id == 1);
            ViewBag.add = data.Address;
            ViewBag.phone = data.Phone;
            ViewBag.email = data.Email;
            ViewBag.photo = data.aboutphoto;
            TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";
            TempData["phone2"] = data.Phone;
            TempData["email2"] = data.Email;
            TempData["add2"] = data.Address;
            TempData["desc2"] = data.ProfileDesc;

            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    SendEmail(contact.Name,contact.Email, contact.Subject, contact.Description);
                    _context.Add(contact);
                    await _context.SaveChangesAsync();
                    TempData["cont"] = "Message Send Successfully....!";
					RedirectToAction("Creates");
				}
                catch (Exception error)
                {
                    TempData["errors"] = error;
                }
            }
			var data = _context.profile.FirstOrDefault(x => x.Id == 1);
			ViewBag.add = data.Address;
			ViewBag.phone = data.Phone;
			ViewBag.email = data.Email;
			ViewBag.photo = data.aboutphoto;
			return View();
        }

        // GET: Contacts/Create
        public IActionResult Creates()
        {
            var profile = _context.profile.FirstOrDefault(x => x.Id == 1);
            TempData["add"] = profile.Address;
            TempData["phone"] = profile.Phone;
            TempData["photo1"] = profile.aboutphoto;
            TempData["email"] = profile.Email;

            TempData["yt"] = "https://youtube.com/@alpeshkhandekar?si=KdTN_xhyMjWj6Ovh";
            TempData["phone2"] = profile.Phone;
            TempData["email2"] = profile.Email;
            TempData["add2"] = profile.Address;

            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Creates(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    SendEmail(contact.Name,contact.Email, contact.Subject, contact.Description);
                    _context.Add(contact);
                    await _context.SaveChangesAsync();
                    TempData["cont"] = "Message Send Successfully....!";
                    return RedirectToAction("Create");
                }
                catch (Exception error)
                {
                    TempData["errors"] = error;
                }
            }
			var profile = _context.profile.FirstOrDefault(x => x.Id == 1);
			TempData["add"] = profile.Address;
			TempData["phone"] = profile.Phone;
			TempData["photo1"] = profile.aboutphoto;
			TempData["email"] = profile.Email;

			return View();
        }

        public void SendEmail(string Name, string toEmail, string subject, string body)
        {
            var fromAddress = new MailAddress("alk.website.09@gmail.com", "ALKWEBSITE");
            var toAddress = new MailAddress(toEmail);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "trhxyyqegwatqwnn")
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = "Thank you" +" " + Name +" "+ "for contacting me. I have received your message. You will get reply soon." + Environment.NewLine +"your message:"+ Environment.NewLine + body

            })
            {
                smtp.Send(message);
            }

        }
        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.FindAsync(id);
            if (contact != null)
            {
                _context.Contact.Remove(contact);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.Id == id);
        }
    }
}
