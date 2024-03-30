namespace Dev.Business.Models
{
    public class PaginateList<T> where T : class
    {   
        public IEnumerable<T>? List { get; set; }
        public int PageIndex { get; set; }
        public int TotalResults { get; set; }
        public int PageSize { get; set; }
        public string? Query { get; set; }

    }
}
