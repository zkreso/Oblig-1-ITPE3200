using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Oblig_1_ITPE3200.Models;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;

namespace Oblig_1_ITPE3200.DAL
{
    public class ObligRepository : IObligRepository
    {
        private readonly DB _db;
        private ILogger<ObligRepository> _log;

        public ObligRepository(DB db, ILogger<ObligRepository> log)
        {
            _db = db;
            _log = log;
        }

        // Gets all diseases from Diseases-table
        public async Task<List<Disease>> GetAllDiseases()
        {
            try
            {
                List<Disease> allDiseases = await _db.Diseases.ToListAsync();
                return allDiseases;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        // Gets all symptoms from Symptoms-table
        public async Task<List<Symptom>> GetAllSymptoms()
        {
            try
            {
                List<Symptom> allSymptoms = await _db.Symptoms.ToListAsync();
                return allSymptoms;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        // Gets one disease from the disease-table by using id
        public async Task<Disease> GetDisease(int id)
        {
            try
            {
                Disease disease = await _db.Diseases.FindAsync(id);
                return disease;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        public async Task<Symptom> GetSymptom(int id)
        {
            try
            {
                Symptom symptom = await _db.Symptoms.FindAsync(id);
                return symptom;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        // Gets all symptoms linked to one disease
        public async Task<List<Symptom>> GetSymptomsDisease (int id)
        {
            try
            {
                await _db.DiseaseSymptoms.LoadAsync();
                await _db.Symptoms.LoadAsync();
                var oldD = await _db.Diseases.FindAsync(id);
                List<Symptom> symptoms = new List<Symptom>();

                if (oldD.DiseaseSymptoms != null)
                {
                    foreach (var ds in oldD.DiseaseSymptoms)
                    {
                        symptoms.Add(ds.Symptom);
                    }
                }
                else
                {
                    return null;
                }

                return symptoms;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        //Deletes one disease from disease-table using id
        public async Task<bool> DeleteDisease(int id)
        {
            try
            {
                Disease d = await _db.Diseases.FindAsync(id);
                _db.Remove(d);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        // Changes current disease with new disease
        public async Task<bool> ChangeDisease(Disease newD, List<Symptom> newSlist)
        {
            try
            {
                await _db.DiseaseSymptoms.LoadAsync();
                await _db.Symptoms.LoadAsync();
                await _db.Diseases.LoadAsync();

                // Find old d
                var oldD = await _db.Diseases.FindAsync(newD.Id);

                newD.DiseaseSymptoms = new List<DiseaseSymptom>();

                // Adding each s into a new ds object list
                bool first = true;
                foreach (var s in newSlist)
                {
                    // Getting the s object in the db
                    var newS = await _db.Symptoms.FindAsync(s.Id);



                    // Running clean up of old ds in s ds list, only first time
                    if (first)
                    {
                        // Collects ds to remove in list not to crash program
                        var rm = new List<DiseaseSymptom>();

                        // If s ds object already empty, no need to add to delete list
                        if (newS.DiseaseSymptoms != null)
                        {
                            foreach (var ds in newS.DiseaseSymptoms)
                            {
                                if (ds.DiseaseId == oldD.Id)
                                {
                                    rm.Add(ds); // Adding which ds to delete in list
                                }
                            }
                            // Removing ds from s ds list using the rm ds list
                            foreach (var r in rm)
                            {
                                newS.DiseaseSymptoms.Remove(r);
                            }
                            // Turning this if-test off
                            first = false;
                        }

                    }



                    //Creating new ds object with s and d
                    var newDs = new DiseaseSymptom { Symptom = newS, Disease = newD };

                    // If s object got fully emptied during deletion (no more links)
                    // Have to initialize new list
                    if (newS.DiseaseSymptoms == null)
                    {
                        newS.DiseaseSymptoms = new List<DiseaseSymptom>();
                    }


                    // Adding ds to both tables
                    newS.DiseaseSymptoms.Add(newDs);
                    newD.DiseaseSymptoms.Add(newDs);
                }




                // Removing oldD for safety and saving new
                _db.Diseases.Remove(oldD);
                await _db.Diseases.AddAsync(newD);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }


        public async Task<Disease> FindMatchingDisease (List<int> ids)
        {
            try
            {

                List<Symptom> symptoms = null;

                foreach (int id in ids)
                {
                    symptoms.Add(await _db.Symptoms.FindAsync(id));
                }

                List<Disease> allDiseases = await _db.Diseases.ToListAsync();
                List<int> scoreList = new List<int>(allDiseases.Count());
                foreach (int k in scoreList)
                {
                    scoreList[k] = 0;
                }


                int i = 0;
                foreach (var s in symptoms)
                {
                    foreach (var d in allDiseases)
                    {
                        int j = 0;
                        var ds = d.DiseaseSymptoms;
                        if (s == ds[j].Symptom)
                        {
                            scoreList[i]++;
                        }

                        j++;
                    }
                    i++;
                }

                int maxIndex = scoreList.IndexOf(scoreList.Max());

                return allDiseases[maxIndex];
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        // Log in stuff

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
        
        public async Task<bool> LogIn(User user)
        {
            try
            {
                Users foundUser = await _db.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

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
                _log.LogInformation(e.Message);
                return false;
            }

        }
    }
}