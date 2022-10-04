using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oblig_1_ITPE3200.Models
{
    public class Symptom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual List<DiseaseSymptom> DiseaseSymptoms { get; set; }
        [NotMapped]
        public List<Disease> Diseases { get; set; }
    }
}
