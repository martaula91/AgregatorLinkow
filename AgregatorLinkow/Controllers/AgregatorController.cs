using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgregatorLinkow.Data;
using AgregatorLinkow.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AgregatorLinkow.Controllers
{
    public class AgregatorController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private readonly ApplicationDbContext db;

        public AgregatorController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var links = db.Link.ToList();
            return View(links);

        }

        public IActionResult MyProfile()
        {
            var links = (from u in db.User
                          join l in db.Link on u.Id equals l.UserId
                          where u.UserName == User.Identity.Name
                         select l).ToList();

            //var links = db.Link.ToList();
            return View(links);

        }


        [HttpGet]
        [Authorize]
        public IActionResult AddLink()
        {
            
            return View();
        }

       
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLink( [Bind("Id,Title,Url")] Link link)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    link.AddingDate = DateTime.Now;
                    link.UserId = User.Identity.GetUserId();
                    db.Add(link);
                    await db.SaveChangesAsync();
                    //return RedirectToAction("Success");
                    return View("Success");
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return RedirectToAction(nameof(Index));
        }






        public IActionResult Success()
        {
            return View();
        }

        public async Task<IActionResult> AddPlus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await db.Link.FindAsync(id);
            if (link == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                link.TotalityOfPluses += 1;
                db.Update(link);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}