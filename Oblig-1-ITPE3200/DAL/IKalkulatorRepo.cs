using Oblig_1_ITPE3200.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.DAL
{
    public interface IKalkulatorRepo
    {       
        Task<List<SymptomDiagnose>> GetSymptomDiagnoser();
        Task<List<Symptom>> GetSymptomer();

        Task<List<Diagnose>> GetDiagnoser(Symptom symptom);
    }
}
