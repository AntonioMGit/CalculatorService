using System.Text.Json.Serialization;


namespace ModelContainer
{
	public class Multiplicacion
	{
		[JsonPropertyName("Product")]
		public float Product { get; set; }
	}
}
