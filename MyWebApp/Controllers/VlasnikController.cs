using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyWebApp.Controllers
{
    public class VlasnikController : ApiController
    {
        public List<FitnesCentar> Get(string id)
        {
            Korisnik vlasnik = KorisnikCRUD.findByNaziv(id);
            return vlasnik.FitnesCentri;
        }

        public IHttpActionResult Delete(string id)
        {
            FitnesCentar fitnesCentar = FitnesCentarCRUD.findById(int.Parse(id));

            foreach (GrupniTrening trening in GrupniTreningCRUD.listaGrupniTreninzi)
            {
                if (trening.FitnesCentarOdrzavanja.Id == fitnesCentar.Id)
                    return BadRequest();
            }

            foreach (Korisnik item in KorisnikCRUD.listaKorisnika)
            {
                foreach (FitnesCentar fitnes in item.FitnesCentri)
                {
                    if(fitnes.Id == fitnesCentar.Id)
                    {
                        fitnes.Obrisan = true;

                        if(item.UlogaKorisnika == Uloga.TRENER)
                        {
                            item.Blokiran = true;
                        }
                    }
                }
            }

            fitnesCentar.Obrisan = true;

            FitnesCentarCRUD.SerializeFitnesCentar();
            KorisnikCRUD.SerializeKorisnik();

            return Ok();
        }

        public IHttpActionResult Post(string id, FitnesCentar fitnes)
        {
            if (fitnes == null)
                return BadRequest();
            if (fitnes.Naziv == null || fitnes.Naziv == "")
                return BadRequest();
            if (fitnes.AdresaFitnesCentra.Grad == null || fitnes.AdresaFitnesCentra.Grad == "")
                return BadRequest();
            if (fitnes.AdresaFitnesCentra.Ulica == null || fitnes.AdresaFitnesCentra.Ulica == "")
                return BadRequest();
            if (fitnes.AdresaFitnesCentra.Broj <= 0)
                return BadRequest();
            if (fitnes.AdresaFitnesCentra.PostanskiBroj <= 0)
                return BadRequest();
            if (fitnes.GodinaOtvaranja <= 2000)
                return BadRequest();
            if (fitnes.MesecnaCena <= 0)
                return BadRequest();
            if (fitnes.GodisnjaCena <= 0)
                return BadRequest();
            if (fitnes.CenaJednogTreninga <= 0)
                return BadRequest();
            if (fitnes.CenaJednogTreningaGrupni <= 0)
                return BadRequest();
            if (fitnes.CenaJednogTreningaPersonalni <= 0)
                return BadRequest();

            //AKO IMAJU ISTI ID
            foreach (var item in FitnesCentarCRUD.listaFitnesCentri)
            {
                if (item.Id == fitnes.Id)
                    return BadRequest();
            }

            FitnesCentarCRUD.listaFitnesCentri.Add(fitnes);

            Korisnik vlasnik = KorisnikCRUD.findByNaziv(id);
            vlasnik.FitnesCentri.Add(fitnes);

            FitnesCentarCRUD.SerializeFitnesCentar();
            KorisnikCRUD.SerializeKorisnik();

            return Ok(fitnes);
        }

        public IHttpActionResult Put(string id, [FromBody]FitnesCentar fitnes)
        {
            FitnesCentar fitnesCentar = FitnesCentarCRUD.findById(int.Parse(id));

            if (fitnesCentar != null)
            {
                if (fitnes == null)
                    return BadRequest();
                if (fitnes.Naziv == null || fitnes.Naziv == "")
                    return BadRequest();
                if (fitnes.AdresaFitnesCentra.Grad == null || fitnes.AdresaFitnesCentra.Grad == "")
                    return BadRequest();
                if (fitnes.AdresaFitnesCentra.Ulica == null || fitnes.AdresaFitnesCentra.Ulica == "")
                    return BadRequest();
                if (fitnes.AdresaFitnesCentra.Broj <= 0)
                    return BadRequest();
                if (fitnes.AdresaFitnesCentra.PostanskiBroj <= 0)
                    return BadRequest();
                if (fitnes.GodinaOtvaranja <= 2000)
                    return BadRequest();
                if (fitnes.MesecnaCena <= 0)
                    return BadRequest();
                if (fitnes.GodisnjaCena <= 0)
                    return BadRequest();
                if (fitnes.CenaJednogTreninga <= 0)
                    return BadRequest();
                if (fitnes.CenaJednogTreningaGrupni <= 0)
                    return BadRequest();
                if (fitnes.CenaJednogTreningaPersonalni <= 0)
                    return BadRequest();

                fitnesCentar.Naziv = fitnes.Naziv;
                fitnesCentar.AdresaFitnesCentra.Grad = fitnes.AdresaFitnesCentra.Grad;
                fitnesCentar.AdresaFitnesCentra.Ulica = fitnes.AdresaFitnesCentra.Ulica;
                fitnesCentar.AdresaFitnesCentra.Broj = fitnes.AdresaFitnesCentra.Broj;
                fitnesCentar.AdresaFitnesCentra.PostanskiBroj = fitnes.AdresaFitnesCentra.PostanskiBroj;
                fitnesCentar.GodinaOtvaranja = fitnes.GodinaOtvaranja;
                fitnesCentar.MesecnaCena = fitnes.MesecnaCena;
                fitnesCentar.GodisnjaCena = fitnes.GodisnjaCena;
                fitnesCentar.CenaJednogTreninga = fitnes.CenaJednogTreninga;
                fitnesCentar.CenaJednogTreningaGrupni = fitnes.CenaJednogTreningaGrupni;
                fitnesCentar.CenaJednogTreningaPersonalni = fitnes.CenaJednogTreningaPersonalni;

                foreach (Korisnik k in KorisnikCRUD.listaKorisnika)
                {
                    foreach (FitnesCentar fc in k.FitnesCentri)
                    {
                        if (fc.Id.Equals(fitnesCentar.Id))
                        {
                            //STAVIM AZURIRANI FITNES CENTAR SVIM TRENERIMA KOJI RADE TAMO I VLASNIKU
                            fc.Naziv = fitnes.Naziv;
                            fc.AdresaFitnesCentra.Grad = fitnesCentar.AdresaFitnesCentra.Grad;
                            fc.AdresaFitnesCentra.Ulica = fitnesCentar.AdresaFitnesCentra.Ulica;
                            fc.AdresaFitnesCentra.Broj = fitnesCentar.AdresaFitnesCentra.Broj;
                            fc.AdresaFitnesCentra.PostanskiBroj = fitnesCentar.AdresaFitnesCentra.PostanskiBroj;
                            fc.GodinaOtvaranja = fitnesCentar.GodinaOtvaranja;
                            fc.MesecnaCena = fitnesCentar.MesecnaCena;
                            fc.GodisnjaCena = fitnesCentar.GodisnjaCena;
                            fc.CenaJednogTreninga = fitnesCentar.CenaJednogTreninga;
                            fc.CenaJednogTreningaGrupni = fitnesCentar.CenaJednogTreningaGrupni;
                            fc.CenaJednogTreningaPersonalni = fitnesCentar.CenaJednogTreningaPersonalni;
                            break;
                        }
                    }
                    foreach (GrupniTrening gt in k.Treninzi)
                    {
                        //STAVIM AZURIRANI FITNES CENTAR KOD SVIH KORISNIKA KOJI IMAJU TRENING TAMO
                        if (gt.FitnesCentarOdrzavanja.Id == fitnesCentar.Id)
                            gt.FitnesCentarOdrzavanja = fitnesCentar;
                    }
                }

                foreach (GrupniTrening gt in GrupniTreningCRUD.listaGrupniTreninzi)
                {
                    //STAVIM AZURIRANI FITNES CENTAR KOD SVIH TRENINGA
                    if (gt.FitnesCentarOdrzavanja.Id == fitnesCentar.Id)
                        gt.FitnesCentarOdrzavanja = fitnesCentar;
                }

                foreach (Komentar k in KomentarCRUD.listaKomentara)
                {
                    if (k.FitnesCentar.Id == fitnesCentar.Id)
                        k.FitnesCentar = fitnesCentar;
                }


                GrupniTreningCRUD.SerializeTrening();
                FitnesCentarCRUD.SerializeFitnesCentar();
                KomentarCRUD.SerializeKomentar();
                KorisnikCRUD.SerializeKorisnik();
            }
            else
            {
                return BadRequest();
            }

            return Ok(fitnesCentar);
        }
    }
}