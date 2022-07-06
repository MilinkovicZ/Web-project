using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyWebApp.Controllers
{
    public class TreningController : ApiController
    {  

        public List<string> Get(string id)
        {
            GrupniTrening trening = GrupniTreningCRUD.findById(id);
            List<string> ret = new List<string>();   
            ret = trening.SpisakPosetilaca;
            return ret;
        }

        public IHttpActionResult Put(string id, [FromBody]GrupniTrening trening)
        {
            GrupniTrening gtr = GrupniTreningCRUD.findById(id);

            if (gtr != null)
            {
                if (trening == null)
                    return BadRequest();
                if (trening.Naziv == null || trening.Naziv == "")
                    return BadRequest();
                if (trening.TrajanjeTreninga <= 0)
                    return BadRequest();
                if (trening.VremeTreninga == null || trening.VremeTreninga == "")
                    return BadRequest();
                if (trening.MaksimalanBrojPosetilaca <= 0)
                    return BadRequest();               

                DateTime temp = DateTime.Now;
                temp = temp.AddDays(3);
                if (DateTime.Parse(trening.VremeTreninga) <= temp)
                    return BadRequest();

                gtr.Naziv = trening.Naziv;
                gtr.TipTreninga = trening.TipTreninga;
                gtr.TrajanjeTreninga = trening.TrajanjeTreninga;
                gtr.VremeTreninga = trening.VremeTreninga;
                gtr.MaksimalanBrojPosetilaca = trening.MaksimalanBrojPosetilaca;

                foreach (Korisnik k in KorisnikCRUD.listaKorisnika)
                {
                    foreach (GrupniTrening tr in k.Treninzi)
                    {
                        if (tr.Id.Equals(gtr.Id))
                        {
                            //STAVIM AZURIRANI TRENING
                            tr.Naziv = gtr.Naziv;
                            tr.TipTreninga = gtr.TipTreninga;
                            tr.TrajanjeTreninga = gtr.TrajanjeTreninga;
                            tr.VremeTreninga = gtr.VremeTreninga;
                            tr.MaksimalanBrojPosetilaca = gtr.MaksimalanBrojPosetilaca;
                            break;
                        }
                    }
                }                

                GrupniTreningCRUD.SerializeTrening();
                KorisnikCRUD.SerializeKorisnik();
            }
            else
            {
                return BadRequest();
            }            
            
            return Ok(gtr);
        }

        public IHttpActionResult Delete(string id)
        {
            GrupniTrening trening = GrupniTreningCRUD.findById(id);
            if (trening.SpisakPosetilaca.Count != 0)
                return BadRequest();
            if (DateTime.Parse(trening.VremeTreninga) <= DateTime.Now)
                return BadRequest();

            Korisnik trener = KorisnikCRUD.findTrenerByTraining(id);

            foreach (GrupniTrening t in trener.Treninzi)
            {
                if (t.Id.Equals(int.Parse(id)))
                {
                    t.Obrisan = true;
                    break;
                }
            }

            trening.Obrisan = true;

            GrupniTreningCRUD.SerializeTrening();
            KorisnikCRUD.SerializeKorisnik();

            return Ok();
        }

        public IHttpActionResult Post(string id, GrupniTrening trening)
        {
            if (trening == null)
                return BadRequest();
            if (trening.Naziv == null || trening.Naziv == "")
                return BadRequest();
            if (trening.TrajanjeTreninga <= 0)
                return BadRequest();     
            if (trening.VremeTreninga == null || trening.VremeTreninga == "")
                return BadRequest();
            if (trening.MaksimalanBrojPosetilaca <=0)
                return BadRequest();           
            if (trening.FitnesCentarOdrzavanja.Naziv == null || trening.FitnesCentarOdrzavanja.Naziv == "")
                return BadRequest();

            DateTime temp = DateTime.Now;
            temp = temp.AddDays(3);
            if (DateTime.Parse(trening.VremeTreninga) <= temp)
                return BadRequest();

            trening.FitnesCentarOdrzavanja = FitnesCentarCRUD.listaFitnesCentri.Find(fc => fc.Naziv.Equals(trening.FitnesCentarOdrzavanja.Naziv));
            Korisnik trener = KorisnikCRUD.findByNaziv(id);

            //DA LI POSTOJI FITNES CENTAR S TIM NAZIVOM
            if (trening.FitnesCentarOdrzavanja == null)
                return BadRequest();

            //DA LI TRENER RADI U TOM FITNES CENTRU?
            bool postoji = false;
            foreach (var item in trener.FitnesCentri)
            {
                if ((item.Naziv + item.AdresaFitnesCentra.Grad + item.AdresaFitnesCentra.Ulica).Equals(trening.FitnesCentarOdrzavanja.Naziv + trening.FitnesCentarOdrzavanja.AdresaFitnesCentra.Grad + trening.FitnesCentarOdrzavanja.AdresaFitnesCentra.Ulica))
                    postoji = true;
            }
            if (!postoji)
                return BadRequest();

            //DA LI IMA TRENING SA TIM ID
            foreach (var item in GrupniTreningCRUD.listaGrupniTreninzi)
            {
                if (item.Id == trening.Id)
                    return BadRequest();
            }

            GrupniTrening kopija = new GrupniTrening(trening.Naziv, trening.TipTreninga, trening.FitnesCentarOdrzavanja, trening.TrajanjeTreninga, DateTime.Parse(trening.VremeTreninga), trening.MaksimalanBrojPosetilaca) { Id = trening.Id}; //ZBOG REFERENCI

            trener.Treninzi.Add(trening);
            GrupniTreningCRUD.listaGrupniTreninzi.Add(kopija);
            GrupniTreningCRUD.SerializeTrening();
            KorisnikCRUD.SerializeKorisnik();

            return Ok(trening);
        }
    }    
}
