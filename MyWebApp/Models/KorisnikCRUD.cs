using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace MyWebApp.Models
{
    public class KorisnikCRUD
    {
        public static List<Korisnik> listaKorisnika { get; set; } = DeserializeKorisnik();

        public static Korisnik AddKorisnik(Korisnik korisnik)
        {
            listaKorisnika.Add(korisnik);
            return korisnik;
        }

        public static Korisnik findByNaziv(string username)
        {
            return listaKorisnika.Find(k => k.KorisnickoIme.Equals(username));
        }

        public static Korisnik findTrenerByTraining(string idTreninga)
        {
            int gt = -1;
            Korisnik trener = new Korisnik();

            foreach (GrupniTrening item in GrupniTreningCRUD.listaGrupniTreninzi)
            {
                if (item.Id == int.Parse(idTreninga))
                    gt = item.Id;
            }

            foreach (Korisnik item in listaKorisnika)
            {
                if(item.UlogaKorisnika == Uloga.TRENER)
                {
                    foreach(GrupniTrening trening in item.Treninzi)
                    {
                        if (trening.Id == gt)
                            trener = item;
                    }
                }
            }

            return trener;
        }

        public static void SerializeKorisnik()
        {
            XmlSerializer xmls = new XmlSerializer(typeof(List<Korisnik>));
            using (var txtWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Data\\korisnici.xml"))
            {
                xmls.Serialize(txtWriter, listaKorisnika);
            }                
        }

        public static List<Korisnik> DeserializeKorisnik()
        {
            List<Korisnik> korisnici;
            XmlSerializer ser = new XmlSerializer(typeof(List<Korisnik>));
            using (var reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Data\\korisnici.xml"))
            {
                korisnici = (List<Korisnik>)ser.Deserialize(reader);
            }
            return korisnici;
        }
    }
}