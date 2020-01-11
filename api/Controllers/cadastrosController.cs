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
using api.Models;

namespace api.Controllers
{
    
    [EnableCors(origins:"*", headers:"*", methods:"*")]
    public class cadastrosController : ApiController
    {
        private apiContext db = new apiContext();


        // GET: api/cadastros
        public IQueryable<cadastro> Getcadastroes()
        {
            //piorar o serviço esperar 5 segundos
            System.Threading.Thread.Sleep(5000);
            return db.cadastroes;
        }

        // GET: api/cadastros/5
        [ResponseType(typeof(cadastro))]
        public IHttpActionResult Getcadastro(int id)
        {
            //piorar o serviço esperar 5 segundos
            System.Threading.Thread.Sleep(5000);

            cadastro cadastro = db.cadastroes.Find(id);
            if (cadastro == null)
            {
                return NotFound();
            }

            return Ok(cadastro);
        }

        // PUT: api/cadastros/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcadastro(int id, cadastro cadastro)
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
                if (!cadastroExists(id))
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

        // POST: api/cadastros
        [ResponseType(typeof(cadastro))]
        public IHttpActionResult Postcadastro(cadastro cadastro)
        {
            if (!ModelState.IsValid)
            {
                //parar por 5 segundos
                //System.Threading.Thread.Sleep(5000);

                return BadRequest(ModelState);
            }

            db.cadastroes.Add(cadastro);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cadastro.Id }, cadastro);
        }

        // DELETE: api/cadastros/5
        [ResponseType(typeof(cadastro))]
        public IHttpActionResult Deletecadastro(int id)
        {
            cadastro cadastro = db.cadastroes.Find(id);
            if (cadastro == null)
            {
                return NotFound();
            }

            db.cadastroes.Remove(cadastro);
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

        private bool cadastroExists(int id)
        {
            return db.cadastroes.Count(e => e.Id == id) > 0;
        }
    }
}