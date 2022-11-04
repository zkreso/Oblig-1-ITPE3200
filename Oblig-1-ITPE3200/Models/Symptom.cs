using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.Models
{
    public class Symptom
    {
        [RegularExpression(@"^[1-9]([0-9]){0,5}$")] // 6 or less digits natural number
        public int Id { get; set; }
        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-\']{0,80}")]
        public string Name { get; set; }
        public List<DiseaseSymptom> DiseaseSymptoms { get; set; }
    }
}
