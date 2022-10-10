using System;
using System.Collections.Generic;

namespace Oblig_1_ITPE3200.Models
{
    public class Symptom
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public virtual List<Sykdom> SykdomListe { get; set; }
        public virtual List<Sykdomssymptom> SykdomssymptomListe { get; set; }
    }
}