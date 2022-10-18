using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oblig_1_ITPE3200.Models
{
    public class DiseaseSymptom
    {
        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }
        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; }
    }
}
