using Microsoft.Data.SqlClient;

namespace PMF.Model
{
    public class PagedSortedList<T>
    {
        

        public string TransactionKind { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public SortOrder SortOrder { get; set; }


    }
}
