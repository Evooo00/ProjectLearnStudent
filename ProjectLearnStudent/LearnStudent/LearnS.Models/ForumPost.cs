using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LearnS.Models
{
    public class ForumPost
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }


        public int ForumThreadId { get; set; }

        [ForeignKey("ForumThreadId")]
        [Required]
        [ValidateNever]
        public ForumThread ForumThread { get; set; }

        [ValidateNever]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }

        [ValidateNever]
        public List<ForumComment> ForumComments { get; set; }

        [ValidateNever]

        public List<ForumRating> ForumRatings { get; set; }
        [ValidateNever]
        public int NumberOfViews { get; set; } = 0;
        [ValidateNever]
        [Display(Name = "Number of Comments")]
        public int NumberOfComments => ForumComments?.Count ?? 0;
    }
}
