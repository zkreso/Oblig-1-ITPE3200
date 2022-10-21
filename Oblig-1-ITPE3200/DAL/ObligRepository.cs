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

        // Disease CRUD
        public async Task<DiseaseDTO> GetDisease(int id)
        {
            try
            {
                DiseaseDTO diseaseDTO = await _db.Diseases
                    .MapDiseaseToDTO()
                    .FirstOrDefaultAsync(x => x.Id == id);

                return diseaseDTO;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<DiseaseDTO>> GetAllDiseases()
        {
            try
            {
                List<DiseaseDTO> allDiseases = await _db.Diseases
                    .Select(d => new DiseaseDTO
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Symptoms = d.DiseaseSymptoms.Select(s => s.Symptom.Name).ToArray()
                    })
                    .ToListAsync();

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
                    DiseaseSymptoms = disease.DiseaseSymptoms?.Select(s => new DiseaseSymptom
                    {
                        SymptomId = s.SymptomId
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

        // Symptom CRUD
        public async Task<List<SymptomDTO>> GetAllSymptoms(string searchString)
        {
            try
            {
                List<SymptomDTO> allSymptoms = await _db.Symptoms
                    .MapSymptomToDTO()
                    .SearchSymptomDTO(searchString)
                    .ToListAsync();

                return allSymptoms;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<SymptomDTO>> GetFilteredSymptoms(int[] symptomsArray, string searchString)
        {
            try
            {
                List<SymptomDTO> symptoms = await _db.Symptoms
                    .Where(s => !symptomsArray.Contains(s.Id))
                    .MapSymptomToDTO()
                    .SearchSymptomDTO(searchString)
                    .ToListAsync();

                return symptoms;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<SymptomDTO>> GetRelatedSymptoms(int id)
        {
            List<SymptomDTO> symptoms = await _db.DiseaseSymptoms
                .Where(ds => ds.DiseaseId == id)
                .Select(ds => ds.Symptom)
                .MapSymptomToDTO()
                .ToListAsync();

            return symptoms;
        }
        public async Task<List<SymptomDTO>> GetUnrelatedSymptoms(int id)
        {
            List<SymptomDTO> symptoms = await _db.Symptoms
                .Where(s => !s.DiseaseSymptoms.Select(ds => ds.DiseaseId).Contains(id))
                .MapSymptomToDTO()
                .ToListAsync();

            return symptoms;
        }

        /* Unused/unnecessary methods
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
        */

        // Joining table CRUD
        public async Task<bool> CreateDiseaseSymptom(int DiseaseId, int SymptomId)
        {
            try
            {
                var newDiseaseSymptom = new DiseaseSymptom
                {
                    SymptomId = SymptomId,
                    DiseaseId = DiseaseId
                };

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
        public async Task<List<DiseaseDTO>> SearchDiseases(int[] symptomsArray)
        {
            try
            {
                // Return empty list if nothing is selected
                if (symptomsArray.Length == 0)
                {
                    return new List<DiseaseDTO>();
                }

                List<DiseaseDTO> results = await _db.DiseaseSymptoms
                    .Where(ds => symptomsArray.Contains(ds.SymptomId))
                    .GroupBy(ds => ds.DiseaseId)
                    .Where(x => x.Count() == symptomsArray.Count())
                    .Select(x => x.Key)
                    .Join(_db.Diseases, x => x, y => y.Id, (x, y) => y)
                    .MapDiseaseToDTO()
                    .ToListAsync();

                return results;
            }
            catch
            {
                return null;
            }
        }
    }
}
