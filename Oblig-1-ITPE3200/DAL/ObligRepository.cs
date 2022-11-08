using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oblig_1_ITPE3200.DTOs;
using Oblig_1_ITPE3200.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public async Task<List<DiseaseDTO>> GetAllDiseases(string searchString)
        {
            try
            {
                List<DiseaseDTO> allDiseases = await _db.Diseases
                    .MapDiseaseToDTO()
                    .FilterBySearchString(searchString)
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
                Disease changedDisease = _db.Diseases
                    .Include(d => d.DiseaseSymptoms)
                    .FirstOrDefault(x => x.Id == disease.Id);

                changedDisease.Name = disease.Name;
                changedDisease.Description = disease.Description;
                changedDisease.DiseaseSymptoms.Clear();

                changedDisease.DiseaseSymptoms = disease.DiseaseSymptoms?.ToList();

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
        public async Task<List<SymptomDTO>> GetAllSymptoms()
        {
            try
            {
                List<SymptomDTO> symptoms = await _db.Symptoms
                    .MapSymptomToDTO()
                    .ToListAsync();

                return symptoms;
            }
            catch
            {
                return null;
            }
        }
        public async Task<SymptomsTable> GetSymptomsTable(SymptomsTableOptions options)
        {
            try
            {
                IQueryable<SymptomDTO> query = _db.Symptoms
                    .MapSymptomToDTO()
                    .FilterBySearchString(options.SearchString)
                    .ExcludeSymptomsById(options.SymptomIds)
                    .OrderSymptomsBy(options.OrderByOptions);

                options.SetupRestOfDTO(query);

                int pageNum = options.PageNum - 1;
                int pageSize = options.PageSize;

                if (pageNum != 0)
                {
                    query = query.Skip(pageNum * pageSize);
                }
                query = query.Take(pageSize);

                List<SymptomDTO> symptoms = await query.ToListAsync();

                return new SymptomsTable(options, symptoms);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<SymptomDTO>> GetRelatedSymptoms(int id)
        {
            try
            {
                List<SymptomDTO> symptoms = await _db.DiseaseSymptoms
                .Where(ds => ds.DiseaseId == id)
                .Select(ds => ds.Symptom)
                .MapSymptomToDTO()
                .ToListAsync();

                return symptoms;
            }
            catch
            {
                return null;
            }
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

        // Joining table CRUD
        public async Task<bool> CreateDiseaseSymptom(int diseaseId, int symptomId)
        {
            try
            {
                var newDiseaseSymptom = new DiseaseSymptom
                {
                    SymptomId = symptomId,
                    DiseaseId = diseaseId
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
        public async Task<bool> DeleteDiseaseSymptom(int diseaseId, int symptomId)
        {
            try
            {
                DiseaseSymptom diseaseSymptom = await _db.DiseaseSymptoms.FindAsync(diseaseId, symptomId);
                _db.DiseaseSymptoms.Remove(diseaseSymptom);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        */

        // Search method
        public async Task<List<DiseaseDTO>> SearchDiseases(int[] symptomIds)
        {
            try
            {
                // Return empty list if nothing is selected
                if (symptomIds.Length == 0)
                {
                    return new List<DiseaseDTO>();
                }

                IQueryable<Disease> query = _db.Diseases;

                foreach(int i in symptomIds)
                {
                    query = query.Where(d => d.DiseaseSymptoms.Select(ds => ds.SymptomId).Contains(i));
                }

                List<DiseaseDTO> results = await query
                    .MapDiseaseToDTO()
                    .ToListAsync();

                return results;
            }
            catch
            {
                return null;
            }
        }


        public static byte[] MakeHash(string password, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 1000,
                numBytesRequested: 32);
        }

        public static byte[] MakeSalt()
        {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csp.GetBytes(salt);
            return salt;
        }

        [HttpPost]
        public async Task<bool> LogIn(User user)
        {
            try
            {
                Users foundUser = await _db.Users.FirstOrDefaultAsync(u =>
                                                u.Username == user.Username);

                byte[] hash = MakeHash(user.Password, foundUser.Salt);
                bool ok = hash.SequenceEqual(foundUser.Password);
                if (ok)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                //_log.LogInformation("Could not log in. " + e.Message);
                return false;
            }
        }
    }
}
