using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PretrazivacCasopisa.Models
{
    public class Casopis
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage ="Naziv ne smije biti veći od 50 znakova.")]
        public string Naziv { get; set; }
    }
}