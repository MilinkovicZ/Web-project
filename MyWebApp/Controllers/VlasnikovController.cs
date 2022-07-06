using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyWebApp.Controllers
{
    public class VlasnikovController : ApiController
    {        
        public IHttpActionResult Post(string id, [FromBody]Korisnik tr)
        {            
            Korisnik trener = KorisnikCRUD.findByNaziv(tr.KorisnickoIme);

            if (trener == null)
                return BadRequest();

            //Provera da li je vlasnik koji ga je blokirao u tom fitnes centru
            bool jeste = false;
            foreach (FitnesCentar item in trener.FitnesCentri)
            {
                if (item.Vlasnik.Equals(id))
                    jeste = true;
            }

            if (!jeste)
                return BadRequest();

            trener.Blokiran = true;

            KorisnikCRUD.SerializeKorisnik();
            return Ok();
        }

        public IHttpActionResult Post([FromBody]VlasnikTrener data)
        {
            Korisnik vlasnik = KorisnikCRUD.findByNaziv(data.VlasnikUsername);
            Korisnik trener = KorisnikCRUD.findByNaziv(data.TrenerUsername);
            FitnesCentar fitnesCentar = FitnesCentarCRUD.findByNazivGradUlica(data.Naziv, data.Grad, data.Ulica);

            //AKO NE POSTOJE
            if (trener == null || vlasnik == null || fitnesCentar == null)
            {
                return BadRequest();
            }

            //DA LI JE OBRISAN
            if (fitnesCentar.Obrisan)
                return BadRequest();

            //DA LI JE TRENER BLOKIRAN
            if (trener.Blokiran)
                return BadRequest();

            //DA LI TRENER VEC IMA FITNES CENTAR
            if (trener.FitnesCentri.Count != 0)
                return BadRequest();

            //DA LI JE UOPSTE VLASNIK TOG FITNES CENTRA
            bool jeste = false;
            foreach (FitnesCentar item in vlasnik.FitnesCentri)
            {
                if (item.Id == fitnesCentar.Id)
                {
                    jeste = true;
                    break;
                }                    
            }

            if (!jeste)
                return BadRequest();

            trener.FitnesCentri.Add(fitnesCentar);
            KorisnikCRUD.SerializeKorisnik(); 

            return Ok();
        }
    }
}
