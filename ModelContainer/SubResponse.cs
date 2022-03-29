using System.Text.Json.Serialization;

namespace ModelContainer
{
	public class SubResponse
	{
		[JsonPropertyName("Difference")]
		public float Difference { get; set; }
	}
}
