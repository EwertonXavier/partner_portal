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
    public class ConsultantDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ConsultantData/ListConsultants
        [HttpGet]
        public IEnumerable<ConsultantDto> ListConsultants()
        {
            List<ConsultantDto> consultantsDtos = new List<ConsultantDto>();
            List<Consultant> consultants = db.Consultants.ToList();
            consultants.ForEach(c => consultantsDtos.Add(new ConsultantDto()
            {
                Consultant_Id = c.Consultant_Id,
                First_Name = c.First_Name,
                Last_Name = c.Last_Name,
                Address = c.Address,
                Mobile_Number = c.Mobile_Number,
                Email = c.Email,
                Wage = c.Wage,
                Hire_date = c.Hire_date,
                Status = c.Status,
                PartnerId = c.PartnerId
            }));
            return consultantsDtos;
        }

        // GET: api/ConsultantData/FindConsultant/5
        [ResponseType(typeof(ConsultantDto))]
        [HttpGet]
        public IHttpActionResult FindConsultant(int id)
        {
            Consultant consultant = db.Consultants.Find(id);
            if (consultant == null)
            {
                return NotFound();
            }
            ConsultantDto consultantDto = new ConsultantDto() {
                Consultant_Id = consultant.Consultant_Id,
                First_Name = consultant.First_Name,
                Last_Name = consultant.Last_Name,
                Address = consultant.Address,
                Mobile_Number = consultant.Mobile_Number,
                Email = consultant.Email,
                Wage = consultant.Wage,
                Hire_date = consultant.Hire_date,
                Status = consultant.Status,
                PartnerId = consultant.PartnerId
            };

            return Ok(consultantDto);
        }

        // PUT: api/ConsultantData/UpdateConsultant/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateConsultant(int id, Consultant consultant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != consultant.Consultant_Id)
            {
                return BadRequest();
            }

            db.Entry(consultant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultantExists(id))
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

        // POST: api/ConsultantData/AddConsultant
        [ResponseType(typeof(Consultant))]
        [HttpPost]
        public IHttpActionResult AddConsultant(Consultant consultant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Consultants.Add(consultant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = consultant.Consultant_Id }, consultant);
        }

        // DELETE: api/ConsultantData/DeleteConsultant/5
        [ResponseType(typeof(Consultant))]
        [HttpPost]
        public IHttpActionResult DeleteConsultant(int id)
        {
            Consultant consultant = db.Consultants.Find(id);
            if (consultant == null)
            {
                return NotFound();
            }

            db.Consultants.Remove(consultant);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConsultantExists(int id)
        {
            return db.Consultants.Count(e => e.Consultant_Id == id) > 0;
        }
    }
}