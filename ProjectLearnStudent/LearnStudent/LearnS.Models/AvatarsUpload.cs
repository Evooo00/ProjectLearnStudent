using Microsoft.AspNetCore.Http;
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
    public class AvatarsUpload
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string Name {  get; set; }
        [DisplayName("Prześlij zdjęcie")]
        [ValidateNever]
        public string ImageUrl { get; set; }

        [ValidateNever]
        [DisplayName("Wartść monet")]
        public int CoinsValue { get; set; }

        public List<AvatarPurchase> AvatarPurchases { get; set; }
    }
}
