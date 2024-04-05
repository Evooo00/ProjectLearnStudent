using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnS.Models
{
    public class ApplicationUser : IdentityUser
    {
        [ValidateNever]
        public List<ForumPost> ForumPost { get; set; }
        [ValidateNever]
        public List<ForumThread> ForumThreads { get; set; }

        [ValidateNever]
        public int Points { get; set; }
        [ValidateNever]
        public int Coins { get; set; }
        [ValidateNever]
        [Required]
        [DisplayName("Poziom")]
        public int Level { get; set; }
        [ValidateNever]
        public List<AvatarPurchase> AvatarPurchases { get; set; }
      
        [ValidateNever]
        public List<AvatarPurchase> PurchasedAvatars { get; set; }
        
        public void CalculateLevel()
        {

            Level = Points / 200 + 1;
        }
    }
}
