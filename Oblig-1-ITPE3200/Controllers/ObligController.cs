using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        private readonly ILogger<ObligController> _log;

        public ObligController(IObligRepository db, ILogger<ObligController> log)
        {
            _db = db;
            _log = log;
        }

        // Disease CRUD
        /**public async Task<DiseaseDTO> GetDisease(int id)
        {
            return await _db.GetDisease(id);
        }**/

        public async Task<ActionResult> GetDisease(int id)
        {
            DiseaseDTO diseaseDTO = await _db.GetDisease(id);
            if(diseaseDTO == null)
            {
                _log.LogInformation("Don't find the disease");
                return NotFound("Don't find the disease");
            }
            return Ok(diseaseDTO);
        }

        /**public async Task<List<DiseaseDTO>> GetAllDiseases(string searchString)
        {
            return await _db.GetAllDiseases(searchString);
        }**/

        public async Task<ActionResult> GetAllDiseases(string searchString)
        {
            List<DiseaseDTO> diseaseDTOList = await _db.GetAllDiseases(searchString);
            if(diseaseDTOList == null)
            {
                _log.LogInformation("Don't find the disease list");
                return NotFound("Don't find the disease list");
            }
            return Ok(diseaseDTOList);
        }

        /**public async Task<bool> CreateDisease(Disease disease)
        {
            return await _db.CreateDisease(disease);
        }**/

        public async Task<ActionResult> CreateDisease(Disease disease)
        {
            bool returOK = await _db.CreateDisease(disease);

            if (!returOK)
            {
                _log.LogInformation("Disease is not created");
                return BadRequest("Disease is not created");
            }
            return Ok("Disease is created");
        }

        /**public async Task<bool> UpdateDisease(Disease disease)
        {
            return await _db.UpdateDisease(disease);
        }**/

        public async Task<ActionResult> UpdateDisease(Disease disease)
        {
            bool returOK = await _db.UpdateDisease(disease);
            if(!returOK)
            {
                _log.LogInformation("Disease is not updated");
                return BadRequest("Disease is not updated");
            }
            return Ok("Disease is updated");
        }

        /**public async Task<bool> DeleteDisease(int id)
        {
            return await _db.DeleteDisease(id);
        }**/

        public async Task<ActionResult> DeleteDisease(int id)
        {
            bool returOK = await _db.DeleteDisease(id);
            if(!returOK)
            {
                _log.LogInformation("Disease is not deleted");
                return BadRequest("Disease is not deleted");
            }
            return Ok("Disease is deleted");
        }

        // Symptom CRUD
        /**public async Task<List<SymptomDTO>> GetAllSymptoms()
        {
            return await _db.GetAllSymptoms();
        }**/

        public async Task<ActionResult> GetAllSymptoms()
        {
            List<SymptomDTO> symptomDTOList = await _db.GetAllSymptoms();
            if(symptomDTOList == null)
            {
                _log.LogInformation("Don't find symptom list");
                return NotFound("Don't find symptom list");
            }
            return Ok(symptomDTOList);
        }

        /**public async Task<SymptomsTable> GetSymptomsTable(SymptomsTableOptions options)
        {
            return await _db.GetSymptomsTable(options);
        }**/

        public async Task<ActionResult> GetSymptomsTable(SymptomsTableOptions options)
        {
            SymptomsTable symptomsTable = await _db.GetSymptomsTable(options);
            if(symptomsTable == null)
            {
                _log.LogInformation("Don't find symptom table");
                return NotFound("Don't find symptom table");
            }
            return Ok(symptomsTable);
        }

        /**public async Task<List<SymptomDTO>> GetRelatedSymptoms(int id)
        {
            return await _db.GetRelatedSymptoms(id);
        }**/

        public async Task<ActionResult> GetRelatedSymptoms(int id)
        {
            List<SymptomDTO> symptomDTOs = await _db.GetRelatedSymptoms(id);
            if(symptomDTOs == null)
            {
                _log.LogInformation("Don't find symptom list");
                return NotFound("Don't find symptom list");
            }
            return Ok(symptomDTOs);
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
        /**public async Task<List<DiseaseDTO>> SearchDiseases(int[] symptomIds)
        {
            return await _db.SearchDiseases(symptomIds);
        }**/

        public async Task<ActionResult> SearchDiseases(int[] symptomIds)
        {
            List<DiseaseDTO> diseaseDTOs = await _db.SearchDiseases(symptomIds);
            if(diseaseDTOs == null)
            {
                _log.LogInformation("Don't find disease list");
                return NotFound("Don't find disease list");
            }
            return Ok(diseaseDTOs);
        }
    }
}
