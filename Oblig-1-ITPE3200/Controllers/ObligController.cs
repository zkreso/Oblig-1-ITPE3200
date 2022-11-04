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
            if (!ModelState.IsValid)
            {
                // _log.LogInformation("Feil i inputvalidering");
                // return BadRequest("Feil i inputvalidering");
            }
            return await _db.GetDisease(id);
        }
        public async Task<List<DiseaseDTO>> GetAllDiseases(string searchString)
        {
            return await _db.GetAllDiseases(searchString);
        }
        public async Task<bool> CreateDisease(Disease disease)
        {
            if (!ModelState.IsValid)
            {
                // _log.LogInformation("Feil i inputvalidering");
                // return BadRequest("Feil i inputvalidering");
            }
            return await _db.CreateDisease(disease);
        }

        public async Task<bool> UpdateDisease(Disease disease)
        {
            if (!ModelState.IsValid)
            {
                // _log.LogInformation("Feil i inputvalidering");
                // return BadRequest("Feil i inputvalidering");
            }
            return await _db.UpdateDisease(disease);
        }
        public async Task<bool> DeleteDisease(int id)
        {
            if (!ModelState.IsValid)
            {
                // _log.LogInformation("Feil i inputvalidering");
                // return BadRequest("Feil i inputvalidering");
            }
            return await _db.DeleteDisease(id);
        }

        // Symptom CRUD
        public async Task<List<SymptomDTO>> GetAllSymptoms()
        {
            return await _db.GetAllSymptoms();
        }
        public async Task<SymptomsTable> GetSymptomsTable(SymptomsTableOptions options)
        {
            if (!ModelState.IsValid)
            {
                // _log.LogInformation("Feil i inputvalidering");
                // return BadRequest("Feil i inputvalidering");
            }
            return await _db.GetSymptomsTable(options);
        }
        public async Task<List<SymptomDTO>> GetRelatedSymptoms(int id)
        {
            return await _db.GetRelatedSymptoms(id);
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
        
        // Joining table methods
        public async Task<bool> CreateDiseaseSymptom(int diseaseId, int symptomId)
        {
            return await _db.CreateDiseaseSymptom(diseaseId, symptomId);
        }
        public async Task<bool> DeleteDiseaseSymptom(int diseaseId, int symptomId)
        {
            return await _db.DeleteDiseaseSymptom(diseaseId, symptomId);
        }
        */

        // Search method
        public async Task<List<DiseaseDTO>> SearchDiseases(int[] symptomIds)
        {
            return await _db.SearchDiseases(symptomIds);
        }
    }
}
