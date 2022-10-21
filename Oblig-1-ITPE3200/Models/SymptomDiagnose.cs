using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.Models
{
    public class SymptomDiagnose
    {
        public int SymptomId { get; set; }
        public virtual Symptom Symptom { get; set; }
        public int DiagnoseId { get; set; }
        public virtual Diagnose Diagnose { get; set; }
    }
}
