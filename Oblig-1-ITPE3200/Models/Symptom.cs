using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.Models
{
    public class Symptom
    {
        public int SymptomId { get; set; }
        public string SymptomNavn { get; set; }

        //[JsonIgnore]
        public virtual List<SymptomDiagnose> SymptomDiagnoser { get; set; }

    }
}
