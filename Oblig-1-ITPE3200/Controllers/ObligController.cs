using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Oblig_1_ITPE3200.DAL;
using Oblig_1_ITPE3200.DTOs;
using Oblig_1_ITPE3200.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.Controllers
{
    [Route("[controller]/[action]")]
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

        public async Task<ActionResult> GetDisease(
            [FromQuery]
            [RegularExpression(@"^[1-9][0-9]*$")]
            int id
            )
        {
            if (!ModelState.IsValid)
            {
                _log.LogInformation("Feil i inputvalidering");
                return BadRequest("Feil i inputvalidering");
            }
            DiseaseDTO diseaseDTO = await _db.GetDisease(id);
            if(diseaseDTO == null)
            {
                _log.LogInformation("Disease was not found");
                return NotFound("Disease was not found");
            }
            return Ok(diseaseDTO);
        }

        public async Task<ActionResult> GetAllDiseases(
            [RegularExpression(@"[a-zA-ZÊ¯Â∆ÿ≈0-9\\\'\(\)-. ]*")]
            string searchString
            )
        {
            // I don't want this to require login
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggedOn)))
            //{
            //    return Unauthorized();
            //}
            if (!ModelState.IsValid)
            {
                _log.LogInformation("Feil i inputvalidering");
                return BadRequest("Feil i inputvalidering");
            }
            List<DiseaseDTO> diseaseDTOList = await _db.GetAllDiseases(searchString);
            if(diseaseDTOList == null)
            {
                _log.LogInformation("Couldn't get disease list");
                return new ObjectResult("Couldn't get disease list")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            return Ok(diseaseDTOList);
        }

        public async Task<ActionResult> CreateDisease([FromBody] Disease disease)
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
            
            DiseaseDTO createdDisease = await _db.CreateDisease(disease);

            if (createdDisease == null)
            {
                _log.LogInformation("Couldn't create disease");
                return new ObjectResult("Couldn't create disease")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            var actionName = nameof(GetDisease);
            var routeValues = new { id = createdDisease.Id };
            return CreatedAtAction(actionName, routeValues, createdDisease);
        }

        public async Task<ActionResult> UpdateDisease([FromBody] Disease disease)
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
                _log.LogInformation("Couldn't update disease");
                return new ObjectResult("Couldn't update disease")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            return Ok();
        }

        public async Task<ActionResult> DeleteDisease(
            [FromQuery]
            [RegularExpression(@"^[1-9][0-9]*$")]
            int id)
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
                _log.LogInformation("Couldn't delete disease");
                return new ObjectResult("Couldn't delete disease")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            return NoContent();
        }

        // Symptom CRUD

        public async Task<ActionResult> GetAllSymptoms()
        {
            List<SymptomDTO> symptomDTOList = await _db.GetAllSymptoms();
            if(symptomDTOList == null)
            {
                _log.LogInformation("Couldn't get symptoms list");
                return new ObjectResult("Couldn't get symptoms list")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            return Ok(symptomDTOList);
        }

        public async Task<ActionResult> GetSymptomsTable([FromBody]SymptomsTableOptions options)
        {
            if (!ModelState.IsValid)
            {
                _log.LogInformation("Feil i inputvalidering");
                return BadRequest("Feil i inputvalidering");
            }
            
            SymptomsTable symptomsTable = await _db.GetSymptomsTable(options);
            if(symptomsTable == null)
            {
                _log.LogInformation("Couldn't get symptoms table");
                return new ObjectResult("Couldn't get symptoms table")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            return Ok(symptomsTable);
        }

        public async Task<ActionResult> GetRelatedSymptoms([RegularExpression(@"^[1-9][0-9]*$")] int id)
        {
            if (!ModelState.IsValid)
            {
                _log.LogInformation("Feil i inputvalidering");
                return BadRequest("Feil i inputvalidering");
            }

            List<SymptomDTO> symptomDTOs = await _db.GetRelatedSymptoms(id);
            if(symptomDTOs == null)
            {
                _log.LogInformation("Couldn't get related symptoms");
                return new ObjectResult("Couldn't get related symptoms")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
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
        public async Task<ActionResult> SearchDiseases([FromBody] List<Symptom> selectedSymptoms)
        {
            List<DiseaseDTO> diseaseDTOs = await _db.SearchDiseases(selectedSymptoms);
            if(diseaseDTOs == null)
            {
                if (!ModelState.IsValid)
                {
                    _log.LogInformation("Feil i inputvalidering");
                    return BadRequest("Feil i inputvalidering");
                }

                _log.LogInformation("Couldn't get disease list");
                return new ObjectResult("Couldn't get disease list")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            return Ok(diseaseDTOs);
        }

        // Login functions
        
        public async Task<ActionResult> LogIn([FromBody] UserDTO user)
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
                    HttpContext.Session.SetString(_loggedOn, "loggedOn");
                    return Ok(true);
                }
                _log.LogInformation("Something wrong in inputvalidation, user=" + user.Username);
                return BadRequest("Feil i inputvalidering");
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return new ObjectResult("Couldn't verify log in information")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ActionResult> LogOut()
        {
            try
            {
                HttpContext.Session.SetString(_loggedOn, "");
                return Ok();
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return new ObjectResult("Something went wrong when logging out")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ActionResult> IsLoggedIn()
        {
            try
            {
                if (HttpContext.Session.GetString(_loggedOn) == "loggedOn")
                {
                    return Ok(true);
                }
                return Ok(false);
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return new ObjectResult("Couldn't check login status")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
