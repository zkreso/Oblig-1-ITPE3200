using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
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
        public static IQueryable<SymptomDTO> FilterBySearchString(this IQueryable<SymptomDTO> symptoms, string searchString)
        {
            if (searchString.IsNullOrEmpty())
            {
                return symptoms;
            }
            return symptoms.Where(s => EF.Functions.Like(s.Name, "%" + searchString + "%"));
        }
        public static IQueryable<SymptomDTO> ExcludeSymptomsById(this IQueryable<SymptomDTO> symptoms, int[] symptomsArray)
        {
            if (symptomsArray.Length == 0)
            {
                return symptoms;
            }
            return symptoms.Where(s => !symptomsArray.Contains(s.Id));
        }
    }
    public class SymptomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
