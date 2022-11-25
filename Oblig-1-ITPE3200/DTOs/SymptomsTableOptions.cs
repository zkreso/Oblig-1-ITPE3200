using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Oblig_1_ITPE3200.DTOs
{
    // Adapted from https://github.com/JonPSmith/EfCoreinAction-SecondEdition repo of book:
    // Smith, J.P. (2021), Entity Framework Core in Action (2nd edition). Manning
    [ExcludeFromCodeCoverage]
    public class SymptomsTableOptions
    {
        public const int DefaultPageSize = 10;
        private int _pageSize = DefaultPageSize;
        private int _pageNum = 1;

        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-\'\!\?]{0,999}")]
        public string OrderByOptions { get; set; }
        public List<SymptomDTO> SelectedSymptoms { get; set; }
        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-\'\!\?]{0,99}")]
        public string SearchString { get; set; }

        [RegularExpression(@"^[1-9]([0-9]){0,3}$")]
        public int PageNum
        {
            get { return _pageNum; }
            set { _pageNum = value; }
        }
        [RegularExpression(@"^(5|10|20)")]
        public int PageSize { 
            get { return _pageSize; }
            set { _pageSize = value; }
        }
        public int NumEntries { get; private set; }
        public int NumPages { get; private set; }

        public void SetupRestOfDTO<T>(IQueryable<T> query)
        {
            NumEntries = query.Count();
            NumPages = (int)Math.Ceiling((double)NumEntries / _pageSize);
            PageNum = Math.Min(Math.Max(1, PageNum), NumPages);
        }
    }
}
