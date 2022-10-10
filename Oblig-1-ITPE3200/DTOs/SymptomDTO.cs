using Oblig_1_ITPE3200.Models;
using System.Linq;

namespace Oblig_1_ITPE3200.DTOs
{
    // Code of extensions class adapted from Finch, J.P. (2021), Entity Framework Core in Action (2nd edition). Manning
    public static class SymptomDTOExtensions
    {
        public static IQueryable<SymptomDTO> MapSymptomToDTO (this IQueryable<Symptom> symptoms)
        {
            return symptoms.Select(s => new SymptomDTO
            {
                Id = s.Id,
                Name = s.Name
            });
        }
    }
    public class SymptomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
