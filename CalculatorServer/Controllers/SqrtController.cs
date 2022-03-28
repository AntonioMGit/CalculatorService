using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelContainer;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace CalculatorServer.Controllers
{
	[ApiController]
	[Route("/sqrt")]
	public class SqrtController : ControllerBase
	{
		private readonly ILogger<SqrtController> _logger;

		public SqrtController(ILogger<SqrtController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Funcion([FromBody] Sqrt cosa)
		{
			_logger.LogInformation("Processing sqrt");

			ObjectResult resp;

			if (cosa.Number.HasValue)
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
					sqrt.Square = (float)Math.Sqrt(cosa.Number.Value);

					resp = Ok(sqrt);

					_logger.LogInformation("Processing sqrt - DONE");
				}
				else
				{
					_logger.LogInformation("Processing sqrt - DISRUPTED - Incorrect data: square root of number < 0");

					Error error = new Error();
					error.ErrorCode = "Bad request";
					error.ErrorStatus = 400;
					error.ErrorMessage = "Square root of number < 0";

					resp = BadRequest(error);
				}

				if (!key.Equals(""))
				{
					_logger.LogInformation("Processing sqrt - X-Evi-Tracking-Id: " + key);

					string operation = "Sqrt";
					string calculation = "";
					string date = DateTime.Now.ToString();

					calculation = "Square root of: " + cosa.Number + " = " + sqrt.Square;

					Operation op = new Operation();

					op.OperationStr = operation;
					op.Calculation = calculation;
					op.Date = date;

					SaveData.Add(key, op);

					Console.WriteLine(key + " " + SaveData.GetOperationsByKey(key).OperationList[0].Calculation);

					_logger.LogInformation("Processing mult - Operation save in history");
				}
			}
			else
			{
				_logger.LogInformation("Processing sqrt - DISRUPTED - Incorrect data");

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
