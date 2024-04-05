using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnS.Models
{
    public class AvatarPurchase
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


        [ForeignKey("AvatarId")]
        public int AvatarId { get; set; }
        public AvatarsUpload Avatar { get; set; }
        public DateTime PurchaseDate { get; set; }


    }
}
