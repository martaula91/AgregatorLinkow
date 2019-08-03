using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgregatorLinkow.Data;
using AgregatorLinkow.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
            //var AllLinks = db.Link.ToList();
            var AllLinks = (from p in db.Link
                            orderby p.TotalityOfPluses descending
                            select p).ToList();
            
            var links = new List<LinkExtendedModel>();
            var userId = User.Identity.GetUserId();

           
            foreach (var g in AllLinks)
            {
                var linkToAdd = new LinkExtendedModel()
                {
                    Id = g.Id,
                    UserId = g.UserId,
                    TotalityOfPluses = g.TotalityOfPluses,
                    Title = g.Title,
                    Description = g.Description,
                    Url = g.Url,
                    AddingDate = g.AddingDate,
                    CanAddPlus = User.Identity.IsAuthenticated && g.UserId != userId, //!(userLikes.Any(x => x.Link.LinkId == displayLink.Id
                    Finish = false // g.AddingDate >= DateTime.Now.Date.AddDays(-5) //false //
                };
            links.Add(linkToAdd);
             }
            
            var model = new LinkViewModel();
             model.Links = links;
            
            return View("Index", model);

        }

        public IActionResult MyProfile()
        {
            var links = (
                         //from u in db.User
                         //join l in db.Link on u.Id equals l.UserId
                         from l in db.Link
                         where l.UserId == User.Identity.GetUserId()
                        //link.UserId = User.Identity.GetUserId();
                        select l).ToList();

            //var links = db.Link.ToList();
            return View("MyProfile", links); //return View("Index", links);

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