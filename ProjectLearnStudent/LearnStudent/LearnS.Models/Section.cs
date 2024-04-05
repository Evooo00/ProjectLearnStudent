using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LearnS.Models
{
    public class Section
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Tytuł")]
        public string Title { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Opis")]
        public string Description { get; set; }
        [Required]
        [MaxLength(25)]
        [DisplayName("Autor")]
        public string Author { get; set; }
        [DisplayName("Kategoria")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
    }
}
