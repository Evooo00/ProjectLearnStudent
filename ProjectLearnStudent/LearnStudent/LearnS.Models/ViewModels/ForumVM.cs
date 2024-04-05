using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnS.Models.ViewModels
{
    public class ForumVM
    {
        [ValidateNever]
        public ForumThread ForumThread { get; set; }
        [ValidateNever]
        public List<ForumPost> ForumPosts { get; set; }
    }
}
