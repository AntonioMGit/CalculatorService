using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Web.Http;

namespace ModelContainer
{
	public class Suma
	{
		[JsonPropertyName("Sum")]
		public float Sum { get; set; }
	}
}
