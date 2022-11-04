using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oblig_1_ITPE3200.Models
{
    public class Disease
    {
        [RegularExpression(@"^[1-9]([0-9]){0,5}$")] // 6 or less digits natural number
        public int Id { get; set; }
        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-\']{0,80}")]
        public string Name { get; set; }
        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-\'\!\?]{0,999}")]
        public string Description { get; set; }
        public List<DiseaseSymptom> DiseaseSymptoms { get; set; }
    }
}
