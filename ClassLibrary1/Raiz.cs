using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ModelContainer
{
	public class Raiz
	{
		[JsonPropertyName("Square")]
		public float Square { get; set; }
	}
}
