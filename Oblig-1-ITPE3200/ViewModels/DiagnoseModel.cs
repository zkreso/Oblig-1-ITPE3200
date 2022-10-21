using Oblig_1_ITPE3200.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.ViewModels
{
    public static class DiagnoseModelExtensions
    {
        public static IQueryable<DiagnoseModel> MapToDiagnoseModel (this IQueryable<Diagnose> diagnoseSource)
        {
            return diagnoseSource.Select(ds => new DiagnoseModel
            {
                DiagnoseId = ds.DiagnoseId,
                DiagnoseNavn = ds.DiagnoseNavn,
                Description = ds.Description,
                SymptomNavnene = ds.SymptomDiagnoser.Select(sd => sd.Symptom.SymptomNavn).ToArray()
            });
        }
    }
    public class DiagnoseModel
    {
        public int DiagnoseId { get; set; }
        public string DiagnoseNavn { get; set; }
        public string Description { get; set; }

        public string[] SymptomNavnene { get; set; }

        public DiagnoseModel() { }
        public DiagnoseModel(Diagnose d)
        {
            DiagnoseId = d.DiagnoseId;
            DiagnoseNavn = d.DiagnoseNavn;
            Description = d.Description;
            SymptomNavnene = d.SymptomDiagnoser.Select(sd => sd.Symptom.SymptomNavn).ToArray();
        }
    }
}
