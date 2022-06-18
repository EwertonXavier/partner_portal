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
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class PartnerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PartnerData/ListPartners
        [HttpGet]
        public IEnumerable<Partner> ListPartners()
        {
            return db.Partners;
        }

        // GET: api/PartnerData/FindPartner/5
        [HttpGet]
        [ResponseType(typeof(Partner))]
        public IHttpActionResult FindPartner(int id)
        {
            Partner partner = db.Partners.Find(id);
            if (partner == null)
            {
                return NotFound();
            }

            return Ok(partner);
        }

        // POST: api/PartnerData/UpdatePartner/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePartner(int id, Partner partner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != partner.PartnerId)
            {
                return BadRequest();
            }

            db.Entry(partner).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartnerExists(id))
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

        // POST: api/PartnerData/AddPartner
        [ResponseType(typeof(Partner))]
        [HttpPost]
        public IHttpActionResult AddPartner(Partner partner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Partners.Add(partner);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = partner.PartnerId }, partner);
        }

        // Post: api/PartnerData/DeletePartner/5
        [ResponseType(typeof(Partner))]
        [HttpPost]
        public IHttpActionResult DeletePartner(int id)
        {
            Partner partner = db.Partners.Find(id);
            if (partner == null)
            {
                return NotFound();
            }

            db.Partners.Remove(partner);
            db.SaveChanges();

            return Ok(partner);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PartnerExists(int id)
        {
            return db.Partners.Count(e => e.PartnerId == id) > 0;
        }
    }
}