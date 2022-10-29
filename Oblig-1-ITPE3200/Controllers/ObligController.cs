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

        public async Task<List<Disease>> GetAllDiseases()
        {
            return await _db.GetAllDiseases();
        }

        public async Task<List<Symptom>> GetAllSymptoms()
        {
            return await _db.GetAllSymptoms();
        }

        public async Task<Disease> GetDisease(int id)
        {
            return await _db.GetDisease(id);
        }

        public async Task<List<Symptom>> GetSymptomsDisease(int id)
        {
            return await _db.GetSymptomsDisease(id);

        }

        public async Task<Symptom> GetSymptom(int id)
        {
            return await _db.GetSymptom(id);
        }

        public async Task<bool> DeleteDisease(int id)
        {
            return await _db.DeleteDisease(id);
        }

        public async Task<bool> ChangeDisease(Disease d, List<Symptom> s)
        {
            return await _db.ChangeDisease(d, s);
        }

        public async Task<Disease> FindMatchingDisease (List<int> ids)
        {
            return await _db.FindMatchingDisease(ids);
        }

    }
}
