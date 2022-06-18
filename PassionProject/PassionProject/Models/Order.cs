using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PassionProject.Models
{

    /// <summary>
    /// Order is created by customers and handed to a Partner. Partners can then assign one of their consultants to fulfill the order.
    /// </summary>
    public class Order
    {
        [Key]
        public int Order_Id { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime Create_Date { get; set; }
        public decimal Price { get; set; }

        //partner which receives order
        public int? PartnerId { get; set; }
        [ForeignKey("PartnerId")]
        public virtual Partner Partner { get; set; }

        //consultant who is going to fulfil the order
        //An order is executed by one consultant
        public int? Consultant_Id { get; set; }
        [ForeignKey("Consultant_Id")]
        public virtual Consultant Consultant { get; set; }

        //Artifact target of the order
        public int Artifact_Id { get; set; }
        [ForeignKey("Artifact_Id")]
        public virtual Artifact Artifact { get; set; }

    }

    public class OrderDto
    {
        public int Order_Id { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime Create_Date { get; set; }
        public decimal Price { get; set; }
        public int? PartnerId { get; set; }
        public int? Consultant_Id { get; set; }
        public int Artifact_Id { get; set; }

    }
}