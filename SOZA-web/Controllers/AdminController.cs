using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SOZA_web.Models;

namespace SOZA_web.Controllers
{
    [Authorize(Roles = "AppAdmin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UsersIndex()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var admins = roleManager.FindByName("AppAdmin").Users.Select(u => u.UserId);
            var normalUsers = db.Users.Where(u => !admins.Contains(u.Id)).ToList();
            return View(normalUsers);
        }
        
        public ActionResult UsersDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }
        
        public ActionResult UsersEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UsersEdit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UsersIndex");
            }
            return View(applicationUser);
        }
        
        public ActionResult UsersDelete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }
        
        [HttpPost, ActionName("UsersDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult UsersDeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
            db.SaveChanges();
            return RedirectToAction("UsersIndex");
        }

        public ActionResult AndroidClientsIndex()
        {
            return View(db.AndroidClients.ToList());
        }

        public ActionResult AndroidClientsDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AndroidClient androidClient = db.AndroidClients.Find(id);
            if (androidClient == null)
            {
                return HttpNotFound();
            }
            return View(androidClient);
        }

        public ActionResult AndroidClientsCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AndroidClientsCreate([Bind(Include = "Id,Token,PhoneNumber")] AndroidClient androidClient)
        {
            if (String.IsNullOrWhiteSpace(androidClient.Token)
                || db.AndroidClients.Any(a => a.Token == androidClient.Token))
            {
                ModelState.AddModelError(string.Empty, "Token invalid or already exists.");
            }
            else if (ModelState.IsValid)
            {
                db.AndroidClients.Add(androidClient);
                db.SaveChanges();
                return RedirectToAction("AndroidClientsIndex");
            }

            return View(androidClient);
        }

        public ActionResult AndroidClientsEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AndroidClient androidClient = db.AndroidClients.Find(id);
            if (androidClient == null)
            {
                return HttpNotFound();
            }
            return View(androidClient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AndroidClientsEdit([Bind(Include = "Id,PhoneNumber")] AndroidClient androidClient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(androidClient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AndroidClientsIndex");
            }
            return View(androidClient);
        }

        public ActionResult AndroidClientsDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AndroidClient androidClient = db.AndroidClients.Find(id);
            if (androidClient == null)
            {
                return HttpNotFound();
            }
            return View(androidClient);
        }

        [HttpPost, ActionName("AndroidClientsDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult AndroidClientsDeleteConfirmed(int id)
        {
            AndroidClient androidClient = db.AndroidClients.Find(id);
            db.AndroidClients.Remove(androidClient);
            db.SaveChanges();
            return RedirectToAction("AndroidClientsIndex");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
