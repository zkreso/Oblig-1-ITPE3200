using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oblig_1_ITPE3200.DAL;
using Oblig_1_ITPE3200.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.Controllers
{
    [Route("[controller]/[action]")]
    public class ObligController : ControllerBase
    {
        private readonly IObligRepository _db;

        private ILogger<ObligController> _log;

        public ObligController(IObligRepository db, ILogger<ObligController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> GetAllDiseases()
        {
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

    }
}
