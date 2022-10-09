using Microsoft.AspNetCore.Mvc;
using Oblig_1_ITPE3200.DAL;
using Oblig_1_ITPE3200.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.Controllers
{
    [Route("[controller]/[action]")]
    public class KalkulatorController : ControllerBase
    {
        private readonly IKalkulatorRepo _db;

        public KalkulatorController(IKalkulatorRepo db)
        {
            _db = db;
        }

        public async Task<List<SymptomDiagnose>> GetSymptomDiagnoser()
        {
            
            return await _db.GetSymptomDiagnoser();
        }

        public async Task<List<Symptom>> GetSymptomer()
        {
            return await _db.GetSymptomer();
        }

        public async Task<List<Diagnose>> GetDiagnoser(Symptom symptom)
         {
             return await _db.GetDiagnoser(symptom);
         }
    }
}
