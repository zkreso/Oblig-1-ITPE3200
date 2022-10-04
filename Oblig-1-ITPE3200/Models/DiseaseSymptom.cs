using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oblig_1_ITPE3200.Models
{
    public class DiseaseSymptom
    {
        public int DiseaseId { get; set; }
        [ForeignKey("DiseaseId")]
        public virtual Disease Disease { get; set; }
        public int SymptomId { get; set; }
        [ForeignKey("SymptomId")]
        public virtual Symptom Symptom { get; set; }
    }
}
