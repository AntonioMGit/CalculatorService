using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using ModelContainer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace CalculatorServer.Controllers
{
	[ApiController]
	[Route("/div")]
	public class DivController : ControllerBase
	{
		/*
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};
		*/
		private readonly ILogger<DivController> _logger;

		public DivController(ILogger<DivController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Funcion([FromBody] Div cosa)
		{

			ObjectResult resp;

			if (cosa.Dividend.HasValue && cosa.Divisor.HasValue)
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

				float dividend = cosa.Dividend.Value;
				float divisor = cosa.Divisor.Value;

				Division div = new Division();

				if (divisor != 0)
				{
					//https://social.msdn.microsoft.com/Forums/es-ES/fed06f10-2c79-498f-92c5-cfb03b727510/tomar-la-parte-entera-de-una-divisin-sin-redondear?forum=vcses
					div.Quotient = (float)Math.Floor(dividend / divisor);
					div.Remainder = dividend % divisor;

					resp = Ok(div);

				}
				else
				{
					Error error = new Error();
					error.ErrorCode = "Bad request";
					error.ErrorStatus = 400;
					error.ErrorMessage = "Disor = 0";

					var myContent = JsonConvert.SerializeObject(error);
					resp = BadRequest(myContent);
				}

				if (!key.Equals(""))
				{
					string operation = "Div";
					string calculation = "";
					string date = "";

					calculation = cosa.Dividend + " / " + cosa.Divisor + " = " + div.Quotient + " Remainder: " + div.Remainder;

					Operation op = new Operation();

					op.OperationStr = operation;
					op.Calculation = calculation;
					op.Date = date;

					SaveData.Add(key, op);

					Console.WriteLine(key + " " + SaveData.GetOperationsByKey(key).OperationList[0].Calculation);
				}

			}
			else
			{
				Error error = new Error();
				error.ErrorCode = "Bad request";
				error.ErrorStatus = 400;
				error.ErrorMessage = "Datos incorrectos";

				var myContent = JsonConvert.SerializeObject(error);
				resp = BadRequest(myContent);
			}

			return resp;
		}

	}
}
