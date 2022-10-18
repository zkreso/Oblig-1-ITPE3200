using Microsoft.AspNetCore.Mvc;
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

        public ObligController(IObligRepository db)
        {
            _db = db;
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

        public async Task<bool> DeleteDisease(int id)
        {
            return await _db.DeleteDisease(id);
        }

        public async Task<bool> ChangeDisease(Disease d, List<Symptom> s)
        {
            return await _db.ChangeDisease(d, s);
        }

        public async Task<Disease> FindMatchingDisease (List<Symptom> sList)
        {
            return await _db.FindMatchingDisease(sList);
        }

    }
}
