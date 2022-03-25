using System;
using System.Text.Json.Serialization;

namespace ModelContainer
{
	public class Query
	{
		[JsonPropertyName("Id")]
		public string Id { get; set; }
	}
}
