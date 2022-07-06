using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyWebApp.Controllers
{
    public class HelperController : ApiController
    {
        public GrupniTrening Get(string id)
        {
            return GrupniTreningCRUD.findById(id);            
        }

        public IHttpActionResult Post([FromBody]VlasnikKomentar temp)
        {
            Komentar komentar = KomentarCRUD.getById(temp.IdKomentar);
            Korisnik vlasnik = KorisnikCRUD.findByNaziv(temp.IdVlasnik);

            if (vlasnik == null)
                return BadRequest();
            if (komentar == null)
                return BadRequest();
            if (komentar.Odobren)
                return BadRequest();

            //DA LI JE VLASNIK TOG FITNES CENTRA KOJEG ODOBRAVA KOMENTAR
            bool jeste = false;
            foreach (FitnesCentar item in vlasnik.FitnesCentri)
            {
                if (item.Id == komentar.FitnesCentar.Id)
                    jeste = true;
            }

            if (!jeste)
                return BadRequest();

            komentar.Odobren = temp.Odobren;
            KomentarCRUD.SerializeKomentar();

            return Ok();
        }
    }
}
