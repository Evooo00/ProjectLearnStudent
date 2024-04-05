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
    public class ForumThread
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Tytuł")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Zawartość")]
        public string Content { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [ValidateNever]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }
        [ValidateNever]
        public int NumberOfViews { get; set; }


        [ValidateNever]
        public List<ForumPost> ForumPosts { get; set; }
        [ValidateNever]
        public int ReplyCount { get; set; }
    }
}
