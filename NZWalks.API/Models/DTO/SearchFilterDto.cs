namespace NZWalks.API.Models.DTO
{
    public class SearchFilterDto
    {
        public string? FilterOn { get; set; }
        public string? FilterQuery { get; set; }
        public string? SortBy { get; set; }
        public bool? IsAccending { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > 100 ? 100 : value;
        }
        private int pageSize = 10;
    }
}
