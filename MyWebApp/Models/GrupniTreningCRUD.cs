using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace MyWebApp.Models
{
    public class GrupniTreningCRUD
    {
        public static List<GrupniTrening> listaGrupniTreninzi { get; set; } = DeserializeTrening();

        public static GrupniTrening findById(string id)
        {
            return listaGrupniTreninzi.Find(gt => gt.Id.Equals(int.Parse(id)));
        }

        public static List<GrupniTrening> findByFitnesCentar(string nazivFitnesCentra, string ulica, int broj)
        {
            List<GrupniTrening> treninzi = new List<GrupniTrening>();
            foreach (GrupniTrening item in listaGrupniTreninzi)
            {
                if(item.FitnesCentarOdrzavanja.Naziv == nazivFitnesCentra && item.FitnesCentarOdrzavanja.AdresaFitnesCentra.Ulica == ulica && item.FitnesCentarOdrzavanja.AdresaFitnesCentra.Broj == broj && DateTime.Parse(item.VremeTreninga) >= DateTime.Now)
                {
                    treninzi.Add(item);
                }
            }
            return treninzi;
        }

        public static void SerializeTrening()
        {
            XmlSerializer xmls = new XmlSerializer(typeof(List<GrupniTrening>));
            using (var txtWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Data\\treninzi.xml"))
            {
                xmls.Serialize(txtWriter, listaGrupniTreninzi);
            }
        }

        public static List<GrupniTrening> DeserializeTrening()
        {
            List<GrupniTrening> treninzi;
            XmlSerializer ser = new XmlSerializer(typeof(List<GrupniTrening>));
            using (var reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Data\\treninzi.xml"))
            {
                treninzi = (List<GrupniTrening>)ser.Deserialize(reader);
            }
            return treninzi;
        }
    }
}