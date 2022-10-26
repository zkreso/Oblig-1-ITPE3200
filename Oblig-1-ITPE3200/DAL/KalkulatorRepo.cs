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

        public async Task<List<SymptomModel>> GetSymptomer()
        {
            try
            {
                List<SymptomModel> s = await _db.Symptomer.MapToSymptomModel().ToListAsync();
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

        public async Task<DiagnoseModel> GetEnDiagnose(int diagnoseId)
        {
            try
            {
                var diagnose = await _db.Diagnoser.MapToDiagnoseModel().SingleAsync(d => d.DiagnoseId == diagnoseId);
                
                return diagnose;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateDescription(int diagnoseId, string description)
        {
            try
            {
                var diagnose = await _db.Diagnoser.FindAsync(diagnoseId);
                diagnose.Description = description;
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<SymptomModel>> GetRelevantSymptoms(int diagnoseId)
        {
            try
            {
                List<SymptomModel> symptoms = await _db.SymptomDiagnoser
                                                .Where(sd => sd.DiagnoseId == diagnoseId)
                                                .Select(sd => sd.Symptom)
                                                .MapToSymptomModel()
                                                .ToListAsync();
                return symptoms;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<SymptomModel>> GetIrrelevantSymptoms(int diagnoseId)
        {
            try
            {
                List<SymptomModel> symptoms = await _db.Symptomer
                                                .Where(s => !s.SymptomDiagnoser.Select(sd => sd.DiagnoseId).Contains(diagnoseId))
                                                .MapToSymptomModel()
                                                .ToListAsync();
                return symptoms;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> RemoveSymptomDiagnose(SymptomDiagnose symptomDiagnose)
        {
            try
            {
                _db.SymptomDiagnoser.Remove(symptomDiagnose);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddSymptomDiagnose(SymptomDiagnose symptomDiagnose)
        {
            try
            {
                await _db.SymptomDiagnoser.AddAsync(symptomDiagnose);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddNewSymptom(string symptomNavn, int diagnoseId)
        {
            try
            {
                var newSymptom = new Symptom { SymptomNavn = symptomNavn };
                var diagnose = await _db.Diagnoser.FindAsync(diagnoseId);
                var newSymptomDiagnose = new SymptomDiagnose { Symptom = newSymptom, Diagnose = diagnose };
                await _db.SymptomDiagnoser.AddAsync(newSymptomDiagnose);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateEnDiagnose(Diagnose diagnose)
        {
            try
            {
                var newDiagnose = new Diagnose { DiagnoseNavn = diagnose.DiagnoseNavn, Description = diagnose.Description };
                await _db.Diagnoser.AddAsync(newDiagnose);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}