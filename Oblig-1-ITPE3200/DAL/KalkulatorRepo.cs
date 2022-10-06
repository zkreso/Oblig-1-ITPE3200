using Microsoft.EntityFrameworkCore;
using Oblig_1_ITPE3200.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.DAL
{
    public class KalkulatorRepo : IKalkulatorRepo
    {
        private readonly KalkulatorContext _db;

        public KalkulatorRepo (KalkulatorContext db)
        {
            _db = db;
        }

        public async Task<List<SymptomDiagnose>> Index()
        {
            try
            {
                List<SymptomDiagnose> sd = await _db.SymptomDiagnoser.ToListAsync();

                return sd;
            }
            catch
            {
                return null;
            }

        }
    }
}
