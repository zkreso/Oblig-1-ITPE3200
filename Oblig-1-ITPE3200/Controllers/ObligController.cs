using Microsoft.AspNetCore.Mvc;
using Oblig_1_ITPE3200.DAL;
using Oblig_1_ITPE3200.DTOs;
using Oblig_1_ITPE3200.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.Controllers
{
    [Route("[controller]/[action]")]
    public class ObligController : ControllerBase
    {
        private readonly IObligRepository _db;

        public ObligController(IObligRepository db)
        {
            _db = db;
        }

        // Disease CRUD
        public async Task<DiseaseDTO> GetDisease(int id)
        {
            return await _db.GetDisease(id);
        }
        public async Task<List<DiseaseListDTO>> GetAllDiseases()
        {
            return await _db.GetAllDiseases();
        }
        public async Task<bool> CreateDisease(Disease disease)
        {
            return await _db.CreateDisease(disease);
        }

        public async Task<bool> UpdateDisease(Disease disease)
        {
            return await _db.UpdateDisease(disease);
        }
        public async Task<bool> DeleteDisease(int id)
        {
            return await _db.DeleteDisease(id);
        }

        // Symptom CRUD
        public async Task<List<SymptomDTO>> GetAllSymptoms()
        {
            return await _db.GetAllSymptoms();
        }
        public async Task<List<SymptomDTO>> GetRelatedSymptoms(int id)
        {
            return await _db.GetRelatedSymptoms(id);
        }
        public async Task<List<SymptomDTO>> GetUnrelatedSymptoms(int id)
        {
            return await _db.GetUnrelatedSymptoms(id);
        }
        /* Unused/unneccessary methods
        public async Task<Symptom> GetSymptom(int id)
        {
            return await _db.GetSymptom(id);
        }
        public async Task<bool> CreateSymptom(Symptom symptom)
        {
            return await _db.CreateSymptom(symptom);
        }
        public async Task<bool> UpdateSymptom(Symptom symptom)
        {
            return await _db.UpdateSymptom(symptom);
        }
        public async Task<bool> DeleteSymptom(int id)
        {
            return await _db.DeleteSymptom(id);
        }
        */

        // Joining table methods
        public async Task<bool> CreateDiseaseSymptom(int diseaseId, int symptomId)
        {
            return await _db.CreateDiseaseSymptom(diseaseId, symptomId);
        }
        public async Task<bool> DeleteDiseaseSymptom(int diseaseId, int symptomId)
        {
            return await _db.DeleteDiseaseSymptom(diseaseId, symptomId);
        }

        // Search method
        public async Task<List<DiseaseDTO>> SearchDiseases(int[] symptomsArray)
        {
            return await _db.SearchDiseases(symptomsArray);
        }
    }
}
