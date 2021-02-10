using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PretrazivacCasopisa.Models
{
    public class Clanci
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage ="Naziv članka ne smije biti dulji od 100 znakova")]
        public string Naslov { get; set; }
        [Required]
        public int CasopisID { get; set; }
        [ForeignKey("CasopisID")]
        public virtual Casopis Casopis2 { get; set; }
        [Required]
        public int broj { get; set; }
        [Required]
        public int AutorID { get; set; }
        [ForeignKey("AutorID")]
        public virtual Autori Autor { get; set; }
    }
}