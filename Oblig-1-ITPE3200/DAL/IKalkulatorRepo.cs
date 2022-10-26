using Oblig_1_ITPE3200.Models;
using Oblig_1_ITPE3200.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.DAL
{
    public interface IKalkulatorRepo
    {       
        Task<List<DiagnoseModel>> GetDiagnoser();
        Task<List<SymptomModel>> GetSymptomer();

        Task<List<DiagnoseModel>> SearchDiagnoser(string[] symptomArray);

        Task<bool> SlettEnDiagnoser(int diagnoseId);

        Task<DiagnoseModel> GetEnDiagnose(int diagnoseId);

        Task<bool> UpdateDescription(int diagnoseId, string description);

        Task<List<SymptomModel>> GetRelevantSymptoms(int diagnoseId);

        Task<List<SymptomModel>> GetIrrelevantSymptoms(int diagnoseId);

        Task<bool> RemoveSymptomDiagnose(SymptomDiagnose symptomDiagnose);

        Task<bool> AddSymptomDiagnose(SymptomDiagnose symptomDiagnose);
            
        Task<bool> AddNewSymptom(string symptomNavn, int diagnoseId);

        Task<DiagnoseModel> CreateEnDiagnose(Diagnose diagnose);
    }
}
