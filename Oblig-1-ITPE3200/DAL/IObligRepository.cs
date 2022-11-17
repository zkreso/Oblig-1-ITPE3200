using Oblig_1_ITPE3200.DTOs;
using Oblig_1_ITPE3200.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.DAL
{
    public interface IObligRepository
    {
        // Disease CRUD
        Task<DiseaseDTO> GetDisease(int id);
        Task<List<DiseaseDTO>> GetAllDiseases(string searchString);
        Task<bool> CreateDisease(Disease disease);
        Task<bool> UpdateDisease(Disease disease);
        Task<bool> DeleteDisease(int id);

        // Symptom CRUD
        Task<List<SymptomDTO>> GetAllSymptoms();
        Task<SymptomsTable> GetSymptomsTable(SymptomsTableOptions options);
        Task<List<SymptomDTO>> GetRelatedSymptoms(int id);

        /* Unused/unneccessary methods
        Task<Symptom> GetSymptom(int id);
        Task<bool> CreateSymptom(Symptom symptom);
        Task<bool> UpdateSymptom(Symptom symptom);
        Task<bool> DeleteSymptom(int id);

        // Joining table CRUD
        Task<bool> CreateDiseaseSymptom(int DiseaseId, int SymptomId);
        Task<bool> DeleteDiseaseSymptom(int DiseaseId, int SymptomId);
        */
        // Search method
        Task<List<DiseaseDTO>> SearchDiseases(int[] symptomIds);
        Task<bool> LogIn(UserDTO user);
    }
}
