using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using ModelContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CalculatorServer.Controllers
{
	[ApiController]
	[Route("/add")]
	public class AddController : ControllerBase
	{
		/*
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};
		*/
		private readonly ILogger<AddController> _logger;

		public AddController(ILogger<AddController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public Suma Funcion([FromBody] Add cosa)
		{
			//https://stackoverflow.com/questions/21404734/how-to-add-and-get-header-values-in-webapi
			var re = Request;
			var headers = re.Headers;
			StringValues keyValues;
			String key = "";
			if (headers.ContainsKey("X-Evi-Tracking-Id"))
			{
				headers.TryGetValue("X-Evi-Tracking-Id", out keyValues);
				key = keyValues.First();
			}

			List<float> lista = cosa.Addends;

			Suma suma = new Suma();
			suma.Sum = lista.Sum();

			if (!key.Equals(""))
			{
				string operation = "Sum";
				string calculation = "";
				string date = "";

				calculation = lista[0].ToString() + " ";

				for(int i = 1; i < lista.Count; i++)
				{
					calculation += " + " + lista[i];
				}

				calculation += " = " + suma.Sum.ToString();

				Operation op = new Operation();

				op.OperationStr = operation;
				op.Calculation = calculation;
				op.Date = date;

				SaveData.Add(key, op);

				Console.WriteLine(key + " " + SaveData.GetOperationsByKey(key).OperationList[0].Calculation);
			}
			//Request.Create

			return suma;
		}

	}
}
