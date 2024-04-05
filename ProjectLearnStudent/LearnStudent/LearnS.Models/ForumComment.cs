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
    public class ForumComment
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Zawartość")]
        public string Content { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }


        public int ForumPostId { get; set; }
        [ForeignKey("ForumPostId")]
        [ValidateNever]
        public ForumPost ForumPost { get; set; }

      

        [ForeignKey("UserId")]
        [ValidateNever]
        public string UserId { get; set; }
        [ValidateNever]
        public ApplicationUser User { get; set; }
    }
}

