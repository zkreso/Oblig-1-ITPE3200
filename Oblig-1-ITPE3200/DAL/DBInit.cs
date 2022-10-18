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
            Symptom symptom3 = new Symptom { SymptomNavn = "cold" };
            Symptom symptom4 = new Symptom { SymptomNavn = "fever" };

            Diagnose diagnose1 = new Diagnose { DiagnoseNavn = "Covid", Description = "New virus found in 2020"};
            Diagnose diagnose2 = new Diagnose { DiagnoseNavn = "flu", Description = "High incidence in winter"};

            /**SymptomDiagnose symptomDiagnose1 = new SymptomDiagnose { Symptom = symptom1,  Diagnose = diagnose1 };
            SymptomDiagnose symptomDiagnose2 = new SymptomDiagnose { Symptom = symptom2, Diagnose = diagnose1 }; 
            //SymptomDiagnose symptomDiagnose3 = new SymptomDiagnose { Symptom = symptom3, Diagnose = diagnose1 };
            //SymptomDiagnose symptomDiagnose4 = new SymptomDiagnose { Symptom = symptom4, Diagnose = diagnose1 };
            SymptomDiagnose symptomDiagnose5 = new SymptomDiagnose { Symptom = symptom1, Diagnose = diagnose2 };
            SymptomDiagnose symptomDiagnose6 = new SymptomDiagnose { Symptom = symptom2, Diagnose = diagnose2 };    
            SymptomDiagnose symptomDiagnose7 = new SymptomDiagnose { Symptom = symptom3, Diagnose = diagnose2 };
            SymptomDiagnose symptomDiagnose8 = new SymptomDiagnose { Symptom = symptom4, Diagnose = diagnose2 };

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
            symptom2.SymptomDiagnoser = symptomDiagnoser2;**/


            //context.SymptomDiagnoser.AddRange(symptomDiagnose1, symptomDiagnose2, symptomDiagnose5, symptomDiagnose6, symptomDiagnose7, symptomDiagnose8);

            context.Symptomer.AddRange(symptom1, symptom2,symptom3,symptom4);
            context.SaveChanges();
        }
    }
}
