using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using Oblig_1_ITPE3200.Models;
using System.Linq;

namespace Oblig_1_ITPE3200.DTOs
{
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
        public static IQueryable<SymptomDTO> FilterBySearchString(this IQueryable<SymptomDTO> symptoms, string searchString)
        {
            if (searchString.IsNullOrEmpty())
            {
                return symptoms;
            }
            return symptoms.Where(s => EF.Functions.Like(s.Name, "%" + searchString + "%"));
        }
        public static IQueryable<SymptomDTO> ExcludeSymptomsById(this IQueryable<SymptomDTO> symptoms, int[] symptomIds)
        {
            if (symptomIds.IsNullOrEmpty())
            {
                return symptoms;
            }
            if (symptomIds.Length == 0)
            {
                return symptoms;
            }
            return symptoms.Where(s => !symptomIds.Contains(s.Id));
        }
        public static IQueryable<SymptomDTO> OrderSymptomsBy(this IQueryable<SymptomDTO> symptoms, string orderBy)
        {
            switch (orderBy)
            {
                case "idAscending":
                    return symptoms.OrderBy(s => s.Id);
                case "idDescending":
                    return symptoms.OrderByDescending(s => s.Id);
                case "nameAscending":
                    return symptoms.OrderBy(s => s.Name);
                case "nameDescending":
                    return symptoms.OrderByDescending(s => s.Name);
                default:
                    return symptoms.OrderBy(s => s.Id);
            }
        }
    }
    public class SymptomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
