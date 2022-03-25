using System.Text.Json.Serialization;

namespace ModelContainer
{
	public class Sub
	{
		[JsonPropertyName("Minuend")]
		public float Minuend { get; set; }
		[JsonPropertyName("Subtrahend")]
		public float Subtrahend { get; set; }
	}
}
