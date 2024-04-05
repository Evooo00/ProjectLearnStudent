using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearnS.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Nazwa kategorii")]
        public string Name { get; set; }
        [DisplayName("Kolejność wyświetlania")]
        [Range(1,100,ErrorMessage= "Kolejność wyświetlania musi zawierać się w przedziale 1-100")]
        public int DisplayOrder { get; set; }
    }
}
