using System.Diagnostics.CodeAnalysis;

namespace Oblig_1_ITPE3200.Models
{
    [ExcludeFromCodeCoverage]
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
    }
}
