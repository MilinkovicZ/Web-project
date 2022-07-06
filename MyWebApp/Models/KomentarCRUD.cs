using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace MyWebApp.Models
{
    public class KomentarCRUD
    {
        public static List<Komentar> listaKomentara { get; set; } = DeserializeKomentar();

        public static List<Komentar> findByFitnesCentar(string nazivFitnesCentra, string grad, string ulica, int broj)
        {
            List<Komentar> komentari = new List<Komentar>();
            foreach (Komentar item in listaKomentara)
            {
                if (item.FitnesCentar.Naziv == nazivFitnesCentra && item.FitnesCentar.AdresaFitnesCentra.Grad == grad &&item.FitnesCentar.AdresaFitnesCentra.Ulica == ulica && item.FitnesCentar.AdresaFitnesCentra.Broj == broj)
                {
                    komentari.Add(item);
                }
            }
            return komentari;
        }

        public static void SerializeKomentar()
        {
            XmlSerializer xmls = new XmlSerializer(typeof(List<Komentar>));
            using (var txtWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Data\\komentari.xml"))
            {
                xmls.Serialize(txtWriter, listaKomentara);
            }
        }

        public static List<Komentar> DeserializeKomentar()
        {
            List<Komentar> komentari;
            XmlSerializer ser = new XmlSerializer(typeof(List<Komentar>));
            using (var reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Data\\komentari.xml"))
            {
                komentari = (List<Komentar>)ser.Deserialize(reader);
            }
            return komentari;
        }

        public static Komentar getById(int id)
        {
            return listaKomentara.Find(k => k.Id == id);
        }
    }
}