using Microsoft.EntityFrameworkCore;
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
                List<Symptom> symptoms = new List<Symptom>();

                Disease d = await _db.Diseases.FindAsync(id);

                List<DiseaseSymptom> lds = d.DiseaseSymptoms.ToList();

                foreach (DiseaseSymptom ds in lds)
                {
                    if (!symptoms.Contains(ds.Symptom))
                    {
                        symptoms.Add(ds.Symptom);
                    }
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
                var oldD = await _db.Diseases.FindAsync(newD.Id);

                newD.DiseaseSymptoms = new List<DiseaseSymptom>();

                foreach (var s in newSlist)
                {
                    newD.DiseaseSymptoms.Add(new DiseaseSymptom { DiseaseId = newD.Id, Disease = newD, SymptomId = s.Id,Symptom = s });

                    s.DiseaseSymptoms = new List<DiseaseSymptom>();
                    s.DiseaseSymptoms.Add(new DiseaseSymptom { DiseaseId = newD.Id, Disease = newD, Symptom = s, SymptomId = s.Id });
                }

                // Changing symptoms linked to disease
                oldD.DiseaseSymptoms = newD.DiseaseSymptoms;

                oldD.Name = newD.Name;
                oldD.Description = newD.Description;

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
