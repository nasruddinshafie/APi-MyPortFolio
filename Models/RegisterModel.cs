using System.ComponentModel.DataAnnotations;

namespace MyPortFolio.Models
{
    public class RegisterModel
    {
        [Required]
        public string username { get; set; }
        [Required]
        [StringLength(8, MinimumLength=4)]
        public string password { get; set; }
    }
}
