namespace Application.Core
{
	/// <summary>
	/// Wrapper class for generic ways to deal with any kind of response
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Result<T>
	{
		public bool IsSuccess { get; set; }

		public T? Value { get; set; }

		public string? Error { get; set; }

		public List<string>? PartialSuccessMessages { get; set; }

		public static Result<T> Success(T? value) => new() { IsSuccess = true, Value = value };
		public static Result<T> Failure(string error) => new() { IsSuccess = false, Error = error };
		public static Result<T> PartialSuccess(T? value, List<string> messages) => new()
        {
			IsSuccess = true,
			Value = value,
			PartialSuccessMessages = messages
		};
	}
}