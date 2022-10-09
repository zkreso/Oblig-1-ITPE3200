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

        public async Task<List<SymptomDiagnose>> GetSymptomDiagnoser()
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

        public async Task<List<Symptom>> GetSymptomer()
        {
            try
            {
                List<Symptom> s = await _db.Symptomer.ToListAsync();
                return s;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Diagnose>> GetDiagnoser(Symptom symptom)
        {
            try
            {
                List<Diagnose> diagnoser = await _db.Diagnoser.ToListAsync();
                diagnoser = diagnoser.Where(d => d.SymptomDiagnoser
                                .Select(sd => sd.DiagnoseId)
                                .Contains(symptom.SymptomId))
                                .ToList();
                    

                //System.Console.WriteLine(symptomDiagnoseList.Count);

                return diagnoser;
            }
            catch
            {
                return null;
            }
        }
    }
}
