using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Oblig_1_ITPE3200.Models
{
    [ExcludeFromCodeCoverage]
    public class Disease
    {
        public int Id { get; set; }
        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-\']{0,80}")]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DiseaseSymptom> DiseaseSymptoms { get; set; }
    }
}
