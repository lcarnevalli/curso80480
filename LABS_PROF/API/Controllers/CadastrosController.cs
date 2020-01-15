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
using API.Models;

namespace API.Controllers
{
    [EnableCors(origins:"*", headers:"*", methods:"*")]
    public class CadastrosController : ApiController
    {
        private APIContext db = new APIContext();

        // GET: api/Cadastros
        public IList<Cadastro> GetCadastroes()
        {
            var lista = db.Cadastroes.ToList();

            return lista;
        }

        // GET: api/Cadastros/5
        [ResponseType(typeof(Cadastro))]
        public IHttpActionResult GetCadastro(int id)
        {
            Cadastro cadastro = db.Cadastroes.Find(id);
            if (cadastro == null)
            {
                return NotFound();
            }

            return Ok(cadastro);
        }

        // PUT: api/Cadastros/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCadastro(int id, Cadastro cadastro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cadastro.Id)
            {
                return BadRequest();
            }

            db.Entry(cadastro).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CadastroExists(id))
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
        [ResponseType(typeof(Cadastro))]
        public IHttpActionResult PostCadastro(Cadastro cadastro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cadastroes.Add(cadastro);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cadastro.Id }, cadastro);
        }

        // DELETE: api/Cadastros/5
        [ResponseType(typeof(Cadastro))]
        public IHttpActionResult DeleteCadastro(int id)
        {
            Cadastro cadastro = db.Cadastroes.Find(id);
            if (cadastro == null)
            {
                return NotFound();
            }

            db.Cadastroes.Remove(cadastro);
            db.SaveChanges();

            return Ok(cadastro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CadastroExists(int id)
        {
            return db.Cadastroes.Count(e => e.Id == id) > 0;
        }
    }
}