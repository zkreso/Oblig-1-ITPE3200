using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Hosting;
using Oblig_1_ITPE3200.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.DAL
{
    public class ObligRepository : IObligRepository
    {
        private readonly DB _db;

        public ObligRepository(DB db)
        {
            _db = db;
        }

        // Disease methods
        public async Task<Disease> GetDisease(int id)
        {
            try
            {
                Disease disease = await _db.Diseases.FindAsync(id);

                disease.Symptoms = disease.DiseaseSymptoms.Select(ds => ds.Symptom).ToList();

                return disease;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<Disease>> GetAllDiseases()
        {
            try
            {
                List<Disease> allDiseases = await _db.Diseases.Select(d => new Disease
                {
                    Id = d.Id,
                    Name = d.Name,
                    Symptoms = d.DiseaseSymptoms.Select(ds => ds.Symptom).ToList()
                }).ToListAsync();

                return allDiseases;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> CreateDisease(Disease disease)
        {
            try
            {
                var newDisease = new Disease();
                newDisease.Id = disease.Id;
                newDisease.Name = disease.Name;
                
                _db.Diseases.Add(newDisease);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateDisease(Disease disease)
        {
            try
            {
                Disease changedDisease = await _db.Diseases.FindAsync(disease.Id);

                changedDisease.Name = disease.Name;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteDisease(int id)
        {
            try
            {
                Disease disease = await _db.Diseases.FindAsync(id);
                _db.Diseases.Remove(disease);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        // Symptom methods
        public async Task<Symptom> GetSymptom(int id)
        {
            try
            {
                Symptom symptom = await _db.Symptoms.FindAsync(id);

                symptom.Diseases = symptom.DiseaseSymptoms.Select(ds => ds.Disease).ToList();

                return symptom;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<Symptom>> GetAllSymptoms()
        {
            try
            {
                List<Symptom> allSymptoms = await _db.Symptoms.Select(s => new Symptom
                {
                    Id = s.Id,
                    Name = s.Name,
                    Diseases = s.DiseaseSymptoms.Select(ds => ds.Disease).ToList()
                }).ToListAsync();

                return allSymptoms;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> CreateSymptom(Symptom symptom)
        {
            try
            {
                var newSymptom = new Symptom();
                newSymptom.Id = symptom.Id;
                newSymptom.Name = symptom.Name;

                _db.Symptoms.Add(newSymptom);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateSymptom(Symptom symptom)
        {
            try
            {
                Symptom changedSymptom = await _db.Symptoms.FindAsync(symptom.Id);

                changedSymptom.Name = symptom.Name;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteSymptom(int id)
        {
            try
            {
                Symptom symptom = await _db.Symptoms.FindAsync(id);
                _db.Symptoms.Remove(symptom);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        
        // Joining table methods
        public async Task<List<DiseaseSymptom>> GetAllDiseaseSymptoms()
        {
            try
            {
                List<DiseaseSymptom> allDiseaseSymptoms = await _db.DiseaseSymptoms.ToListAsync();
                return allDiseaseSymptoms;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> CreateDiseaseSymptom(int DiseaseId, int SymptomId)
        {
            try
            {
                var newDiseaseSymptom = new DiseaseSymptom();
                newDiseaseSymptom.SymptomId = SymptomId;
                newDiseaseSymptom.DiseaseId = DiseaseId;

                _db.DiseaseSymptoms.Add(newDiseaseSymptom);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteDiseaseSymptom(int DiseaseId, int SymptomId)
        {
            try
            {
                DiseaseSymptom diseaseSymptom = await _db.DiseaseSymptoms.FindAsync(DiseaseId, SymptomId);
                _db.DiseaseSymptoms.Remove(diseaseSymptom);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Search method
        public async Task<List<Disease>> SearchDiseases(int[] symptomsArray)
        {
            try
            {
                if (symptomsArray.Length > 0)
                {
                    List<Disease> results = await _db.Diseases.ToListAsync();
                    results = results
                        .Where(d => symptomsArray.All(d.DiseaseSymptoms.Select(ds => ds.SymptomId).Contains))
                        .Select(d => new Disease
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Symptoms = d.DiseaseSymptoms.Select(s => s.Symptom).ToList()
                        })
                        .ToList();

                    return results;
                }
                else
                {
                    return new List<Disease>();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
