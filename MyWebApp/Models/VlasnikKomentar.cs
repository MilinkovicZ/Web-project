using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebApp.Models
{
    public class VlasnikKomentar
    {
        public int IdKomentar { get; set; }
        public string IdVlasnik { get; set; }
        public bool Odobren { get; set; }
    }
}