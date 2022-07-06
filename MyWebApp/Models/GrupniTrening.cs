using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MyWebApp.Models
{
    public enum Trening
    {
        YOGA,
        LES_MILLS_TONE,
        BODY_PUMP
    }
    public class GrupniTrening
    {
        [NonSerialized]
        private DateTime vremeTreninga;
        [NonSerialized]
        Random r = new Random();
        public string Naziv { get; set; }
        public Trening TipTreninga { get; set; }
        public FitnesCentar FitnesCentarOdrzavanja { get; set; }
        public int TrajanjeTreninga { get; set; }
        public string VremeTreninga { get => vremeTreninga.ToLocalTime().ToShortDateString() + " " + vremeTreninga.ToLocalTime().ToShortTimeString(); set => vremeTreninga = DateTime.Parse(value); }
        public int MaksimalanBrojPosetilaca { get; set; }
        public List<string> SpisakPosetilaca { get; set; }  
        public bool Obrisan { get; set; }
        public int Id { get; set; }

        public GrupniTrening()
        {
            SpisakPosetilaca = new List<string>();
            Obrisan = false;            
            Id = r.Next(100, 10000);
        }

        public GrupniTrening(string naziv, Trening tipTreninga, FitnesCentar fitnesCentarOdrzavanja, int trajanjeTreninga, DateTime vremeTreninga, int maksimalanBrojPosetilaca)
        {
            Naziv = naziv;
            TipTreninga = tipTreninga;
            FitnesCentarOdrzavanja = fitnesCentarOdrzavanja;
            TrajanjeTreninga = trajanjeTreninga;
            this.vremeTreninga = vremeTreninga;            
            MaksimalanBrojPosetilaca = maksimalanBrojPosetilaca;
            SpisakPosetilaca = new List<string>();
            Obrisan = false;
            Id = r.Next(100, 10000);
        }
    }
}