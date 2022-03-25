using System.Text.Json.Serialization;

namespace ModelContainer
{
	public class Division
	{
		[JsonPropertyName("Quotient")]
		public float Quotient { get; set; }
		[JsonPropertyName("Remainder")]
		public float Remainder { get; set; }
	}
}
