using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnS.Models
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Tytuł")]
        [Required]
        public string Title { get; set; }
        
        [DisplayName("Dział")]
        public int SectionId { get; set; }
        [ForeignKey("SectionId")]
        [ValidateNever]
        public Section Section { get; set; }

        [ValidateNever]
        [DisplayName("Liczba punktów")]
        public int Points { get; set; }
        [ValidateNever]
        [DisplayName("Liczba monet")]
        public int Coins { get; set; }
        [ValidateNever]
        public List<Question> Questions { get; set; }
    }
}
