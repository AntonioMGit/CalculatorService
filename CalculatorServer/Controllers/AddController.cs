using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using ModelContainer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CalculatorServer.Controllers
{
	[ApiController]
	[Route("/add")]
	public class AddController : ControllerBase
	{
		private readonly ILogger<AddController> _logger;

		public AddController(ILogger<AddController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Funcion([FromBody] Add cosa)
		{
			ObjectResult resp;

			_logger.LogInformation("Processing add");

			if (cosa.Addends != null)
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
					_logger.LogInformation("Processing add - X-Evi-Tracking-Id: " + key);

					string operation = "Sum";
					string calculation = "";
					string date = DateTime.Now.ToString();

					calculation = lista[0].ToString();

					for (int i = 1; i < lista.Count; i++)
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

					_logger.LogInformation("Processing add - Operation save in history");
				}

				resp = Ok(suma);

				_logger.LogInformation("Processing add - DONE");
			}
			else
			{
				_logger.LogInformation("Processing add - DISRUPTED - Incorrect data");

				Error error = new Error();
				error.ErrorCode = "Bad request";
				error.ErrorStatus = 400;
				error.ErrorMessage = "Incorrect data";

				var myContent = JsonConvert.SerializeObject(error);
				resp = BadRequest(myContent);
			}

			return resp;
		}

	}
}
