using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Oblig_1_ITPE3200.DTOs;
using Oblig_1_ITPE3200.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.DAL
{
    [ExcludeFromCodeCoverage]
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
        public async Task<DiseaseDTO> CreateDisease(Disease disease)
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

                return await GetDisease(newDisease.Id);
            }
            catch
            {
                return null;
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
                    .ExcludeSymptoms(options.SelectedSymptoms)
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

        // Search method
        public async Task<List<DiseaseDTO>> SearchDiseases(List<Symptom> selectedSymptoms)
        {
            try
            {                
                // Return empty list if nothing is selected
                if (selectedSymptoms.Count() == 0)
                {
                    return new List<DiseaseDTO>();
                }

                IQueryable<Disease> query = _db.Diseases;


                foreach(Symptom symptom in selectedSymptoms)
                {
                    query = query.Where(d => d.DiseaseSymptoms.Select(ds => ds.SymptomId).Contains(symptom.Id));
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

        public async Task<bool> LogIn(UserDTO user)
        {
            try
            {
                User foundUser = await _db.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

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
