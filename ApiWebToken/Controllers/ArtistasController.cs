using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ApiWebToken.Models;

namespace ApiWebToken.Controllers
{
    [Authorize]
    [RoutePrefix("api/Artista")]
    public class ArtistasController : ApiController
    {
        private MusicAppEntities db = new MusicAppEntities();

        // GET: api/Artistas
        [HttpGet]
        public IQueryable<Artista> GetArtistas()
        {
            return db.Artistas;
        }

        // GET: api/Artistas/5
        [HttpGet]
        [ResponseType(typeof(Artista))]
        public IHttpActionResult GetArtista(int id)
        {
            Artista artista = db.Artistas.Find(id);
            if (artista == null)
            {
                return NotFound();
            }

            return Ok(artista);
        }

        // PUT: api/Artistas/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArtista(int id, Artista artista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artista.artistaID)
            {
                return BadRequest();
            }

            db.Entry(artista).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Artistas
        [HttpPost]
        [ResponseType(typeof(Artista))]
        public IHttpActionResult PostArtista(Artista artista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Artistas.Add(artista);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = artista.artistaID }, artista);
        }

        // DELETE: api/Artistas/5
        [HttpDelete]
        [ResponseType(typeof(Artista))]
        public IHttpActionResult DeleteArtista(int id)
        {
            Artista artista = db.Artistas.Find(id);
            if (artista == null)
            {
                return NotFound();
            }

            db.Artistas.Remove(artista);
            db.SaveChanges();

            return Ok(artista);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArtistaExists(int id)
        {
            return db.Artistas.Count(e => e.artistaID == id) > 0;
        }
    }
}