using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOZA_web.Models;
using System.Data.Entity;
using System.Globalization;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace SOZA_web.Controllers
{
    [Authorize]
    public class MapController : Controller
    {
        public ActionResult LocationHistory()
        {
            var model = new Map
            {
                Loc = new List<Location>(),
                Date = new List<DateTime>()
            };
            string username = User.Identity.Name;
            var db = ApplicationDbContext.Create();

            ApplicationUser user = db.Users.FirstOrDefault(u => u.UserName.Equals(username));
            model.safearea = new SafeArea { SafeLatLng = new ApplicationUser.Location() };

            model.safearea.SafeLatLng = user.SafeLatLng;
            model.safearea.Radius = user.SafeAreaRadius;

            model.Loc.Add(new Location()
            {
                Latitude = 54.3715175,
                Longitude = 18.6126851,
                Timestamp = DateTime.Now.AddMinutes(-54)
            });
            model.Loc.Add(new Location()
        {
            Latitude = 54.3726143,
                Longitude = 18.613522,
                Timestamp = DateTime.Now.AddMinutes(-48)
            });
            model.Loc.Add(new Location()
        {
            Caption = "test3",
            Latitude = 54.3721956,
                Longitude = 18.6155014,
                Timestamp = DateTime.Now.AddMinutes(-41)
            });
            model.Loc.Add(new Location()
            {
                Latitude = 54.3730332,
                Longitude = 18.6160891,
                Timestamp = DateTime.Now.AddMinutes(-37)
            });
            model.Loc.Add(new Location()
            {
                Latitude = 54.3729893,
                Longitude = 18.6162471,
                Timestamp = DateTime.Now.AddMinutes(-32)
            });
            model.Loc.Add(new Location()
            {
                Latitude = 54.37258,
                Longitude = 18.6161827,
                Timestamp = DateTime.Now.AddMinutes(-26)
            });
            model.Loc.Add(new Location()
            {
                Latitude = 54.3722331,
                Longitude = 18.6166762,
                Timestamp = DateTime.Now.AddMinutes(-15)
            });
            model.Loc.Add(new Location()
            {
                Latitude = 54.3717394,
                Longitude = 18.6166601,
                Timestamp = DateTime.Now.AddMinutes(-7)
            });
            model.Loc.Add(new Location()
            {
                Latitude = 54.3712851,
                Longitude = 18.6166806,
                Timestamp = DateTime.Now
            });

            var GPSTraces = db.GPSTraces;

            //Load all GPSTraces to the view - you can filter them by day in this point 
            //(but you have to maintain View<->Model connection to update Map in Controller after selecting different day)
            //or just pass whole list to View and then filter them in View based on model.Date list
            foreach(var row in GPSTraces.ToList())
            {
                Location locationDb = new Location
                {
                    Latitude = row.Latitude,
                    Longitude = row.Longitude
                };
                model.Loc.Add(locationDb);
            }
          /*  //Date list used to create list of days with traces
            foreach(var row in GPSTraces.Select(d => d.Timestamp.Date).Distinct())
            {
                model.Date.Add(row);
            }*/
            return View(model);
        }

        public ActionResult SafeAreasPanel()
        {
            string username = User.Identity.Name;
            var db = ApplicationDbContext.Create();

            // Fetch the userprofile
            ApplicationUser user = db.Users.FirstOrDefault(u => u.UserName.Equals(username));
            var model = new SafeArea { SafeLatLng = new ApplicationUser.Location()};

            model.SafeLatLng = user.SafeLatLng;
            model.Radius = user.SafeAreaRadius;

            ViewBag.InputHint = "Wpisz i wyszukaj punkt celem wyznaczenia strefy bezpiecznej";
            ViewBag.InputHint2 = "... albo przeciągnij i upuść znacznik bezpośrednio na mapie.";
            ViewBag.SliderHint = "Przesuwaj pasek by zmieniać rozmiar strefy bezpiecznej.";
            return View(model);
        }
        [HttpPost]
        public ActionResult SafeAreasPanel(string SafeAreaLoc, int SafeAreaRadius)
        {
            //passing just model (without need of convertions to string and vice versa) was too big pain in the ass
            //hence code below
            string[] Coords = SafeAreaLoc.Split(',');
            if (Coords.Length < 2)
                return View();
            double Latitude, Longitude;
            double.TryParse(Coords[0], NumberStyles.Number, CultureInfo.InvariantCulture, out Latitude);
            double.TryParse(Coords[1], NumberStyles.Number, CultureInfo.InvariantCulture, out Longitude);
            string username = User.Identity.Name;
            var db = ApplicationDbContext.Create();

            // Fetch the userprofile
            ApplicationUser user = db.Users.FirstOrDefault(u => u.UserName.Equals(username));

           // user.SafeLatLng = SafeAreaLoc;
            user.SafeLatLng.lat = Latitude;
            user.SafeLatLng.lng = Longitude;
            user.SafeAreaRadius = SafeAreaRadius;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return View(SafeAreaLoc);
        }
    }
}