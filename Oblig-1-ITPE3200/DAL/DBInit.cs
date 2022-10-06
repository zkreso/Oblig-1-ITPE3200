using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Oblig_1_ITPE3200.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.DAL
{
    public class DBInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<KalkulatorContext>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Symptom symptom1 = new Symptom { SymptomNavn = "cough" };
            Symptom symptom2 = new Symptom { SymptomNavn = "sneeze" };

            Diagnose diagnose1 = new Diagnose { DiagnoseNavn = "Covid", Description = "New virus found in 2020"};
            Diagnose diagnose2 = new Diagnose { DiagnoseNavn = "Pollen allergy", Description = "High incidence in the spring"};

            SymptomDiagnose symptomDiagnose1 = new SymptomDiagnose { Symptom = symptom1,  Diagnose = diagnose1 };
            SymptomDiagnose symptomDiagnose2 = new SymptomDiagnose { Symptom = symptom2, Diagnose = diagnose2 };

            List<SymptomDiagnose> symptomDiagnoser1 = new List<SymptomDiagnose>
            {
                symptomDiagnose1,
            };

            List<SymptomDiagnose> symptomDiagnoser2 = new List<SymptomDiagnose>
            {
                symptomDiagnose2,
            };

            diagnose1.SymptomDiagnoser = symptomDiagnoser1;
            diagnose2.SymptomDiagnoser = symptomDiagnoser2;      

            symptom1.SymptomDiagnoser = symptomDiagnoser1;
            symptom2.SymptomDiagnoser = symptomDiagnoser2;


            context.Symptomer.Add(symptom1);
            context.Symptomer.Add(symptom2);

            context.SaveChanges();
        }
    }
}
