using System;
using Oblig_1_ITPE3200.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Oblig_1_ITPE3200.DAL
{
    public class KalkulatorRepository : IKalkulatorRepository
    {
        private readonly DB _sykdomDB;


        // Constructor
        public KalkulatorRepository(DB db)
        {
            _sykdomDB = db;
        }

        // Get the whole table/list
        public async Task<List<Sykdom>> HentSykdom()
        {
            try
            {
                List<Sykdom> sykdomListe = await _sykdomDB.Sykdomstabel.ToListAsync();
                return sykdomListe;
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
                _sykdomDB.Sykdomstabel.Add(nySykdom);
                await _sykdomDB.SaveChangesAsync();
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
                Sykdom enSykdom = await _sykdomDB.Sykdomstabel.FindAsync(id);
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
                Sykdom gammelSykdom = await _sykdomDB.Sykdomstabel.FindAsync(nySykdom.Id);
                gammelSykdom.Navn = nySykdom.Navn;
                gammelSykdom.Beskrivelse = nySykdom.Beskrivelse;
                await _sykdomDB.SaveChangesAsync();
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
                Sykdom targetSykdom = await _sykdomDB.Sykdomstabel.FindAsync(id);
                _sykdomDB.Sykdomstabel.Remove(targetSykdom);
                await _sykdomDB.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

