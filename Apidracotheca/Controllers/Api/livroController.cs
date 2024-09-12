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
using Apidracotheca.Models;

namespace Apidracotheca.Controllers.Api
{
    public class livroController : ApiController
    {
        private Banco db = new Banco();

        // GET: api/livro
        public IQueryable<livro> Getlivro()
        {
            return db.livro;
        }

        // GET: api/livro/5
        [ResponseType(typeof(livro))]
        public IHttpActionResult Getlivro(int id)
        {
            livro livro = db.livro.Find(id);
            if (livro == null)
            {
                return NotFound();
            }

            return Ok(livro);
        }

        // PUT: api/livro/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlivro(int id, livro livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != livro.tombo)
            {
                return BadRequest();
            }

            db.Entry(livro).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!livroExists(id))
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

        // POST: api/livro
        [ResponseType(typeof(livro))]
        public IHttpActionResult Postlivro(livro livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.livro.Add(livro);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = livro.tombo }, livro);
        }

        // DELETE: api/livro/5
        [ResponseType(typeof(livro))]
        public IHttpActionResult Deletelivro(int id)
        {
            livro livro = db.livro.Find(id);
            if (livro == null)
            {
                return NotFound();
            }

            db.livro.Remove(livro);
            db.SaveChanges();

            return Ok(livro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool livroExists(int id)
        {
            return db.livro.Count(e => e.tombo == id) > 0;
        }
    }
}