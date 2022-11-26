using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using Oblig_1_ITPE3200.Models;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Oblig_1_ITPE3200.DTOs
{
    [ExcludeFromCodeCoverage]
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
        public static IQueryable<DiseaseDTO> FilterBySearchString(this IQueryable<DiseaseDTO> diseases, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return diseases;
            }
            return diseases.Where(d => EF.Functions.Like(d.Name, "%" + searchString + "%"));
        }
    }
    [ExcludeFromCodeCoverage]
    public class DiseaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Symptoms { get; set; }
    }
}
