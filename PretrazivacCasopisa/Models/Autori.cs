using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PretrazivacCasopisa.Models
{
    public class Autori
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage="Ime i prezime ne smiju biti veći od 50 znakova.")]
        public string ImeIPrezime { get; set; }
    }
}