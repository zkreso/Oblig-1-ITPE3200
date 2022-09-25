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

                var symptom1 = new Symptom { Name = "Runny nose" };
                var symptom2 = new Symptom { Name = "Coughing" };
                var symptom3 = new Symptom { Name = "High temperature" };
                var symptom4 = new Symptom { Name = "Sneezing" };
                var symptom5 = new Symptom { Name = "Muscle ache" };

                var symptoms1 = new List<Symptom>();
                var symptoms2 = new List<Symptom>();

                symptoms1.Add(symptom1);
                symptoms1.Add(symptom4);

                symptoms2.Add(symptom2);
                symptoms2.Add(symptom3);
                symptoms2.Add(symptom5);

                disease1.Symptoms = symptoms1;
                disease2.Symptoms = symptoms2;

                context.Diseases.Add(disease1);
                context.Diseases.Add(disease2);
                context.SaveChanges();
            }
        }
    }
}
