using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ModelContainer
{
	public class Sqrt
	{
		[JsonPropertyName("Number")]
		public float Number { get; set; }
	}
}
