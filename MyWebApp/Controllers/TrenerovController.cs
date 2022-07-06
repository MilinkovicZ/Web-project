using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyWebApp.Controllers
{
    public class TrenerovController : ApiController
    {
        public List<GrupniTrening> Get(string id)
        {
            Korisnik trener = KorisnikCRUD.findByNaziv(id);
            List<GrupniTrening> lista = trener.Treninzi;
            List<GrupniTrening> buduci = new List<GrupniTrening>();

            foreach (var item in lista)
            {
                if (DateTime.Parse(item.VremeTreninga) >= DateTime.Now)
                    buduci.Add(item);
            }

            buduci.Sort((gt1, gt2) => string.Compare(gt1.VremeTreninga, gt2.VremeTreninga));

            return buduci;
        }

        [Route("api/trener/{id}")]
        public List<GrupniTrening> Get(string id,[FromUri]string naziv, [FromUri]string tipTreninga, [FromUri]string minVreme, [FromUri]string maxVreme)
        {
            Korisnik trener = KorisnikCRUD.findByNaziv(id);
            List<GrupniTrening> lista = trener.Treninzi;
            List<GrupniTrening> proslost = new List<GrupniTrening>();

            foreach (var item in lista)
            {
                if (DateTime.Parse(item.VremeTreninga) <= DateTime.Now)
                    proslost.Add(item);
            }

            List<GrupniTrening> filterLista = new List<GrupniTrening>();

            if (naziv == null && tipTreninga == null && minVreme == null && maxVreme == null)
                return proslost;
            else
            {
                foreach (GrupniTrening gt in proslost)
                {
                    if (naziv == null)
                    {
                        if (tipTreninga == null)
                        {
                            if (minVreme == null)
                            {                                
                                if (DateTime.Parse(gt.VremeTreninga) <= DateTime.Parse(maxVreme))
                                    filterLista.Add(gt);
                            }
                            else if (maxVreme == null)
                            {                                
                                if (DateTime.Parse(gt.VremeTreninga) >= DateTime.Parse(minVreme))
                                    filterLista.Add(gt);
                            }
                            else
                            {                                
                                if (DateTime.Parse(gt.VremeTreninga) >= DateTime.Parse(minVreme) && DateTime.Parse(gt.VremeTreninga) <= DateTime.Parse(maxVreme))
                                    filterLista.Add(gt);
                            }
                        }
                        else
                        {
                            if (minVreme == null && maxVreme == null)
                            {
                                if (gt.TipTreninga.ToString() == tipTreninga)
                                    filterLista.Add(gt);
                            }
                            else if (minVreme == null)
                            {                                
                                if (gt.TipTreninga.ToString() == tipTreninga && DateTime.Parse(gt.VremeTreninga) <= DateTime.Parse(maxVreme))
                                    filterLista.Add(gt);
                            }
                            else if (maxVreme == null)
                            {                                
                                if (gt.TipTreninga.ToString() == tipTreninga && DateTime.Parse(gt.VremeTreninga) >= DateTime.Parse(minVreme))
                                    filterLista.Add(gt);
                            }
                            else
                            {
                                if (gt.TipTreninga.ToString() == tipTreninga && DateTime.Parse(gt.VremeTreninga) >= DateTime.Parse(minVreme) && DateTime.Parse(gt.VremeTreninga) <= DateTime.Parse(maxVreme))
                                    filterLista.Add(gt);
                            }
                        }
                    }
                    else
                    {
                        if (tipTreninga == null && minVreme == null && maxVreme == null)
                        {
                            if (gt.Naziv.Equals(naziv))
                                filterLista.Add(gt);
                        }
                        else if (tipTreninga == null)
                        {
                            if (minVreme == null)
                            {
                                if (gt.Naziv.Equals(naziv) && DateTime.Parse(gt.VremeTreninga) <= DateTime.Parse(maxVreme))
                                    filterLista.Add(gt);
                            }
                            else if (maxVreme == null)
                            {
                                if (gt.Naziv.Equals(naziv) && DateTime.Parse(gt.VremeTreninga) >= DateTime.Parse(minVreme))
                                    filterLista.Add(gt);
                            }
                            else
                            {                                
                                if (gt.Naziv.Equals(naziv) && DateTime.Parse(gt.VremeTreninga) >= DateTime.Parse(minVreme) && DateTime.Parse(gt.VremeTreninga) <= DateTime.Parse(maxVreme))
                                    filterLista.Add(gt);
                            }
                        }
                        else
                        {
                            if (minVreme == null && maxVreme == null)
                            {
                                if (gt.Naziv.Equals(naziv) && gt.TipTreninga.ToString() == tipTreninga)
                                    filterLista.Add(gt);
                            }
                            else if (minVreme == null)
                            {
                                if (gt.Naziv.Equals(naziv) && gt.TipTreninga.ToString() == tipTreninga && DateTime.Parse(gt.VremeTreninga) <= DateTime.Parse(maxVreme))
                                    filterLista.Add(gt);
                            }
                            else if (maxVreme == null)
                            {
                                if (gt.Naziv.Equals(naziv) && gt.TipTreninga.ToString() == tipTreninga && DateTime.Parse(gt.VremeTreninga) >= DateTime.Parse(minVreme))
                                    filterLista.Add(gt);
                            }
                            else
                            {
                                if (gt.Naziv.Equals(naziv) && gt.TipTreninga.ToString() == tipTreninga && DateTime.Parse(gt.VremeTreninga) >= DateTime.Parse(minVreme) && DateTime.Parse(gt.VremeTreninga) <= DateTime.Parse(maxVreme))
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
