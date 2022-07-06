using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyWebApp.Controllers
{
    public class HistoryController : ApiController
    { 
        [Route("api/history/{id}/{nacin}/{tip}")]
        public List<GrupniTrening> Get(string id, string nacin, int tip)
        {
            Korisnik korisnik = KorisnikCRUD.findByNaziv(id);
            List<GrupniTrening> lista = korisnik.Treninzi;
            List<GrupniTrening> proslost = new List<GrupniTrening>();

            foreach (var item in lista)
            {
                if (DateTime.Parse(item.VremeTreninga) <= DateTime.Now)
                    proslost.Add(item);
            }

            if (nacin == "naziv" && tip == 1)
            {
                proslost.Sort((gt1, gt2) => string.Compare(gt1.Naziv, gt2.Naziv));
            }
            else if (nacin == "naziv" && tip == 2)
            {
                proslost.Sort((gt1, gt2) => string.Compare(gt2.Naziv, gt1.Naziv));
            }
            if (nacin == "tipTreninga" && tip == 1)
            {
                proslost.Sort((gt1, gt2) => string.Compare(gt1.TipTreninga.ToString(), gt2.TipTreninga.ToString()));
            }
            else if (nacin == "tipTreninga" && tip == 2)
            {
                proslost.Sort((gt1, gt2) => string.Compare(gt2.TipTreninga.ToString(), gt1.TipTreninga.ToString()));
            }
            if (nacin == "vreme" && tip == 1)
            {
                proslost.Sort((gt1, gt2) => string.Compare(gt1.VremeTreninga, gt2.VremeTreninga));
            }
            else if (nacin == "vreme" && tip == 2)
            {
                proslost.Sort((gt1, gt2) => string.Compare(gt2.VremeTreninga, gt1.VremeTreninga));
            }

            return proslost;
        }

        [Route("api/history/{id}")]
        public List<GrupniTrening> Get(string id, [FromUri]string naziv, [FromUri]string tipTreninga, [FromUri]string nazivFitnesCentra)
        {
            Korisnik korisnik = KorisnikCRUD.findByNaziv(id);
            List<GrupniTrening> lista = korisnik.Treninzi;
            List<GrupniTrening> proslost = new List<GrupniTrening>();

            foreach (var item in lista)
            {
                if (DateTime.Parse(item.VremeTreninga) <= DateTime.Now)
                    proslost.Add(item);
            }

            List<GrupniTrening> filterLista = new List<GrupniTrening>();

            if (naziv == null && tipTreninga == null && nazivFitnesCentra == null)
                return proslost;
            else
            {
                foreach (GrupniTrening gt in proslost)
                {
                    if (naziv == null)
                    {
                        if (tipTreninga == null)
                        {
                            if (gt.FitnesCentarOdrzavanja.Naziv.Equals(nazivFitnesCentra))
                                filterLista.Add(gt);
                        }
                        else
                        {
                            if (nazivFitnesCentra == null)
                            {
                                if (gt.TipTreninga.ToString() == tipTreninga)
                                    filterLista.Add(gt);
                            }
                            else
                            {
                                if (gt.FitnesCentarOdrzavanja.Naziv.Equals(nazivFitnesCentra) && gt.TipTreninga.ToString() == tipTreninga)
                                    filterLista.Add(gt);
                            }
                        }                        
                    }
                    else
                    {
                        if(tipTreninga == null)
                        {
                            if(nazivFitnesCentra == null)
                            {
                                if (gt.Naziv.Equals(naziv))
                                    filterLista.Add(gt);
                            }
                            else
                            {
                                if (gt.Naziv.Equals(naziv) && gt.FitnesCentarOdrzavanja.Naziv.Equals(nazivFitnesCentra))
                                    filterLista.Add(gt);
                            }
                        }
                        else
                        {
                            if (nazivFitnesCentra == null)
                            {
                                if (gt.Naziv.Equals(naziv) && gt.TipTreninga.ToString() == tipTreninga)
                                    filterLista.Add(gt);
                            }
                            else
                            {
                                if (gt.Naziv.Equals(naziv) && gt.FitnesCentarOdrzavanja.Naziv.Equals(nazivFitnesCentra) && gt.TipTreninga.ToString() == tipTreninga)
                                    filterLista.Add(gt);
                            }
                        }
                    }
                }
            }
            return filterLista;
        }
    }    
}
