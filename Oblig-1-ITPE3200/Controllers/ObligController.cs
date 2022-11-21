using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oblig_1_ITPE3200.DAL;
using Oblig_1_ITPE3200.DTOs;
using Oblig_1_ITPE3200.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Route("[controller]/[action]")]
    public class ObligController : ControllerBase
    {
        private readonly IObligRepository _db;

        private const string _loggedOn = "loggedOn";
        
        private readonly ILogger<ObligController> _log;

        public ObligController(IObligRepository db, ILogger<ObligController> log)
        {
            _db = db;
            _log = log;
        }

        // Disease CRUD

        [HttpGet, Route("GetDisease/{id}")]
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

        [HttpGet, Route("GetAllDiseases/{searchString}")]
        public async Task<ActionResult> GetAllDiseases(string searchString)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggedOn)))
            {
                return Unauthorized();
            }
            if (!ModelState.IsValid)
            {
                _log.LogInformation("Feil i inputvalidering");
                return BadRequest("Feil i inputvalidering");
            }
            List<DiseaseDTO> diseaseDTOList = await _db.GetAllDiseases(searchString);
            if(diseaseDTOList == null)
            {
                _log.LogInformation("Don't find the disease list");
                return NotFound("Don't find the disease list");
            }
            return Ok(diseaseDTOList);
        }

        [HttpPost, Route("CreateDisease")]
        public async Task<ActionResult> CreateDisease(Disease disease)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggedOn)))
            {
                return Unauthorized();
            }
            if (!ModelState.IsValid)
            {
                _log.LogInformation("Feil i inputvalidering");
                return BadRequest("Feil i inputvalidering");
            }
            
            bool returOK = await _db.CreateDisease(disease);

            if (!returOK)
            {
                _log.LogInformation("Disease is not created");
                return BadRequest("Disease is not created");
            }
            return Ok("Disease is created");
        }

        [HttpPut, Route("UpdateDisease")]
        public async Task<ActionResult> UpdateDisease(Disease disease)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggedOn)))
            {
                return Unauthorized();
            }
            if (!ModelState.IsValid)
            {
                _log.LogInformation("Feil i inputvalidering");
                return BadRequest("Feil i inputvalidering");
            }
            
            bool returOK = await _db.UpdateDisease(disease);
            if(!returOK)
            {
                _log.LogInformation("Disease is not updated");
                return BadRequest("Disease is not updated");
            }
            return Ok("Disease is updated");
        }

        [HttpDelete, Route("DeleteDisease/{id}")]
        public async Task<ActionResult> DeleteDisease(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggedOn)))
            {
                return Unauthorized();
            }
            if (!ModelState.IsValid)
            {
                _log.LogInformation("Feil i inputvalidering");
                return BadRequest("Feil i inputvalidering");
            }
            
            bool returOK = await _db.DeleteDisease(id);
            if(!returOK)
            {
                _log.LogInformation("Disease is not deleted");
                return BadRequest("Disease is not deleted");
            }
            return Ok("Disease is deleted");
        }

        // Symptom CRUD

        [HttpGet, Route("GetAllSymptoms")]
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

        [HttpGet, Route("GetSymptomsTable")]
        public async Task<ActionResult> GetSymptomsTable(SymptomsTableOptions options)
        {
            if (!ModelState.IsValid)
            {
                _log.LogInformation("Feil i inputvalidering");
                return BadRequest("Feil i inputvalidering");
            }
            
            SymptomsTable symptomsTable = await _db.GetSymptomsTable(options);
            if(symptomsTable == null)
            {
                _log.LogInformation("Don't find symptom table");
                return NotFound("Don't find symptom table");
            }
            return Ok(symptomsTable);
        }

        [HttpGet, Route("GetRelatedSymptoms/{id}")]
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
        [HttpGet, Route("SearchDisease/{symptomIds}")]
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

        // Login functions
        // [HttpGet, Route("LogIn/{user})"] cant get this to work
        [HttpPost, Route("LogIn")]
        public async Task<ActionResult> LogIn(User user)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    bool b = await _db.LogIn(user);
                    if (!b)
                    {
                        _log.LogInformation("Log in failed with user=" + user.Username);
                        HttpContext.Session.SetString(_loggedOn, "");
                        return Ok(false);
                    }
                    HttpContext.Session.SetString(_loggedOn, "LoggedOn");
                    return Ok(true);
                }
                _log.LogInformation("Something wrong in inputvalidation, user=" + user.Username);
                return BadRequest("Something wrong in inputvalidation");
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return BadRequest("Something wrong on server.");
            }
        }

        [HttpGet, Route("LogOut")]
        public ActionResult LogOut()
        {
            try
            {
                HttpContext.Session.SetString(_loggedOn, "");
                return Ok(true);
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return Ok(false);
            }
        }

        [HttpGet, Route("IsLoggedIn")]
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
                _log.LogInformation(e.Message);
                return BadRequest("Something wrong with server. Try again later.");
            }
        }
    }
}
