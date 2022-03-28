using System;
using System.Text.Json.Serialization;

namespace ModelContainer
{
	public class Query : IOperations
	{
		[JsonPropertyName("Id")]
		public string Id { get; set; }
	}
}
