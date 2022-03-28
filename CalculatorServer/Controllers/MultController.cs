﻿using Microsoft.AspNetCore.Mvc;
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
	[Route("/mult")]
	public class MultController : ControllerBase
	{
		private readonly ILogger<MultController> _logger;

		public MultController(ILogger<MultController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Funcion([FromBody] Mult cosa)
		{
			_logger.LogInformation("Processing mult");

			ObjectResult resp;

			if (cosa.Factors != null)
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

				List<float> lista = cosa.Factors;
				Multiplicacion mult = new Multiplicacion();
				mult.Product = lista[0];

				for (int i = 1; i < lista.Count; i++)
				{
					mult.Product *= lista[i];
				}

				if (!key.Equals(""))
				{
					_logger.LogInformation("Processing mult - X-Evi-Tracking-Id: " + key);

					string operation = "Mult";
					string calculation = "";
					string date = DateTime.Now.ToString();

					calculation = lista[0].ToString();

					for (int i = 1; i < lista.Count; i++)
					{
						calculation += " * " + lista[i];
					}

					calculation += " = " + mult.Product.ToString();

					Operation op = new Operation();

					op.OperationStr = operation;
					op.Calculation = calculation;
					op.Date = date;

					SaveData.Add(key, op);

					Console.WriteLine(key + " " + SaveData.GetOperationsByKey(key).OperationList[0].Calculation);

					_logger.LogInformation("Processing mult - Operation save in history");
				}
				resp = Ok(mult);

				_logger.LogInformation("Processing mult - DONE");
			}
			else
			{
				_logger.LogInformation("Processing mult - DISRUPTED - Incorrect data");

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
