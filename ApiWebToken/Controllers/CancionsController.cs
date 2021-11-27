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
    [RoutePrefix("api/Cancions")]
    public class CancionsController : ApiController
    {
        private MusicAppEntities db = new MusicAppEntities();

        // GET: api/Cancions
        [HttpGet]
        public IQueryable<Cancion> GetCancions()
        {
            return db.Cancions;
        }

        // GET: api/Cancions/5
        [HttpGet]
        [ResponseType(typeof(Cancion))]
        public IHttpActionResult GetCancion(int id)
        {
            Cancion cancion = db.Cancions.Find(id);
            if (cancion == null)
            {
                return NotFound();
            }

            return Ok(cancion);
        }

        // PUT: api/Cancions/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCancion(int id, Cancion cancion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cancion.cancionID)
            {
                return BadRequest();
            }

            db.Entry(cancion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CancionExists(id))
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

        // POST: api/Cancions
        [HttpPost]
        [ResponseType(typeof(Cancion))]
        public IHttpActionResult PostCancion(Cancion cancion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cancions.Add(cancion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cancion.cancionID }, cancion);
        }

        // DELETE: api/Cancions/5
        [HttpDelete]
        [ResponseType(typeof(Cancion))]
        public IHttpActionResult DeleteCancion(int id)
        {
            Cancion cancion = db.Cancions.Find(id);
            if (cancion == null)
            {
                return NotFound();
            }

            db.Cancions.Remove(cancion);
            db.SaveChanges();

            return Ok(cancion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CancionExists(int id)
        {
            return db.Cancions.Count(e => e.cancionID == id) > 0;
        }
    }
}