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

            SymptomDiagnose symptomDiagnose1 = new SymptomDiagnose { SymptomId = symptom1.SymptomId, Symptom = symptom1, DiagnoseId = diagnose1.DiagnoseId, Diagnose = diagnose1 };
            SymptomDiagnose symptomDiagnose2 = new SymptomDiagnose { SymptomId = symptom2.SymptomId, Symptom = symptom2, DiagnoseId = diagnose2.DiagnoseId, Diagnose = diagnose2 };

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

            //diagnose1.SymptomDiagnoser = (ICollection<SymptomDiagnose>)symptomDiagnose1;
            //diagnose2.SymptomDiagnoser = (ICollection<SymptomDiagnose>)symptomDiagnose2;

            /**List<Diagnose> diagnoser1 = new List<Diagnose>
            {
                diagnose1,
            };

            List<Diagnose> diagnoser2 = new List<Diagnose>
            {
                diagnose2,
            };**/

            symptom1.SymptomDiagnoser = symptomDiagnoser1;
            symptom2.SymptomDiagnoser = symptomDiagnoser2;

            //symptom1.SymptomDiagnoser = (ICollection<SymptomDiagnose>)symptomDiagnose1;
            //symptom2.SymptomDiagnoser = (ICollection<SymptomDiagnose>)symptomDiagnose2;

            context.Symptomer.Add(symptom1);
            context.Symptomer.Add(symptom2);
            //context.Diagnoser.Add(diagnose1);
            //context.Diagnoser.Add(diagnose2);
            //context.SymptomDiagnoser.Add(symptomDiagnose1);
            //context.SymptomDiagnoser.Add(symptomDiagnose2);

            context.SaveChanges();
        }
    }
}
