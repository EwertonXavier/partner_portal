using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PassionProject.Models
{
    /// <summary>
    /// Customer is a person who uses Steps2Canada services. They can create orders and artifacts.
    /// </summary>
    public class Customer
    {
        [Key]
        public int Customer_Id { get; set; }
        public string Customer_Name { get; set; }
    }
}