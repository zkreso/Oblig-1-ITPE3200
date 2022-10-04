﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.Models
{
    public class Symptom
    {
        public int SymptomId { get; set; }
        public string SymptomNavn { get; set; }

        //public ICollection<SymptomDiagnose> SymptomDiagnoser { get; set; }
        public List<SymptomDiagnose> SymptomDiagnoser { get; set; }

        //public virtual List<Diagnose> Diagnoser { get; set; }
    }
}
