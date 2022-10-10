using Oblig_1_ITPE3200.DTOs;
using Oblig_1_ITPE3200.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.DAL
{
    public interface IObligRepository
    {
        // Disease methods
        Task<DiseaseDTO> GetDisease(int id);
        Task<List<DiseaseListDTO>> GetAllDiseases();
        Task<bool> CreateDisease(Disease disease);
        Task<bool> UpdateDisease(Disease disease);
        Task<bool> DeleteDisease(int id);
        
        // Symptom methods
        Task<Symptom> GetSymptom(int id);
        Task<List<Symptom>> GetAllSymptoms();
        Task<List<Symptom>> GetRelatedSymptoms(int id);
        Task<List<Symptom>> GetUnrelatedSymptoms(int id);
        Task<bool> CreateSymptom(Symptom symptom);
        Task<bool> UpdateSymptom(Symptom symptom);
        Task<bool> DeleteSymptom(int id);
        
        // Joining table methods
        Task<List<DiseaseSymptom>> GetAllDiseaseSymptoms();
        Task<bool> CreateDiseaseSymptom(int DiseaseId, int SymptomId);
        Task<bool> DeleteDiseaseSymptom(int DiseaseId, int SymptomId);
        
        // Search method
        Task<List<Disease>> SearchDiseases(int[] symptomsArray);
    }
}
