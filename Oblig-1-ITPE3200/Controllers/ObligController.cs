using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oblig_1_ITPE3200.DAL;
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

        private ILogger<ObligController> _log;
        private const string _loggedOn = "loggedOn";

        public ObligController(IObligRepository db, ILogger<ObligController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> GetAllDiseases()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggedOn)))
            {
                return Unauthorized();
            }
            List<Disease> diseases = await _db.GetAllDiseases();
            return Ok(diseases);
        }

        public async Task<ActionResult> GetAllSymptoms()
        {
            List<Symptom> symptoms = await _db.GetAllSymptoms();
            return Ok(symptoms);
        }

        public async Task<ActionResult> GetDisease(int id)
        {
            Disease disease = await _db.GetDisease(id);

            if (disease == null)
            {
                _log.LogInformation("Disease with id=" + id + " was not found.");
                return NotFound("Disease not found");
            }
            return Ok(disease);
        }


        // Not needed, remove when dobble checked
        public async Task<List<Symptom>> GetSymptomsDisease(int id)
        {
            return await _db.GetSymptomsDisease(id);

        }

        public async Task<ActionResult> GetSymptom(int id)
        {
            Symptom symptom = await _db.GetSymptom(id);

            if (symptom == null)
            {
                _log.LogInformation("Symptom with id=" + id + " was not found.");
                return NotFound("Symptom was not found");
            }
            return Ok(symptom);
        }

        public async Task<ActionResult> DeleteDisease(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggedOn)))
            {
                return Unauthorized();
            }
            bool b = await _db.DeleteDisease(id);
            
            if (!b)
            {
                _log.LogInformation("Got in id=" + id + ". Disease was not deleted");
                return NotFound("Disease was not deleted");
            }
            return Ok("Disease deleted");
        }

        public async Task<ActionResult> ChangeDisease(Disease d, List<Symptom> s)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggedOn)))
            {
                return Unauthorized();
            }
            bool b = await _db.ChangeDisease(d, s);

            if (!b)
            {
                _log.LogInformation("Disease with id=" + d.Id + " was not changed.");
                return BadRequest("Disease was not changed");
            }
            return Ok("Disease changed");
        }

        public async Task<ActionResult> FindMatchingDisease (List<int> ids)
        {
            Disease disease = await _db.FindMatchingDisease(ids);
            
            if (disease == null)
            {
                _log.LogInformation("Did not find matching disease with IDs.");
                return NotFound("Did not find matching disease with symptom IDs");
            }
            return Ok(disease);
        }

        public async Task<ActionResult> LogIn (User user)
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

        // Should be ActionResult
        public bool LogOut()
        {
            try
            {
                HttpContext.Session.SetString(_loggedOn, "");
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
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
                _log.LogInformation(e.Message);
                return BadRequest("Something wrong with server. Try again later.");
            }
        }

    }
}
