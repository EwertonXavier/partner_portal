using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PassionProject.Models
{
    /// <summary>Partner is an institution that hires consultors.
    /// They will partner with Steps2 Canada to provide some consulting services to our customers. Properties: id(int) and name (string)</summary>
    public class Partner
    {
        [Key] //Annotation to indicate this is the primary key of Partners table
        // Properties of an Partner:
        public int PartnerId { get; set; }
        public string Name { get; set; }
        // You have to use Nugets package manager to run
        // 1)enable-migrations: once per project
        // 2) add-migration partner: once per new snapshot
        // 3)update-database: whenever you make changes to your model


    }
}