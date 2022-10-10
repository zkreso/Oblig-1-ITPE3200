using Oblig_1_ITPE3200.Models;
using System.Linq;

namespace Oblig_1_ITPE3200.DTOs
{
    // Code of extensions class adapted from Finch, J.P. (2021), Entity Framework Core in Action (2nd edition). Manning
    public static class DiseaseDTOExtensions
    {
        public static IQueryable<DiseaseDTO> MapDiseaseToDTO (this IQueryable<Disease> diseases)
        {
            return diseases.Select(d => new DiseaseDTO
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Symptoms = d.DiseaseSymptoms.Select(ds => ds.Symptom.Name).ToArray()
            });
        }
    }
    public class DiseaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Symptoms { get; set; }
    }
}
