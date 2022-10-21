﻿using Microsoft.AspNetCore.Mvc;
using Oblig_1_ITPE3200.DAL;
using Oblig_1_ITPE3200.Models;
using Oblig_1_ITPE3200.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.Controllers
{
    [Route("[controller]/[action]")]
    public class KalkulatorController : ControllerBase
    {
        private readonly IKalkulatorRepo _db;

        public KalkulatorController(IKalkulatorRepo db)
        {
            _db = db;
        }

        public async Task<List<DiagnoseModel>> GetDiagnoser()
        {
            
            return await _db.GetDiagnoser();
        }

        public async Task<List<Symptom>> GetSymptomer()
        {
            return await _db.GetSymptomer();
        }

        public async Task<List<DiagnoseModel>> SearchDiagnoser(string[] symptomArray)
         {
             return await _db.SearchDiagnoser(symptomArray);
         }

        public async Task<bool> SlettEnDiagnoser(int diagnoseId)
        {
            return await _db.SlettEnDiagnoser(diagnoseId);
        }

        public async Task<List<DiagnoseModel>> GetEnDiagnose(int diagnoseId)
        {
            return await _db.GetEnDiagnose(diagnoseId);
        }
    }
}
