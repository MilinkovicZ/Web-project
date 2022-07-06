using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace MyWebApp.Controllers
{
    public class AuthentificatorController : ApiController
    {
        //POST api/authentificator
        public IHttpActionResult Post(Korisnik korisnik)
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
            if (korisnik.Email == null || korisnik.Email == "" || !korisnik.Email.Contains("@") || !korisnik.Email.Contains(".com"))
                return BadRequest();
            if (korisnik.DatumRodjenja == null)
                return BadRequest();

            List<Korisnik> lista = KorisnikCRUD.listaKorisnika;
            Korisnik temp = lista.Find(k => k.KorisnickoIme.Equals(korisnik.KorisnickoIme));

            if (temp != null)
                return BadRequest();
            
            KorisnikCRUD.AddKorisnik(korisnik);
            KorisnikCRUD.SerializeKorisnik();
            return Ok();
        }

        [Route("api/authentificator/login")]
        public IHttpActionResult Login(Korisnik korisnik)
        {
            if (korisnik.KorisnickoIme == null || korisnik.KorisnickoIme == "")
                return BadRequest();
            if (korisnik.Lozinka == null || korisnik.Lozinka == "")
                return BadRequest();   

            List<Korisnik> lista = KorisnikCRUD.listaKorisnika;
            Korisnik temp = lista.Find(k => k.KorisnickoIme.Equals(korisnik.KorisnickoIme) && k.Lozinka.Equals(korisnik.Lozinka));

            if (temp == null)
                return BadRequest();

            if (temp.Blokiran && temp.UlogaKorisnika == Uloga.TRENER)
                return BadRequest();

            return Ok(temp);
        }      
    }
}
