using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Oblig_1_ITPE3200.DTOs
{
    [ExcludeFromCodeCoverage]
    public class UserDTO
    {
        public int Id { get; set; }
        [RegularExpression(@"^[A-Za-zÆØÅæøå. ]{2,20}$")]
        public string Username { get; set; }
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$")]
        public string Password { get; set; }
    }
}

