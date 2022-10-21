using Microsoft.EntityFrameworkCore;
using Oblig_1_ITPE3200.Models;
using Oblig_1_ITPE3200.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Oblig_1_ITPE3200.DAL
{
    public class KalkulatorRepo : IKalkulatorRepo
    {
        private readonly KalkulatorContext _db;

        public KalkulatorRepo (KalkulatorContext db)
        {
            _db = db;
        }

        public async Task<List<DiagnoseModel>> GetDiagnoser()
        {
            try
            {
                List<DiagnoseModel> dm = await _db.Diagnoser.MapToDiagnoseModel().ToListAsync();

                return dm;
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

        public async Task<List<DiagnoseModel>> SearchDiagnoser(string[] symptomArray)
        {
            try
            {
                List<DiagnoseModel> diagnoser = await _db.Diagnoser.MapToDiagnoseModel().ToListAsync();
                diagnoser = diagnoser.Where(d => d.SymptomNavnene.Any(sn => symptomArray.Contains(sn))).ToList();
                return diagnoser;

                //Console.WriteLine("Hello world");  
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> SlettEnDiagnoser(int diagnoseId)
        {
            try
            {
                var diagnose = await _db.Diagnoser.FindAsync(diagnoseId);
                Console.WriteLine("1: " + diagnose.DiagnoseNavn);
                _db.Diagnoser.Remove(diagnose);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<DiagnoseModel>> GetEnDiagnose(int diagnoseId)
        {
            try
            {
                List<DiagnoseModel> diagnoser = await _db.Diagnoser.MapToDiagnoseModel().ToListAsync();
                diagnoser = diagnoser.Where(d => d.DiagnoseId == diagnoseId).ToList();
                //var diagnose = await _db.Diagnoser.FindAsync(diagnoseId);
                //List<string> symptomNavn = diagnose.SymptomDiagnoser.Select(sd => sd.Symptom.SymptomNavn).ToList();
                return diagnoser;
            }
            catch
            {
                return null;
            }
        }
    }
}