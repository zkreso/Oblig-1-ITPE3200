using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oblig_1_ITPE3200.DAL;
using Oblig_1_ITPE3200.Models;

namespace Oblig_1_ITPE3200.Controllers
{
    [Route("[controller]/[action]")]
    public class kalkulatorController : ControllerBase
    {
        private readonly IKalkulatorRepository _db;

        // Constructor
        public kalkulatorController(IKalkulatorRepository db)
        {
            _db = db;
        }

        public async Task<List<Sykdom>> HentSykdom()
        {
            return await _db.HentSykdom();
        }

        public async Task<List<Symptom>> HentSymptom()
        {
            return await _db.HentSymptom();
        }

        public async Task<bool> Lagre(Sykdom nySykdom)
        {
            return await _db.Lagre(nySykdom);
        }

        public async Task<Sykdom> HentEn(int id)
        {
            return await _db.HentEn(id);
        }

        public async Task<bool> Endre(Sykdom innSykdom)
        {
            return await _db.Endre(innSykdom);
        }

        public async Task<bool> Slett(int id)
        {
            return await _db.Slett(id);
        }
    }
}

