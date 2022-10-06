﻿using Microsoft.EntityFrameworkCore;
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
                    // First gathers all DiseaseSymptoms
                    List<DiseaseSymptom> lds = await _db.DiseaseSymptoms.Select(ds => new DiseaseSymptom
                {
                    DiseaseId = ds.DiseaseId,
                    SymptomId = ds.SymptomId
                }).ToListAsync();

                List<int> symptomIds = new List<int>();

                foreach (DiseaseSymptom ds in lds)
                {
                    if (ds.DiseaseId == id)
                    {
                        symptomIds.Add(ds.SymptomId);
                    }
                }

                List<Symptom> symptoms = new List<Symptom>();

                foreach (int i in symptomIds)
                {
                    symptoms.Add(await _db.Symptoms.FindAsync(i));
                }

                return symptoms;
            }
            catch
            {
                return null;
            }
        }
    }
}
