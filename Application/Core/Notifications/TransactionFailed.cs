namespace Application.Core.Notifications
{
    public class TransactionFailed
    {
        public string OriginalTransaction { get; set; } = null!;
        public string MissingVersion { get; set; } = null!;
        public string? AdditionalDetails {get;set;}
    }
}