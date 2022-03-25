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
	[Route("/sub")]
	public class SubController : ControllerBase
	{
		/*
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};
		*/
		private readonly ILogger<SubController> _logger;

		public SubController(ILogger<SubController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public Resta Funcion([FromBody] Sub cosa)
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

			float minuend = cosa.Minuend;
			float subtrahend = cosa.Subtrahend;
			Resta resta = new Resta();

			resta.Difference = minuend - subtrahend;

			if (!key.Equals(""))
			{
				string operation = "Sub";
				string calculation = "";
				string date = "";

				calculation = cosa.Minuend + " - " + cosa.Subtrahend + " = " + resta.Difference;

				Operation op = new Operation();

				op.OperationStr = operation;
				op.Calculation = calculation;
				op.Date = date;

				SaveData.Add(key, op);

				Console.WriteLine(key + " " + SaveData.GetOperationsByKey(key).OperationList[0].Calculation);
			}


			return resta;
		}

	}
}
