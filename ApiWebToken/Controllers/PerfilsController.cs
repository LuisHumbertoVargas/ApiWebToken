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
    [RoutePrefix("api/Perfils")]
    public class PerfilsController : ApiController
    {
        private MusicAppEntities db = new MusicAppEntities();

        // GET: api/Perfils
        [HttpGet]
        public IEnumerable<Perfil> GetPerfils()
        {
            return db.Perfils.ToList();
        }

        // GET: api/Perfils/5
        [HttpGet]
        [ResponseType(typeof(Perfil))]
        public IHttpActionResult GetPerfil(int id)
        {
            Perfil perfil = db.Perfils.Find(id);
            if (perfil == null)
            {
                return NotFound();
            }

            return Ok(perfil);
        }

        // PUT: api/Perfils/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPerfil(int id, Perfil perfil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != perfil.perfilID)
            {
                return BadRequest();
            }

            db.Entry(perfil).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfilExists(id))
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

        // POST: api/Perfils
        [HttpPost]
        [ResponseType(typeof(Perfil))]
        // [FromBody]Perfil perfil
        public IHttpActionResult PostPerfil(Perfil perfil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Perfils.Add(perfil);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = perfil.perfilID }, perfil);
        }

        // DELETE: api/Perfils/5
        [HttpDelete]
        [ResponseType(typeof(Perfil))]
        public IHttpActionResult DeletePerfil(int id)
        {
            Perfil perfil = db.Perfils.Find(id);
            if (perfil == null)
            {
                return NotFound();
            }

            db.Perfils.Remove(perfil);
            db.SaveChanges();

            return Ok(perfil);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PerfilExists(int id)
        {
            return db.Perfils.Count(e => e.perfilID == id) > 0;
        }
    }
}