namespace Domain.Core
{
    /// <summary>
    /// Common class object template that will be used across application for queries
    /// </summary>
    public class QueryFilter
    {
        public string QueryCriteria { get; set; } = null!;
        public string QueryType { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}