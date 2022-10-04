using Microsoft.EntityFrameworkCore;
using Oblig_1_ITPE3200.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.DAL
{
    public class Repo : IRepo
    {
        private readonly KalkulatorContext _db;

        public Repo (KalkulatorContext db)
        {
            _db = db;
        }

        public async Task<List<Symptom>> Index()
        {
            List<Symptom> symptomer = await _db.Symptomer.ToListAsync();

            return symptomer;
        }
    }
}
