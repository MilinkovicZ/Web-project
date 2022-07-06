using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyWebApp.Controllers
{
    public class FilterController : ApiController
    {        
        public List<FitnesCentar> Get([FromUri]string naziv, [FromUri]string adresa, [FromUri]string minGodina, [FromUri]string maxGodina)
        {
            List<FitnesCentar> lista = FitnesCentarCRUD.listaFitnesCentri;
            List<FitnesCentar> filterLista = new List<FitnesCentar>();

            if (naziv == null && adresa == null && minGodina == null && maxGodina == null)
                return lista;
            else
            {
                foreach (FitnesCentar fc in lista)
                {
                    if(naziv == null)
                    {
                        if(adresa == null)
                        {
                            if(minGodina == null)
                            {
                                int max = int.Parse(maxGodina);
                                if (fc.GodinaOtvaranja <= max)
                                    filterLista.Add(fc);
                            }
                            else if (maxGodina == null)
                            {
                                int min = int.Parse(minGodina);
                                if (fc.GodinaOtvaranja >= min)
                                    filterLista.Add(fc);
                            }
                            else
                            {
                                int min = int.Parse(minGodina);
                                int max = int.Parse(maxGodina);
                                if (fc.GodinaOtvaranja >= min && fc.GodinaOtvaranja <= max)
                                    filterLista.Add(fc);
                            }
                        }
                        else
                        {
                            if(minGodina == null && maxGodina == null)
                            {
                                if ((fc.AdresaFitnesCentra.Ulica + " " + fc.AdresaFitnesCentra.Broj).Equals(adresa))
                                    filterLista.Add(fc);                                
                            }
                            else if (minGodina == null)
                            {
                                int max = int.Parse(maxGodina);
                                if ((fc.AdresaFitnesCentra.Ulica + " " + fc.AdresaFitnesCentra.Broj).Equals(adresa) && fc.GodinaOtvaranja <= max)
                                    filterLista.Add(fc);
                            }
                            else if (maxGodina == null)
                            {
                                int min = int.Parse(minGodina);
                                if ((fc.AdresaFitnesCentra.Ulica + " " + fc.AdresaFitnesCentra.Broj).Equals(adresa) && fc.GodinaOtvaranja >= min)
                                    filterLista.Add(fc);
                            }
                            else
                            {
                                int min = int.Parse(minGodina);
                                int max = int.Parse(maxGodina);
                                if ((fc.AdresaFitnesCentra.Ulica + " " + fc.AdresaFitnesCentra.Broj).Equals(adresa) && fc.GodinaOtvaranja >= min && fc.GodinaOtvaranja <= max)
                                    filterLista.Add(fc);
                            }
                        }
                    }
                    else
                    {
                        if (adresa == null && minGodina == null && maxGodina == null)
                        {
                            if (fc.Naziv.Equals(naziv))
                                filterLista.Add(fc);
                        }
                        else if (adresa == null)
                        {
                            if (minGodina == null)
                            {
                                int max = int.Parse(maxGodina);
                                if (fc.Naziv.Equals(naziv) && fc.GodinaOtvaranja <= max)
                                    filterLista.Add(fc);
                            }
                            else if (maxGodina == null)
                            {
                                int min = int.Parse(minGodina);
                                if (fc.Naziv.Equals(naziv) && fc.GodinaOtvaranja >= min)
                                    filterLista.Add(fc);
                            }
                            else
                            {
                                int min = int.Parse(minGodina);
                                int max = int.Parse(maxGodina);
                                if (fc.Naziv.Equals(naziv) && fc.GodinaOtvaranja >= min && fc.GodinaOtvaranja <= max)
                                    filterLista.Add(fc);
                            }
                        }
                        else
                        {
                            if (minGodina == null && maxGodina == null)
                            {
                                if ((fc.AdresaFitnesCentra.Ulica + " " + fc.AdresaFitnesCentra.Broj).Equals(adresa) && fc.Naziv.Equals(naziv))
                                    filterLista.Add(fc);
                            }
                            else if (minGodina == null)
                            {
                                int max = int.Parse(maxGodina);
                                if ((fc.AdresaFitnesCentra.Ulica + " " + fc.AdresaFitnesCentra.Broj).Equals(adresa) && fc.Naziv.Equals(naziv) && fc.GodinaOtvaranja <= max)
                                    filterLista.Add(fc);
                            }
                            else if (maxGodina == null)
                            {
                                int min = int.Parse(minGodina);
                                if ((fc.AdresaFitnesCentra.Ulica + " " + fc.AdresaFitnesCentra.Broj).Equals(adresa) && fc.Naziv.Equals(naziv) && fc.GodinaOtvaranja >= min)
                                    filterLista.Add(fc);
                            }
                            else
                            {
                                int min = int.Parse(minGodina);
                                int max = int.Parse(maxGodina);
                                if ((fc.AdresaFitnesCentra.Ulica + " " + fc.AdresaFitnesCentra.Broj).Equals(adresa) && fc.Naziv.Equals(naziv) && fc.GodinaOtvaranja >= min && fc.GodinaOtvaranja <= max)
                                    filterLista.Add(fc);
                            }
                        }
                    }
                }
            }
            return filterLista;
        }
    }
}
