using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOZA_web.Models
{
    public class Map
    {
        public List<Location> Loc { get; set; }//IList?
        public double[] SafeArea = new double[2] {54.382842 , 18.600420};

    }
    public class Location
    {
        public string Caption { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
    }
}