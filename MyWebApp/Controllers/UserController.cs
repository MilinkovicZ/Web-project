using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyWebApp.Controllers
{
    public class UserController : ApiController
    { 
        public List<Korisnik> Get()
        {
            return KorisnikCRUD.listaKorisnika;
        }

        public Korisnik Get(string id)
        {            
            return KorisnikCRUD.findByNaziv(id);
        }

        public IHttpActionResult Put(string id, [FromBody]Korisnik korisnik)
        {
            List<Korisnik> lista = KorisnikCRUD.listaKorisnika;
            Korisnik isti = lista.Find(kor => kor.KorisnickoIme.Equals(korisnik.KorisnickoIme));

            if (isti != null)
                return BadRequest();

            Korisnik stari = KorisnikCRUD.findByNaziv(id);
            Korisnik temp = new Korisnik(stari.KorisnickoIme, stari.Lozinka, stari.Ime, stari.Prezime, stari.PolKorisnika, stari.Email, DateTime.Parse(stari.DatumRodjenja))
            {
                UlogaKorisnika = stari.UlogaKorisnika,
                Treninzi = stari.Treninzi,
                FitnesCentri = stari.FitnesCentri
            };
                      

            if(stari != null)
            {
                if (korisnik == null)
                    return BadRequest();
                if (korisnik.Ime == null || korisnik.Ime == "")
                    return BadRequest();
                if (korisnik.Prezime == null || korisnik.Prezime == "")
                    return BadRequest();
                if (korisnik.KorisnickoIme == null || korisnik.KorisnickoIme == "")
                    return BadRequest();
                if (korisnik.Lozinka == null || korisnik.Lozinka == "")
                    return BadRequest();
                if (korisnik.Email == null || korisnik.Email == "" || !korisnik.Email.Contains("@"))
                    return BadRequest();
                if (korisnik.DatumRodjenja == null)
                    return BadRequest();

                stari.Ime = korisnik.Ime;
                stari.Prezime = korisnik.Prezime;
                stari.KorisnickoIme = korisnik.KorisnickoIme;
                stari.Lozinka = korisnik.Lozinka;
                stari.Email = korisnik.Email;
                stari.DatumRodjenja = korisnik.DatumRodjenja;
                stari.PolKorisnika = korisnik.PolKorisnika;

                
                foreach (Komentar komentar in KomentarCRUD.listaKomentara)
                {
                    //AZURIRAM KORISNICKE PODATKE U KOMENTARU
                    if (komentar.Korisnik.KorisnickoIme.Equals(temp.KorisnickoIme))
                        komentar.Korisnik = stari;
                }
                foreach (GrupniTrening tr in GrupniTreningCRUD.listaGrupniTreninzi)
                {
                    //AZURIRAM KORISNICKE PODATKE U TRENINGU
                    if (tr.SpisakPosetilaca.Contains(temp.KorisnickoIme))
                    {
                        tr.SpisakPosetilaca.Remove(temp.KorisnickoIme);
                        tr.SpisakPosetilaca.Add(stari.KorisnickoIme);
                    }                        
                }
                foreach (Korisnik item in lista)
                {
                    foreach (GrupniTrening tr in item.Treninzi)
                    {
                        //AZUZIRAM PODATKE TRENIGNA KOD SVIH KORISNIKA KOJI POHADJAJU TAJ TRENING
                        if (tr.SpisakPosetilaca.Contains(temp.KorisnickoIme))
                        {
                            tr.SpisakPosetilaca.Remove(temp.KorisnickoIme);
                            tr.SpisakPosetilaca.Add(stari.KorisnickoIme);
                        }
                    }
                }
                
                KorisnikCRUD.SerializeKorisnik();
                KomentarCRUD.SerializeKomentar();
                GrupniTreningCRUD.SerializeTrening();

            }
            else
            {
                return BadRequest();
            }

            return Ok(stari);
        }

        public IHttpActionResult Post([FromBody]KorisnikTrening korisnikTrening)
        {            
            Korisnik k = KorisnikCRUD.findByNaziv(korisnikTrening.idKorisnik);
            GrupniTrening gt = GrupniTreningCRUD.findById(korisnikTrening.idTrening);

            if (k.UlogaKorisnika.ToString() != "POSETILAC")
                return BadRequest();

            if (k.Treninzi.Contains(gt))
                return BadRequest();            

            if (gt.SpisakPosetilaca.Count >= gt.MaksimalanBrojPosetilaca)
                return BadRequest();

            if (gt.SpisakPosetilaca.Contains(korisnikTrening.idKorisnik))
                return BadRequest();

            Korisnik trener = KorisnikCRUD.findTrenerByTraining(korisnikTrening.idTrening);           

            foreach (GrupniTrening trening in trener.Treninzi)
            {
                if (trening.Id.Equals(int.Parse(korisnikTrening.idTrening)))
                {
                    trening.SpisakPosetilaca.Add(korisnikTrening.idKorisnik); //U TRENENEROV SPISAK DODAM STRING
                    break;
                }
            }

            k.Treninzi.Add(gt); //OBJEKT TRENING U KORISNIKA KOJI SE PRIJAVIO NA NJEGA
            gt.SpisakPosetilaca.Add(korisnikTrening.idKorisnik); //U KORISNIKOV SPISAK DODAM STRING

            GrupniTreningCRUD.SerializeTrening();
            KorisnikCRUD.SerializeKorisnik();

            return Ok();
        }

        [Route("api/user/{userId}/{fcId}")]
        public IHttpActionResult Post([FromUri]string userId,[FromUri]string fcId, [FromBody]Komentar komentar)
        {
            if (komentar.TekstKomentara == null || komentar.TekstKomentara == "")
                return BadRequest();

            if (komentar.Ocena > 5 || komentar.Ocena < 1)
                return BadRequest();

            Korisnik k = KorisnikCRUD.findByNaziv(userId);

            if (k.UlogaKorisnika.ToString() != "POSETILAC")
                return BadRequest();

            FitnesCentar f = FitnesCentarCRUD.findById(int.Parse(fcId));

            komentar.Korisnik = k;
            komentar.FitnesCentar = f;

            KomentarCRUD.listaKomentara.Add(komentar);
            KomentarCRUD.SerializeKomentar();

            return Ok();
        }
    }
}
