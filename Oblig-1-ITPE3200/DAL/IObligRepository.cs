using Oblig_1_ITPE3200.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.DAL
{
    public interface IObligRepository
    {
        Task<List<Disease>> GetAllDiseases();
        Task<List<Symptom>> GetAllSymptoms();
        Task<Disease> GetDisease(int id);
        Task<List<Symptom>> GetSymptomsDisease(int id);
        Task<bool> DeleteDisease(int id);
    }
}
