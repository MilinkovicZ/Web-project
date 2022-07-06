using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace MyWebApp.Models
{
    public class FitnesCentarCRUD
    {
        public static List<FitnesCentar> listaFitnesCentri { get; set; } = DeserializeFitnesCentar();

        public static FitnesCentar findById(int id)
        {
            return listaFitnesCentri.Find(fitnesCentar => fitnesCentar.Id == id);
        }

        public static FitnesCentar findByNazivGradUlica(string naziv, string grad, string ulica)
        {
            return listaFitnesCentri.Find(fc => fc.Naziv.Equals(naziv) && fc.AdresaFitnesCentra.Grad.Equals(grad) && fc.AdresaFitnesCentra.Ulica.Equals(ulica));
        }

        public static void SerializeFitnesCentar()
        {
            XmlSerializer xmls = new XmlSerializer(typeof(List<FitnesCentar>));
            using (var txtWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Data\\fitnescentri.xml"))
            {
                xmls.Serialize(txtWriter, listaFitnesCentri);
            }
        }

        public static List<FitnesCentar> DeserializeFitnesCentar()
        {
            List<FitnesCentar> fitnesCentri;
            XmlSerializer ser = new XmlSerializer(typeof(List<FitnesCentar>));
            using (var reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Data\\fitnescentri.xml"))
            {
                fitnesCentri = (List<FitnesCentar>)ser.Deserialize(reader);
            }
            return fitnesCentri;
        }
    }
}