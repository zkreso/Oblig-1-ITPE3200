using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Oblig_1_ITPE3200.Models;

namespace Oblig_1_ITPE3200.DAL
{
    public class DBInit
    {
        public static void initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DB>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var d1 = new Disease { Name = "Common cold", Description = "Usually harmless viral infection of upper respiratory tract" };
                var d2 = new Disease { Name = "Influenza", Description = "Viral infection of upper respiratory tract"};
                var d3 = new Disease { Name = "Coronary heart disease", Description = "Common heart condition where the blood vessels struggle to supply the heart" };
                var d4 = new Disease { Name = "Alzheimers disease", Description = "Progressive neurological disorder causing atrophy of the brain" };
                var d5 = new Disease { Name = "Diabetes type 2", Description = "The impairment of the body to use sugar as fuel" };
                var d6 = new Disease { Name = "Anemia", Description = "Lack of red blood cells" };
                var d7 = new Disease { Name = "Covid", Description = "A respiratory disease carried by a coronavirus discovered in 2019" };

                var s1 = new Symptom { Name = "Runny nose" }; // Cold
                var s2 = new Symptom { Name = "Sneezing" }; // Cold
                var s3 = new Symptom { Name = "Coughing" }; // Cold and flu
                var s4 = new Symptom { Name = "High temperature" }; // Flu
                var s5 = new Symptom { Name = "Chest pain" };
                var s6 = new Symptom { Name = "Shortness of breath" };
                var s7 = new Symptom { Name = "Memory loss" };
                var s8 = new Symptom { Name = "Excessive hunger" };
                var s9 = new Symptom { Name = "Frequent urination" };
                var s10 = new Symptom { Name = "Fatigue" };
                var s11 = new Symptom { Name = "Loss of taste or smell" };

                var ds1 = new DiseaseSymptom { Disease = d1, Symptom = s1 };
                var ds2 = new DiseaseSymptom { Disease = d1, Symptom = s2 };
                var ds3 = new DiseaseSymptom { Disease = d1, Symptom = s3 };
                var ds4 = new DiseaseSymptom { Disease = d2, Symptom = s3 };
                var ds5 = new DiseaseSymptom { Disease = d2, Symptom = s4 };
                var ds6 = new DiseaseSymptom { Disease = d3, Symptom = s5 };
                var ds7 = new DiseaseSymptom { Disease = d3, Symptom = s6 };
                var ds8 = new DiseaseSymptom { Disease = d4, Symptom = s7 };
                var ds9 = new DiseaseSymptom { Disease = d5, Symptom = s8 };
                var ds10 = new DiseaseSymptom { Disease = d5, Symptom = s9 };
                var ds11 = new DiseaseSymptom { Disease = d6, Symptom = s10 };
                var ds12 = new DiseaseSymptom { Disease = d7, Symptom = s3 };
                var ds13 = new DiseaseSymptom { Disease = d7, Symptom = s10 };
                var ds14 = new DiseaseSymptom { Disease = d7, Symptom = s11 };

                context.DiseaseSymptoms.AddRange(ds1, ds2, ds3, ds4, ds5, ds6, ds7, ds8, ds9, ds10, ds11, ds12, ds13, ds14);
                context.SaveChanges();
            }
        }
    }
}
