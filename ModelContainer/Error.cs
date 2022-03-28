using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ModelContainer
{
	public class Error
	{
		[JsonPropertyName("ErrorCode")]
		public string ErrorCode { get; set; }
		[JsonPropertyName("ErrorStatus")]
		public int ErrorStatus { get; set; }
		[JsonPropertyName("ErrorMessage")]
		public string ErrorMessage { get; set; }
	}
}
