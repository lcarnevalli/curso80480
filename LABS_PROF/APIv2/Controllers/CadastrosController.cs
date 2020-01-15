using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using APIv2.Models;

namespace APIv2.Controllers
{
    [EnableCors(origins:"*", headers:"*", methods:"*")]
    public class CadastrosController : ApiController
    {
        private APIv2Context db = new APIv2Context();

        // GET: api/Cadastros
        public IQueryable<Cadastros> GetCadastros()
        {
            System.Threading.Thread.Sleep(5000);

            return db.Cadastros;
        }

        // GET: api/Cadastros/5
        [ResponseType(typeof(Cadastros))]
        public IHttpActionResult GetCadastros(int id)
        {
            Cadastros cadastros = db.Cadastros.Find(id);
            if (cadastros == null)
            {
                return NotFound();
            }

            return Ok(cadastros);
        }

        // PUT: api/Cadastros/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCadastros(int id, Cadastros cadastros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cadastros.Id)
            {
                return BadRequest();
            }

            db.Entry(cadastros).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CadastrosExists(id))
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

        // POST: api/Cadastros
        [ResponseType(typeof(Cadastros))]
        public IHttpActionResult PostCadastros(Cadastros cadastros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cadastros.Add(cadastros);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cadastros.Id }, cadastros);
        }

        // DELETE: api/Cadastros/5
        [ResponseType(typeof(Cadastros))]
        public IHttpActionResult DeleteCadastros(int id)
        {
            Cadastros cadastros = db.Cadastros.Find(id);
            if (cadastros == null)
            {
                return NotFound();
            }

            db.Cadastros.Remove(cadastros);
            db.SaveChanges();

            return Ok(cadastros);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CadastrosExists(int id)
        {
            return db.Cadastros.Count(e => e.Id == id) > 0;
        }
    }
}