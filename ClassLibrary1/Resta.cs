using System.Text.Json.Serialization;

namespace ModelContainer
{
	public class Resta
	{
		[JsonPropertyName("Difference")]
		public float Difference { get; set; }
	}
}
