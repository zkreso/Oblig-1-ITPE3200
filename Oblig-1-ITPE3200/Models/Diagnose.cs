using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.Models
{
    public class Diagnose
    {
        public int DiagnoseId { get; set; }
        public string DiagnoseNavn { get; set; }
        public string Description { get; set; }
        //public ICollection<SymptomDiagnose> SymptomDiagnoser { get; set; }
        public List<SymptomDiagnose> SymptomDiagnoser { get; set; }

        //public virtual List<Symptom> Symptomer { get; set; }
    }
}
