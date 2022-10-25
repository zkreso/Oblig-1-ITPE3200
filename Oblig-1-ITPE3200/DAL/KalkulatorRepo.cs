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
                _db.Diagnoser.Remove(diagnose);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //public async Task<List<DiagnoseModel>> GetEnDiagnose(int diagnoseId)
        public async Task<DiagnoseModel> GetEnDiagnose(int diagnoseId)
        {
            try
            {
                var diagnose = await _db.Diagnoser.MapToDiagnoseModel().SingleAsync(d => d.DiagnoseId == diagnoseId);
                
                //List<DiagnoseModel> diagnoser = await _db.Diagnoser.MapToDiagnoseModel().ToListAsync();
                //diagnoser = diagnoser.Where(d => d.DiagnoseId == diagnoseId).ToList();
                //var diagnose = await _db.Diagnoser.FindAsync(diagnoseId);
                //List<string> symptomNavn = diagnose.SymptomDiagnoser.Select(sd => sd.Symptom.SymptomNavn).ToList();
                return diagnose;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateSymptomer(string[] symptomList)
        {
            try
            {
                int id = int.Parse(symptomList[0]);
                var diagnoseModel = await _db.Diagnoser.MapToDiagnoseModel().SingleAsync(d => d.DiagnoseId == id);
                var diagnose = await _db.Diagnoser.FindAsync(id);
                //var diagnose = await _db.Diagnoser.MapToDianoseModel().FindAsync(int.Parse(symptomList[0]));
                List<SymptomDiagnose> symptomDiagnoser = diagnose.SymptomDiagnoser.Where(sd => sd.DiagnoseId==diagnose.DiagnoseId).ToList();
                
                List<Symptom> symptomer = symptomDiagnoser.Select(sd => sd.Symptom).ToList();



                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}