using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Oblig_1_ITPE3200.Models
{
    [ExcludeFromCodeCoverage]
    public class Symptom
    {
        public int Id { get; set; }
        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-\']{0,80}")]
        public string Name { get; set; }
        public List<DiseaseSymptom> DiseaseSymptoms { get; set; }
    }
}
