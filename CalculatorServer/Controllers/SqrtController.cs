using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelContainer;
using Microsoft.Extensions.Primitives;

namespace CalculatorServer.Controllers
{
	[ApiController]
	[Route("/sqrt")]
	public class SqrtController : ControllerBase
	{
		/*
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};
		*/
		private readonly ILogger<SqrtController> _logger;

		public SqrtController(ILogger<SqrtController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public Raiz Funcion([FromBody] Sqrt cosa)
		{
			var re = Request;
			var headers = re.Headers;
			StringValues keyValues;
			String key = "";
			if (headers.ContainsKey("X-Evi-Tracking-Id"))
			{
				headers.TryGetValue("X-Evi-Tracking-Id", out keyValues);
				key = keyValues.First();
			}

			Raiz sqrt = new Raiz();

			if (cosa.Number > 0)
			{
				sqrt.Square = (float)Math.Sqrt(cosa.Number);
			}
			else
			{

			}

			if (!key.Equals(""))
			{
				string operation = "Sqrt";
				string calculation = "";
				string date = "";

				calculation = "Square root of: " + cosa.Number + " = " + sqrt.Square;

				Operation op = new Operation();

				op.OperationStr = operation;
				op.Calculation = calculation;
				op.Date = date;

				SaveData.Add(key, op);

				Console.WriteLine(key + " " + SaveData.GetOperationsByKey(key).OperationList[0].Calculation);
			}

			return sqrt;
		}

	}
}
