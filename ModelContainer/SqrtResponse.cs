using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ModelContainer
{
	public class SqrtResponse
	{
		[JsonPropertyName("Square")]
		public float Square { get; set; }
	}
}
