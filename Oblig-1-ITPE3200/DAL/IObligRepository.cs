using Oblig_1_ITPE3200.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.DAL
{
    public interface IObligRepository
    {
        Task<bool> CreateDisease(Disease disease);
        Task<bool> UpdateDisease(Disease disease);
        Task<Disease> GetDisease(int id);
        Task<bool> DeleteDisease(int id);
        Task<List<Disease>> GetAllDiseases();
        Task<bool> CreateSymptom(Symptom symptom);
        Task<bool> UpdateSymptom(Symptom symptom);
        Task<Symptom> GetSymptom(int id);
        Task<bool> DeleteSymptom(int id);
        Task<List<Symptom>> GetAllSymptoms();
        Task<List<DiseaseSymptom>> GetAllDiseaseSymptoms();
        Task<bool> CreateDiseaseSymptom(int DiseaseId, int SymptomId);
        Task<bool> DeleteDiseaseSymptom(int DiseaseId, int SymptomId);
        Task<List<Disease>> SearchDiseases(int[] symptomsArray);
    }
}
