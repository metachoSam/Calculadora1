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
using DATOS;

namespace NEGOCIO.Controllers
{
    public class resultadosController : ApiController
    {
        private calcEntities db = new calcEntities();

        // GET: api/resultados
        public IQueryable<resultados> Getresultados()
        {
            return db.resultados;
        }

        // GET: api/resultados/5
        [ResponseType(typeof(resultados))]
        public IHttpActionResult Getresultados(int id)
        {
            resultados resultados = db.resultados.Find(id);
            if (resultados == null)
            {
                return NotFound();
            }

            return Ok(resultados);
        }

        // POST: api/resultados
        [ResponseType(typeof(resultados))]
        public IHttpActionResult Postresultados(resultados resultados)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.resultados.Add(resultados);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = resultados.Id }, resultados);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool resultadosExists(int id)
        {
            return db.resultados.Count(e => e.Id == id) > 0;
        }
    }
}