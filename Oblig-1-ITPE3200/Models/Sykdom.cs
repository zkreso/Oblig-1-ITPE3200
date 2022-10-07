using System;
using System.Collections.Generic;

namespace Oblig_1_ITPE3200.Models
{
    public class Sykdom
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public virtual List<Symptom> SymptomListe { get; set; }
    }
}

