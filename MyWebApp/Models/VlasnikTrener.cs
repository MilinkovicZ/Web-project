using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebApp.Models
{
    public class VlasnikTrener
    {
        public string VlasnikUsername { get; set; }
        public string TrenerUsername { get; set; }
        public string Naziv { get; set; }
        public string Grad { get; set; }
        public string Ulica { get; set; }       
    }
}