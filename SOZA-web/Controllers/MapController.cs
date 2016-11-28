using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOZA_web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace SOZA_web.Controllers
{
    [Authorize]
    public class MapController : Controller
    {
        public ActionResult LocationHistory()
        {

            //Set loc in Map model through GPS Trace DB
            var model = new Map
            {
                Loc = new List<Location>()
            };

            Location location = new Location
            {
                Caption = "test",
                Latitude = 54.382842,
                Longitude = 18.600420
            };
            model.Loc.Add(location);
            Location location2 = new Location
            {
                Caption = "test2",
                Latitude = 54.382797,
                Longitude = 18.598535
            };
            model.Loc.Add(location2);
            Location location3 = new Location
            {
                Caption = "test3",
                Latitude = 54.383428,
                Longitude = 18.597044
            };
            model.Loc.Add(location3);

          //  ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var db = ApplicationDbContext.Create();
            var GPSTraces = db.GPSTraces;

            foreach(var row in GPSTraces.ToList())
            {
                Location locationDb = new Location
                {
                    Latitude = row.Latitude,
                    Longitude = row.Longitude
                };
                model.Loc.Add(locationDb);
            }

            return View(model);
        }

        public ActionResult SafeAreasPanel()
        {
            return View();
        }
    }
}