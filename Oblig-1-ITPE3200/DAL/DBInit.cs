
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

                // Resets db on every launch
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Creating symptoms and diseases for db
                var disease1 = new Disease { Name = "Common cold", Description = "Very Common, especially if cold."};
                var disease2 = new Disease { Name = "Influenza" , Description = "Very bad disease. Not good."};

                var symptom1 = new Symptom { Name = "Runny nose" }; // Cold
                var symptom2 = new Symptom { Name = "Sneezing" }; // Cold
                var symptom3 = new Symptom { Name = "Coughing" }; // Cold and flu
                var symptom4 = new Symptom { Name = "High temperature" }; // Flu

                disease1.DiseaseSymptoms = new List<DiseaseSymptom>();
                disease2.DiseaseSymptoms = new List<DiseaseSymptom>();

                disease1.DiseaseSymptoms.Add(new DiseaseSymptom { Symptom = symptom1 });
                disease1.DiseaseSymptoms.Add(new DiseaseSymptom { Symptom = symptom2 });
                disease1.DiseaseSymptoms.Add(new DiseaseSymptom { Symptom = symptom3 });

                disease2.DiseaseSymptoms.Add(new DiseaseSymptom { Symptom = symptom1 });
                disease2.DiseaseSymptoms.Add(new DiseaseSymptom { Symptom = symptom3 });
                disease2.DiseaseSymptoms.Add(new DiseaseSymptom { Symptom = symptom4 });

                // Adding diseases to db
                context.Diseases.Add(disease1);
                context.Diseases.Add(disease2);

                // Making accoutns for login
                var user = new Users();
                user.Username = "admin";
                var password = "Admin123";
                byte[] salt = ObligRepository.MakeSalt();
                byte[] hash = ObligRepository.MakeHash(password, salt);
                user.Password = hash;
                user.Salt = salt;
                context.Users.Add(user);

                context.SaveChanges();
            }
        }
    }
}
