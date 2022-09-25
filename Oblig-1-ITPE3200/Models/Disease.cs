using System.Collections.Generic;

namespace Oblig_1_ITPE3200.Models
{
    public class Disease
    {
        public int id { get; set; }
        public string Name { get; set; }
        public virtual List<DiseaseSymptom> DiseaseSymptoms { get; set; }
    }
}
