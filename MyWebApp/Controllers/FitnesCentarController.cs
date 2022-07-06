using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyWebApp.Controllers
{
    public class FitnesCentarController : ApiController
    {
        [Route("api/fitnescentar/{vrsta}/{tip}")]
        public List<FitnesCentar> Get(string vrsta, int tip)
        {
            List<FitnesCentar> lista = FitnesCentarCRUD.listaFitnesCentri;
            if (vrsta == "naziv" && tip == 1)
            {
                lista.Sort((fc1, fc2) => string.Compare(fc1.Naziv, fc2.Naziv));
            }          
            else if (vrsta == "naziv" && tip == 2)
            {
                lista.Sort((fc1, fc2) => string.Compare(fc2.Naziv, fc1.Naziv));
            }
            if (vrsta == "adresa" && tip == 1)
            {
                lista.Sort((fc1, fc2) => string.Compare(fc1.AdresaFitnesCentra.Ulica, fc2.AdresaFitnesCentra.Ulica));
            }
            else if (vrsta == "adresa" && tip == 2)
            {
                lista.Sort((fc1, fc2) => string.Compare(fc2.AdresaFitnesCentra.Ulica, fc1.AdresaFitnesCentra.Ulica));
            }
            if (vrsta == "godina" && tip == 1)
            {
                lista.Sort((fc1, fc2) => string.Compare(fc1.GodinaOtvaranja.ToString(), fc2.GodinaOtvaranja.ToString()));
            }
            else if (vrsta == "godina" && tip == 2)
            {
                lista.Sort((fc1, fc2) => string.Compare(fc2.GodinaOtvaranja.ToString(), fc1.GodinaOtvaranja.ToString()));
            }

            return lista;
        }        
        
        public FitnesCentar Get(string id)
        {
            return FitnesCentarCRUD.findById(int.Parse(id));
        }

        [Route("api/fitnescentar/{nazivFitnesCentra}/{ulica}/{broj}")]
        public List<GrupniTrening> Get(string nazivFitnesCentra, string ulica, int broj)
        {
            return GrupniTreningCRUD.findByFitnesCentar(nazivFitnesCentra, ulica, broj);
        }

        [Route("api/fitnescentar/{nazivFitnesCentra}/{grad}/{ulica}/{broj}")]
        public List<Komentar> Get(string nazivFitnesCentra, string grad, string ulica, int broj)
        {
            return KomentarCRUD.findByFitnesCentar(nazivFitnesCentra, grad, ulica, broj);
        }
    }
}
