using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModelContainer
{
	public class Div : IOperations
	{
		[JsonPropertyName("Dividend")]
		public float? Dividend { get; set; }
		[JsonPropertyName("Divisor")]
		public float? Divisor { get; set; }
	}
}
