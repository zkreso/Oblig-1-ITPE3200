using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Oblig_1_ITPE3200.DTOs
{
    [ExcludeFromCodeCoverage]
    public class SymptomsTable
    {
        public SymptomsTable(SymptomsTableOptions pageData, List<SymptomDTO> symptomList)
        {
            PageData = pageData;
            SymptomList = symptomList;
        }
        public SymptomsTableOptions PageData { get; private set; }
        public List<SymptomDTO> SymptomList { get; private set; }
    }
}
