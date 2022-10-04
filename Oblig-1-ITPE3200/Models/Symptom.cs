using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oblig_1_ITPE3200.Models
{
    public class Symptom
    {
        [Key]
        public int SymptomId { get; set; }
        public string Name { get; set; }
        public virtual List<DiseaseSymptom> DiseaseSymptoms { get; set; }
    }
}
