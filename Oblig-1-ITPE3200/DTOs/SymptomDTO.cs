using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using Oblig_1_ITPE3200.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Oblig_1_ITPE3200.DTOs
{
    [ExcludeFromCodeCoverage]
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
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return symptoms;
            }
            return symptoms.Where(s => EF.Functions.Like(s.Name, "%" + searchString + "%"));
        }
        public static IQueryable<SymptomDTO> ExcludeSymptoms(this IQueryable<SymptomDTO> symptoms, List<SymptomDTO> selectedSymptoms)
        {
            if (selectedSymptoms.IsNullOrEmpty())
            {
                return symptoms;
            }
            if (selectedSymptoms.Count() == 0)
            {
                return symptoms;
            }
            return symptoms.Where(s => !selectedSymptoms.Select(s => s.Id).ToArray().Contains(s.Id));
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
    [ExcludeFromCodeCoverage]
    public class SymptomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
