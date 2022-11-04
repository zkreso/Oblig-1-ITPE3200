using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oblig_1_ITPE3200.DAL;
using Oblig_1_ITPE3200.DTOs;
using Oblig_1_ITPE3200.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.Controllers
{
    [Route("[controller]/[action]")]
    public class ObligController : ControllerBase
    {
        private readonly IObligRepository _db;

        private const string _loggedOn = "loggedOn";

        public ObligController(IObligRepository db)
        {
            _db = db;
        }

        // Disease CRUD
        public async Task<DiseaseDTO> GetDisease(int id)
        {
            return await _db.GetDisease(id);
        }
        public async Task<List<DiseaseDTO>> GetAllDiseases(string searchString)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggedOn)))
            {
                return Unauthorized();
            }

            List<DiseaseDTO> diseaseDTO = await _db.GetAllDiseases(searchString);
            
        }
        public async Task<bool> CreateDisease(Disease disease)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggedOn)))
            {
                return Unauthorized();
            }

            return await _db.CreateDisease(disease);
        }

        public async Task<bool> UpdateDisease(Disease disease)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggedOn)))
            {
                return Unauthorized();
            }

            return await _db.UpdateDisease(disease);
        }
        public async Task<bool> DeleteDisease(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggedOn)))
            {
                return Unauthorized();
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

        public async Task<ActionResult> LogIn(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool b = await _db.LogIn(user);
                    if (!b)
                    {
                        //_log.LogInformation("Log in failed with user=" + user.Username);
                        HttpContext.Session.SetString(_loggedOn, "");
                        return Ok(false);
                    }
                    HttpContext.Session.SetString(_loggedOn, "LoggedOn");
                    return Ok(true);
                }
                //_log.LogInformation("Something wrong in inputvalidation, user=" + user.Username);
                return BadRequest("Something wrong in inputvalidation");
            }
            catch (Exception e)
            {
                //_log.LogInformation(e.Message);
                return BadRequest("Something wrong on server.");
            }
        }

        public bool LogOut()
        {
            try
            {
                HttpContext.Session.SetString(_loggedOn, "");
                return true;
            }
            catch (Exception e)
            {
                //_log.LogInformation(e.Message);
                return false;
            }
        }

        public ActionResult IsLoggedIn()
        {
            try
            {
                if (HttpContext.Session.GetString(_loggedOn) == "LoggedOn")
                {
                    return Ok(true);
                }
                return Ok(false);
            }
            catch (Exception e)
            {
                //_log.LogInformation(e.Message);
                return BadRequest("Something wrong with server. Try again later.");
            }
        }
    }
}
