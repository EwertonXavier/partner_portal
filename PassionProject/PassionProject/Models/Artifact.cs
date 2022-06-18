using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProject.Models
{
    /// <summary>
    /// Artifacts are objects created by customers. It can be letter, aplications, forms
    /// and other documents required by Canadian Government during Visa Process.
    /// </summary>
    public class Artifact
    {
        // Artifact properties
        [Key]
        public int Artifact_Id { get; set; }
        public DateTime Create_Date { get; set; }
        public string Status { get; set; }
        public string Content { get; set; } //I would like this property to be a JSON in the future.

        //who was the creator of the Artifact
        public int Customer_Id { get; set; }
        [ForeignKey("Customer_Id")]
        public virtual Customer Customer { get; set; }
    }
    public class ArtifactDto
    {
        // Artifact properties
        [Key]
        public int Artifact_Id { get; set; }
        public DateTime Create_Date { get; set; }
        public string Status { get; set; }
        public string Content { get; set; } //I would like this property to be a JSON in the future.

        //who was the creator of the Artifact
        public int Customer_Id { get; set; }

    }
}