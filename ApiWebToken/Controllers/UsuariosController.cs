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
using ApiWebToken.Services;

namespace ApiWebToken.Controllers
{
    [Authorize]
    [RoutePrefix("api/Usuarios")]
    public class UsuariosController : ApiController
    {
        private MusicAppEntities db = new MusicAppEntities();

        // GET: api/Usuarios
        [HttpGet]
        public IEnumerable<Usuario> GetUsuarios()
        {
            return db.Usuarios.ToList();
        }

        // GET: api/Usuarios/5
        [HttpGet]
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetUsuario(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/Usuarios/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(int id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.ID)
            {
                return BadRequest();
            }

            //usuario.ID = id;
            usuario.password = EncryptedSHA256.SHA256Encrypt(usuario.password);
            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        [HttpPost]
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            usuario.password = EncryptedSHA256.SHA256Encrypt(usuario.password);
            db.Usuarios.Add(usuario);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = usuario.ID }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete]
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult DeleteUsuario(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.Usuarios.Remove(usuario);
            db.SaveChanges();

            return Ok(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(int id)
        {
            return db.Usuarios.Count(e => e.ID == id) > 0;
        }
    }
}