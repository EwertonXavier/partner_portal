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
    public class ArtifactDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ArtifactData/ListArtifacts
        [HttpGet]
        public IEnumerable<ArtifactDto> ListArtifacts()
        {
            List<Artifact> artifacts = db.Artifacts.ToList();
            List<ArtifactDto> artifactsDto = new List<ArtifactDto>();
            artifacts.ForEach(artifact => artifactsDto.Add(new ArtifactDto() {
                Artifact_Id = artifact.Artifact_Id,
                Create_Date = artifact.Create_Date,
                Status = artifact.Status,
                Content = artifact.Content, 
                Customer_Id = artifact.Customer_Id
        }));

            
            return artifactsDto;
        }

        // GET: api/ArtifactData/FindArtifact/5
        [ResponseType(typeof(Artifact))]
        [HttpGet]
        public IHttpActionResult FindArtifact(int id)
        {
            Artifact artifact = db.Artifacts.Find(id);
            if (artifact == null)
            {
                return NotFound();
            }

            return Ok(artifact);
        }

        // PUT: api/ArtifactData/UpdateArtifact/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateArtifact(int id, Artifact artifact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artifact.Artifact_Id)
            {
                return BadRequest();
            }

            db.Entry(artifact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtifactExists(id))
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

        // POST: api/ArtifactData/AddArtifact
        [ResponseType(typeof(Artifact))]
        [HttpPost]
        public IHttpActionResult AddArtifact(Artifact artifact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Artifacts.Add(artifact);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = artifact.Artifact_Id }, artifact);
        }

        // DELETE: api/ArtifactData/DeleteArtifact/5
        [ResponseType(typeof(Artifact))]
        [HttpPost]
        public IHttpActionResult DeleteArtifact(int id)
        {
            Artifact artifact = db.Artifacts.Find(id);
            if (artifact == null)
            {
                return NotFound();
            }

            db.Artifacts.Remove(artifact);
            db.SaveChanges();

            return Ok(artifact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArtifactExists(int id)
        {
            return db.Artifacts.Count(e => e.Artifact_Id == id) > 0;
        }
    }
}