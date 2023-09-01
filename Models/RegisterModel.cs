using System.ComponentModel.DataAnnotations;

namespace MyPortFolio.Models
{
    public class RegisterModel
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
