using System.Text.Json.Serialization;


namespace ModelContainer
{
	public class MultResponse
	{
		[JsonPropertyName("Product")]
		public float Product { get; set; }
	}
}
