namespace Domain.Settings
{
	/// <summary>
	/// Logging data model customized to suit licensing scheme
	/// </summary>
	public class Log
	{
		public long Id { get; set; }
		public string Message { get; set; } = null!;
		public string? MessageTemplate { get; set; }
		public string Level { get; set; } = null!;
		public DateTime TimeStamp { get; set; }
		public string? Exception { get; set; }
		public string LogEvent { get; set; } = null!;
		public string? Actor { get; set; }
		public string? Package { get; set; }
		public string? Feature { get; set; }
		public string? Subfeature { get; set; }
		public string? TransactionId { get; set; }
		public string? TransactionType { get; set; }
	}
}

