using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebApp.Models
{
    public enum Pol
    {
        MUSKI,
        ZENSKI
    }

    public enum Uloga
    {
        POSETILAC,
        TRENER,
        VLASNIK
    }
    public class Korisnik
    {
        [NonSerialized]
        private DateTime datumRodjenja;
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public Pol PolKorisnika { get; set; }
        public string Email { get; set; }        
        public string DatumRodjenja { get => datumRodjenja.ToLocalTime().ToShortDateString(); set => datumRodjenja = DateTime.Parse(value); }
        public Uloga UlogaKorisnika { get; set; }
        public List<GrupniTrening> Treninzi;
        public List<FitnesCentar> FitnesCentri;
        public bool Blokiran;
        

        public Korisnik()
        {
            Treninzi = new List<GrupniTrening>();
            FitnesCentri = new List<FitnesCentar>();
            Blokiran = false;
        }

        public Korisnik(string korisnickoIme, string lozinka, string ime, string prezime, Pol polKorisnika, string email, DateTime datumRodjenja)
        {
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            PolKorisnika = polKorisnika;
            Email = email;
            this.datumRodjenja = datumRodjenja;
            UlogaKorisnika = Uloga.POSETILAC;
            Treninzi = new List<GrupniTrening>();
            FitnesCentri = new List<FitnesCentar>();
            Blokiran = false;
        }
    }
}