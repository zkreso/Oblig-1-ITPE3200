namespace Oblig_1_ITPE3200.Models
{
    public class DiseaseSymptom
    {
        public int id { get; set; }
        public virtual Disease Disease { get; set; }
        public virtual Symptom Symptom { get; set; }
    }
}
