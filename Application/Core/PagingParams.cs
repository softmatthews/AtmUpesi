namespace Application.Core
{
    /// <summary>
    /// Tracks global parameters for pagination 
    /// </summary>
    public class PagingParams
    {
        // Page size
        private int _pageSize = 100;
        private const int MaxPageSize = 500;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        // Page number
        public int PageNumber { get; set; } = 1;
    }
}