using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebApp.Models
{
    public class FitnesCentar
    {
        [NonSerialized]
        Random r = new Random();
        public string Naziv { get; set; }
        public Adresa AdresaFitnesCentra { get; set; }
        public int GodinaOtvaranja { get; set; }
        public string Vlasnik { get; set; }
        public int MesecnaCena { get; set; }
        public int GodisnjaCena { get; set; }
        public int CenaJednogTreninga { get; set; }
        public int CenaJednogTreningaGrupni { get; set; }
        public int CenaJednogTreningaPersonalni { get; set; }
        public bool Obrisan { get; set; }
        public int Id { get; set; }        

        public FitnesCentar()
        {
            Obrisan = false;
            Id = r.Next(100, 10000);
        }

        public FitnesCentar(string naziv, Adresa adresaFitnesCentra, int godinaOtvaranja, string vlasnik, int mesecnaCena, int godisnjaCena, int cenaJednogTreninga, int cenaJednogTreningaGrupni, int cenaJednogTreningaPersonalni)
        {
            Naziv = naziv;
            AdresaFitnesCentra = adresaFitnesCentra;
            GodinaOtvaranja = godinaOtvaranja;
            Vlasnik = vlasnik;
            MesecnaCena = mesecnaCena;
            GodisnjaCena = godisnjaCena;
            CenaJednogTreninga = cenaJednogTreninga;
            CenaJednogTreningaGrupni = cenaJednogTreningaGrupni;
            CenaJednogTreningaPersonalni = cenaJednogTreningaPersonalni;
            Obrisan = false;
            Id = r.Next(100, 10000);
        }
    }
}