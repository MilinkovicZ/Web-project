using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebApp.Models
{
    public class Komentar
    {
        [NonSerialized]
        private static int brojac = 10;
        public Korisnik Korisnik { get; set; }
        public FitnesCentar FitnesCentar { get; set; }
        public string TekstKomentara { get; set; }
        public int Ocena { get; set; }
        public bool Odobren { get; set; }
        public int Id { get; set; }

        public Komentar()
        {
            Odobren = false;
            Id = brojac++;
        }

        public Komentar(Korisnik korisnik, FitnesCentar fitnesCentar, string tekstKomentara, int ocena)
        {
            Korisnik = korisnik;
            FitnesCentar = fitnesCentar;
            TekstKomentara = tekstKomentara;
            Ocena = ocena;
            Odobren = false;
            Id = brojac++;
        }
    }
}