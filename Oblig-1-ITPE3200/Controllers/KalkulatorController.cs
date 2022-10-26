using Microsoft.AspNetCore.Mvc;
using Oblig_1_ITPE3200.DAL;
using Oblig_1_ITPE3200.Models;
using Oblig_1_ITPE3200.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.Controllers
{
    [Route("[controller]/[action]")]
    public class KalkulatorController : ControllerBase
    {
        private readonly IKalkulatorRepo _db;

        public KalkulatorController(IKalkulatorRepo db)
        {
            _db = db;
        }

        public async Task<List<DiagnoseModel>> GetDiagnoser()
        {
            
            return await _db.GetDiagnoser();
        }

        public async Task<List<SymptomModel>> GetSymptomer()
        {
            return await _db.GetSymptomer();
        }

        public async Task<List<DiagnoseModel>> SearchDiagnoser(string[] symptomArray)
         {
             return await _db.SearchDiagnoser(symptomArray);
         }

        public async Task<bool> SlettEnDiagnoser(int diagnoseId)
        {
            return await _db.SlettEnDiagnoser(diagnoseId);
        }

        public async Task<DiagnoseModel> GetEnDiagnose(int diagnoseId)
        {
            return await _db.GetEnDiagnose(diagnoseId);
        }

        public async Task<bool> UpdateDescription(int diagnoseId, string description)
        {
            return await _db.UpdateDescription(diagnoseId, description);
        }

        public async Task<List<SymptomModel>> GetRelevantSymptoms(int diagnoseId)
        {
            return await _db.GetRelevantSymptoms(diagnoseId);
        }

        public async Task<List<SymptomModel>> GetIrrelevantSymptoms(int diagnoseId)
        {
            return await _db.GetIrrelevantSymptoms(diagnoseId);
        }

        public async Task<bool> RemoveSymptomDiagnose(SymptomDiagnose symptomDiagnose)
        {
            return await _db.RemoveSymptomDiagnose(symptomDiagnose);
        }

        public async Task<bool> AddSymptomDiagnose(SymptomDiagnose symptomDiagnose)
        {
            return await _db.AddSymptomDiagnose(symptomDiagnose);
        }

        public async Task<bool> AddNewSymptom(string symptomNavn, int diagnoseId)
        {
            return await _db.AddNewSymptom(symptomNavn, diagnoseId);
        }

        public async Task<DiagnoseModel> CreateEnDiagnose(Diagnose diagnose)
        {
            return await _db.CreateEnDiagnose(diagnose);
        }
    }
}
