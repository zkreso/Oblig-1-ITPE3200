using System.ComponentModel.DataAnnotations;

namespace Oblig_1_ITPE3200.Models
{
    public class User
    {
        [RegularExpression(@"^[A-Za-zÆØÅæøå. ]{2,20}$")]
        public string Username { get; set; }
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$")]
        public string Password { get; set; }
    }
}
