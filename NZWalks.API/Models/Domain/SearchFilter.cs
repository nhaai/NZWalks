namespace NZWalks.API.Models.Domain
{
    public class SearchFilter
    {
        public string? FilterOn { get; set; }
        public string? FilterQuery { get; set; }
        public string? SortBy { get; set; }
        public bool? IsAccending { get; set; }
        public int? PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
