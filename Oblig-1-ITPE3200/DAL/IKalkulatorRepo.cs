using Oblig_1_ITPE3200.Models;
using Oblig_1_ITPE3200.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.DAL
{
    public interface IKalkulatorRepo
    {       
        Task<List<DiagnoseModel>> GetDiagnoser();
        Task<List<Symptom>> GetSymptomer();

        Task<List<DiagnoseModel>> SearchDiagnoser(string[] symptomArray);

        Task<bool> SlettEnDiagnoser(int diagnoseId);

        Task<List<DiagnoseModel>> GetEnDiagnose(int diagnoseId);
    }
}
