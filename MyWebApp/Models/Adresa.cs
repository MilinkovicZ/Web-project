using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebApp.Models
{
    public class Adresa
    {
        public string Ulica { get; set; }
        public int Broj { get; set; }
        public string Grad { get; set; }
        public int PostanskiBroj { get; set; }

        public Adresa()
        {

        }

        public Adresa(string ulica, int broj, string grad, int postanskiBroj)
        {
            Ulica = ulica;
            Broj = broj;
            Grad = grad;
            PostanskiBroj = postanskiBroj;
        }
    }
}