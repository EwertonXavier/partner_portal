using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProject.Models
{
    /// <summary>
    /// Consultant is a person who work for a Partner. He is responsible for executing orders.
    /// </summary>
    public class Consultant
    {
        //general information
        [Key]
        public int Consultant_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Address { get; set; }
        public string Mobile_Number { get; set; }
        public string Email { get; set; }

        //job information
        public decimal Wage { get; set; }
        public DateTime Hire_date { get; set; }
        public string Status { get; set; }

        //consultant works for a partner
        public int PartnerId { get; set; }
        [ForeignKey("PartnerId")]
        public virtual Partner Partner { get; set; }

        //a consultant can execute many orders
        public ICollection<Order> orders { get; set; }
    }

    public class ConsultantDto{
        public int Consultant_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Address { get; set; }
        public string Mobile_Number { get; set; }
        public string Email { get; set; }

        //job information
        public decimal Wage { get; set; }
        public DateTime Hire_date { get; set; }
        public string Status { get; set; }

        public int PartnerId { get; set; }
    }
}