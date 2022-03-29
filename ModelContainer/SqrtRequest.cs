using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ModelContainer
{
	public class SqrtRequest : IOperations
	{
		[JsonPropertyName("Number")]
		public float? Number { get; set; }
	}
}
