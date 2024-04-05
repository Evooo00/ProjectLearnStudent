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
    public class ExampleTasks
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Tytuł")]
        public string Title { get; set; }
        [Required]
        [DisplayName("Przykładowe zadania")]
        public string ExampleTask { get; set; }
        [Required]
        [DisplayName("Przykładowe rozwiązanie")]
        public string Solution { get; set; }
        public int SectionId { get; set; }
        [ForeignKey("SectionId")]
        [ValidateNever]
        public Section Section { get; set; }
    }
}
