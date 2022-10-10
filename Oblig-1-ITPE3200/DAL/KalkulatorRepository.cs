using System;
using Oblig_1_ITPE3200.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Oblig_1_ITPE3200.DAL
{
    public class KalkulatorRepository : IKalkulatorRepository
    {
        private readonly DB _db;


        // Constructor
        public KalkulatorRepository(DB db)
        {
            _db = db;
        }

        // Get the whole table/list of diseases
        public async Task<List<Sykdom>> HentSykdom()
        {
            try
            {
                List<Sykdom> sykdomListe = await _db.SykdomTabel.ToListAsync();
                return sykdomListe;
            }

            catch
            {
                return null;
            }

        }

        // Get the whole table/list of symptoms
        public async Task<List<Symptom>> HentSymptom()
        {
            try
            {
                List<Symptom> symptomListe = await _db.SymptomTabel.ToListAsync();
                return symptomListe;
            }

            catch
            {
                return null;
            }

        }


        // Register a new disease
        public async Task<bool> Lagre(Sykdom nySykdom)
        {
            try
            {
                _db.SykdomTabel.Add(nySykdom);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Get one disease
        public async Task<Sykdom> HentEn(int id)
        {
            try
            {
                Sykdom enSykdom = await _db.SykdomTabel.FindAsync(id);
                return enSykdom;
            }
            catch
            {
                return null;
            }

        }

        // Edit disease information
        public async Task<bool> Endre(Sykdom nySykdom)
        {
            try
            {
                Sykdom gammelSykdom = await _db.SykdomTabel.FindAsync(nySykdom.Id);
                gammelSykdom.Navn = nySykdom.Navn;
                gammelSykdom.Beskrivelse = nySykdom.Beskrivelse;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Delete disease from database
        public async Task<bool> Slett(int id)
        {
            try
            {
                Sykdom targetSykdom = await _db.SykdomTabel.FindAsync(id);
                _db.SykdomTabel.Remove(targetSykdom);
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

