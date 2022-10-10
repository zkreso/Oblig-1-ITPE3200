using System;
namespace Oblig_1_ITPE3200.Models
{
    public class Sykdomssymptom
    {
        public int ID { get; set; }
        public int SykdomId { get; set; }
        public int SymptomId { get; set; }
        public virtual Sykdom Sykdom { get; set; }
        public virtual Symptom Symptom { get; set; }
    }
}

