using System.Collections.Generic;

namespace Oblig_1_ITPE3200.DTOs
{
    public class SymptomPage
    {
        public SymptomPage(SymptomPageOptions pageData, List<SymptomDTO> symptomList)
        {
            PageData = pageData;
            SymptomList = symptomList;
        }
        public SymptomPageOptions PageData { get; private set; }
        public List<SymptomDTO> SymptomList { get; private set; }
    }
}
