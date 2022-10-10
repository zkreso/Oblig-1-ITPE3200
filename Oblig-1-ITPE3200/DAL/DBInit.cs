using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Oblig_1_ITPE3200.Models
{
    public static class DBInit
    {
        public static void Initialise(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DB>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var sykdom1 = new Sykdom { Navn = "Corona" };
                var sykdom2 = new Sykdom { Navn = "Influenza" };

                var symptom1 = new Symptom { Navn = "Cough" };
                var symptom2 = new Symptom { Navn = "Fever" };
                var symptom3 = new Symptom { Navn = "Loss of taste or smell" };
                var symptom4 = new Symptom { Navn = "Runny nose" };

                context.SykdomTabel.Add(sykdom1);
                context.SykdomTabel.Add(sykdom2);

                context.SymptomTabel.Add(symptom1);
                context.SymptomTabel.Add(symptom2);
                context.SymptomTabel.Add(symptom3);
                context.SymptomTabel.Add(symptom4);

                context.SaveChanges();

            }
        }
    }
}

