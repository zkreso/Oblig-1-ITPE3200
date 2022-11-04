using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.Models
{
    public class Symptom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DiseaseSymptom> DiseaseSymptoms { get; set; }
    }
}
