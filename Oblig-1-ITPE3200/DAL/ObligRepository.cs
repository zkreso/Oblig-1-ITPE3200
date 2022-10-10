using Microsoft.EntityFrameworkCore;
using Oblig_1_ITPE3200.DTOs;
using Oblig_1_ITPE3200.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<DiseaseDTO> GetDisease(int id)
        {
            try
            {
                Disease disease = await _db.Diseases.FindAsync(id);

                DiseaseDTO diseaseDTO = new DiseaseDTO
                {
                    Id = disease.Id,
                    Name = disease.Name,
                    Description = disease.Description,
                    Symptoms = disease.DiseaseSymptoms.Select(ds => ds.Symptom.Name).ToArray()
                };

                return diseaseDTO;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<DiseaseListDTO>> GetAllDiseases()
        {
            try
            {
                List<DiseaseListDTO> allDiseases = await _db.Diseases.Select(d => new DiseaseListDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Symptoms = d.DiseaseSymptoms.Select(ds => ds.Symptom.Name).ToArray()
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
                Disease newDisease = new Disease
                {
                    Name = disease.Name,
                    Description = disease.Description,
                    DiseaseSymptoms = disease.Symptoms?.Select(s => new DiseaseSymptom
                    {
                        SymptomId = s.Id
                    }).ToList()
                };

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
                Disease changedDisease = _db.Diseases.Find(disease.Id);

                changedDisease.Name = disease.Name;
                changedDisease.Description = disease.Description;
                changedDisease.DiseaseSymptoms.Clear();

                changedDisease.DiseaseSymptoms = disease.Symptoms?.Select(s => new DiseaseSymptom
                {
                    SymptomId = s.Id,
                    Disease = changedDisease
                }).ToList();

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
        public async Task<List<Symptom>> GetRelatedSymptoms(int id)
        {
            List<Symptom> symptoms = await _db.Symptoms.ToListAsync();

            symptoms = symptoms.Where(s => s.DiseaseSymptoms.Select(ds => ds.DiseaseId).Contains(id)).ToList();

            return symptoms;
        }
        public async Task<List<Symptom>> GetUnrelatedSymptoms(int id)
        {
            List<Symptom> symptoms = await _db.Symptoms.ToListAsync();

            symptoms = symptoms.Where(s => !s.DiseaseSymptoms.Select(ds => ds.DiseaseId).Contains(id)).ToList();

            return symptoms;
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
