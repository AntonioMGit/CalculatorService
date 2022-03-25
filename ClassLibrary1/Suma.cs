using System.Text.Json.Serialization;

namespace ModelContainer
{
	public class Suma
	{
		[JsonPropertyName("Sum")]
		public float Sum { get; set; }
	}
}
