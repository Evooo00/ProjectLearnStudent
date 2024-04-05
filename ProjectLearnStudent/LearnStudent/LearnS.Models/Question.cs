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
    public class Question
    {
        [Key]
        public int Id {  get; set; }
        [Required]
        [DisplayName("Pytanie")]
        public string QuestionText { get; set; }
        [Required]
        [DisplayName("Odpowiedź 1")]
        public string Answer1 { get; set; }
        [Required]
        [DisplayName("Odpowiedź 2")]
        public string Answer2 { get; set; }
        [Required]
        [DisplayName("Odpowiedź 3")]
        public string Answer3 { get; set; }
        [Required]
        [DisplayName("Odpowiedź 4")]
        public string Answer4 { get; set; }
        public int IsCorrect { get; set; }

        [DisplayName("Quiz")]
        public int QuizId { get; set; }
        [ForeignKey("QuizId")]
        [ValidateNever]
        public Quiz Quiz { get; set; }



    }
}
