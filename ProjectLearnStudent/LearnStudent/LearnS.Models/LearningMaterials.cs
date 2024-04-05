using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnS.Models
{
    public class LearningMaterials
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Tytuł")]
        public string Title { get; set; }
        [Required]
   
        [DisplayName("Opis")]
        public string Description { get; set; }
        [Required]
        [MaxLength(25)]
        [DisplayName("Autor")]
        public string Author { get; set; }
        [DisplayName("Kategoria")]
        public int SectionId { get; set; }
        [ForeignKey("SectionId")]
        [ValidateNever]
        public Section Section { get; set; }
    }
}
