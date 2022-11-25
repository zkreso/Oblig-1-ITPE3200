using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Oblig_1_ITPE3200.Models
{
    [ExcludeFromCodeCoverage]
    public class DiseaseSymptom
    {
        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }
        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; }
    }
}
