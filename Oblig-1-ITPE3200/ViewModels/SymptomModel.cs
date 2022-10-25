﻿using Oblig_1_ITPE3200.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.ViewModels
{
    public static class SymptomModelExtensions
    {
        public static IQueryable<SymptomModel> MapToSymptomModel(this IQueryable<Symptom> symptomSource)
        {
            return symptomSource.Select(ss => new SymptomModel
            {
                SymptomId = ss.SymptomId,
                SymptomNavn = ss.SymptomNavn
            });
        }
    }
    public class SymptomModel
    {
        public int SymptomId { get; set; }
        public string SymptomNavn { get; set; }


        public SymptomModel() { }
        public SymptomModel(Symptom s)
        {
            SymptomId = s.SymptomId;
            SymptomNavn = s.SymptomNavn;
        }
    }
}
