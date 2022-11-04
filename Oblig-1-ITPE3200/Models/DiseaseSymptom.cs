using System.ComponentModel.DataAnnotations;

namespace Oblig_1_ITPE3200.Models
{
    public class DiseaseSymptom
    {
        [RegularExpression(@"^[1-9]([0-9]){0,5}$")]
        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }
        [RegularExpression(@"^[1-9]([0-9]){0,5}$")]
        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; }
    }
}
