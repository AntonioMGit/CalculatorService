using System.Text.Json.Serialization;

namespace ModelContainer
{
	public class SubRequest : IOperations
	{
		[JsonPropertyName("Minuend")]
		public float? Minuend { get; set; }
		[JsonPropertyName("Subtrahend")]
		public float? Subtrahend { get; set; }
	}
}
