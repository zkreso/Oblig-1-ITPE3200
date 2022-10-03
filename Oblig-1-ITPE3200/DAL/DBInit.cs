using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Oblig_1_ITPE3200.Models;
using System.Collections.Generic;

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

                var disease1 = new Disease { Name = "Common cold" };
                var disease2 = new Disease { Name = "Influenza" };

                var symptom1 = new Symptom { Name = "Runny nose" }; // Cold
                var symptom2 = new Symptom { Name = "Sneezing" }; // Cold
                var symptom3 = new Symptom { Name = "Coughing" }; // Cold and flu
                var symptom4 = new Symptom { Name = "High temperature" }; // Flu

                var ds1 = new DiseaseSymptom { Disease = disease1, Symptom = symptom1 };
                var ds2 = new DiseaseSymptom { Disease = disease1, Symptom = symptom2 };
                var ds3 = new DiseaseSymptom { Disease = disease1, Symptom = symptom3 };
                var ds4 = new DiseaseSymptom { Disease = disease2, Symptom = symptom3 };
                var ds5 = new DiseaseSymptom { Disease = disease2, Symptom = symptom4 };

                context.DiseaseSymptoms.AddRange(ds1, ds2, ds3, ds4, ds5);
                context.SaveChanges();
            }
        }
    }
}
