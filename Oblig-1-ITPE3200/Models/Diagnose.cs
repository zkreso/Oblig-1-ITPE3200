using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.Models
{
    public class Diagnose
    {
        public int DiagnoseId { get; set; }
        public string DiagnoseNavn { get; set; }
        public string Description { get; set; }
        //public ICollection<SymptomDiagnose> SymptomDiagnoser { get; set; }

        [JsonIgnore]
        public virtual List<SymptomDiagnose> SymptomDiagnoser { get; set; }
    }
}
