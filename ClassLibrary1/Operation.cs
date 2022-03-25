using System.Text.Json.Serialization;

namespace ModelContainer
{
	public class Operation
	{
		[JsonPropertyName("Operation")]
		public string OperationStr { get; set; }
		[JsonPropertyName("Calculation")]
		public string Calculation { get; set; }
		[JsonPropertyName("Date")]
		public string Date { get; set; }
	}
}