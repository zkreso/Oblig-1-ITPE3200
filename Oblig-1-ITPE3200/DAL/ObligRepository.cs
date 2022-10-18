﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

        // Gets all diseases from Diseases-table
        public async Task<List<Disease>> GetAllDiseases()
        {
            try
            {
                List<Disease> allDiseases = await _db.Diseases.ToListAsync();
                return allDiseases;
            }
            catch
            {
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
            catch
            {
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
            catch
            {
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

                foreach (var ds in oldD.DiseaseSymptoms)
                {
                    symptoms.Add(ds.Symptom);
                } 

                return symptoms;
            }
            catch
            {
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
            catch
            {
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
            catch
            {
                return false;
            }
        }
    }
}