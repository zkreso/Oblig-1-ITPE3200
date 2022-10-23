using System;
using System.Linq;

namespace Oblig_1_ITPE3200.DTOs
{
    // Adapted from https://github.com/JonPSmith/EfCoreinAction-SecondEdition repo of textbook:
    // Finch, J.P. (2021), Entity Framework Core in Action (2nd edition). Manning
    public class SymptomsTableOptions
    {
        public const int DefaultPageSize = 10;
        private int _pageSize = DefaultPageSize;
        private int _pageNum = 1;

        public string OrderByOptions { get; set; }
        public int[] SymptomIds { get; set; }
        public string SearchString { get; set; }
        public int PageNum
        {
            get { return _pageNum; }
            set { _pageNum = value; }
        }
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
