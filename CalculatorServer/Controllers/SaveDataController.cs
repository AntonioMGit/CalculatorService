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
		private readonly ILogger<SaveDataController> _logger;

		public SaveDataController(ILogger<SaveDataController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public Operations Funcion([FromBody] Query values)
		{
			Operations operations = new Operations();
			operations = SaveData.GetOperationsByKey(values.Id);

			return operations;
		}

	}
}
