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
	[Route("/query")]
	public class SaveDataController : ControllerBase
	{
		/*
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};
		*/
		private readonly ILogger<SaveDataController> _logger;

		public SaveDataController(ILogger<SaveDataController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public Operations Funcion([FromBody] Query cosa)
		{
			Operations operations = new Operations();
			operations = SaveData.GetOperationsByKey(cosa.Id);

			return operations;
		}

	}
}
