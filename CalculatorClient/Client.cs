using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using ModelContainer;
using CalculatorServer;

namespace CalculatorClient
{
	public class Client
	{
		const string URL = "http://localhost:5000/";

		public static void Main(string[] args)
		{
			string idUser = "";

			int valorNum = -1; //este es el del switch del menu

			Console.Write("Do you want to save the history? (y/n):");
			var resp = Console.ReadLine();

			if (resp.Equals("y", StringComparison.InvariantCultureIgnoreCase))
			{
				Console.Write("Identificator:");
				idUser = Console.ReadLine();
			}

			do
			{
				string menu = "Operation:\n" +
					"\t1.Addition\n" +
					"\t2.Subtraction\n" +
					"\t3.Multiply\n" +
					"\t4.Division\n" +
					"\t5.Square root\n" +
					"\t6.Review history\n" +
					"\t7.Change user\n" +
					"\t0.Exit";
				Console.WriteLine(menu);

				String valor = Console.ReadLine();

				bool exito = Int32.TryParse(valor, out valorNum);
				//el try parse se carga lo que tenga en la varibale valorNum antes
				if (!exito)
					valorNum = -1;

				if (exito)
				{
					switch (valorNum)
					{
						//Addition
						case 1:
							Console.WriteLine("Preparing Addition operation");

							OperationAdd(idUser);

							break;
						//Subtraction
						case 2:
							Console.WriteLine("Preparing Subtraction operation");

							OperationSub(idUser);

							break;
						//Multiply
						case 3:
							Console.WriteLine("Preparing Multiply operation");

							OperationMult(idUser);

							break;
						//division
						case 4:
							Console.WriteLine("Preparing Division operation");

							DivOperation(idUser);

							break;
						//square root
						case 5:
							Console.WriteLine("Preparing Square root operation");

							SqrtOperation(idUser);

							break;
						//review history
						case 6:

							ReviewHistoy(idUser);

							break;
						//change user
						case 7:

							idUser = ChangeUser(idUser);

							break;
					}
				}
				else
				{
					Console.WriteLine("Wrong value.");
				}
			}
			while (valorNum != 0);
		}

		public static string ChangeUser(string idUser)
		{
			if (idUser != "")
				Console.WriteLine("Old user id: " + idUser);
			else
				Console.WriteLine("No user logged");

			Console.Write("New user:");
			idUser = Console.ReadLine();

			return idUser;
		}

		public static void ReviewHistoy(string idUser)
		{
			if (idUser != "")
			{
				Console.WriteLine("Review history of " + idUser);

				Query query = new Query();
				query.Id = idUser;

				DoOperation(query, idUser, "query");
			}
			else
			{
				Console.WriteLine("No user logged");
			}
		}

		public static void DoOperation(IOperations operation, string idUser, string path)
		{
			using (var client = new HttpClient())
			{
				try
				{
					var myContent = JsonConvert.SerializeObject(operation);
					Console.WriteLine(myContent);
					var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
					var byteContent = new ByteArrayContent(buffer);
					byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

					byteContent.Headers.Add("X-Evi-Tracking-Id", idUser);

					var result = client.PostAsync(URL + path, byteContent).Result;

					Console.WriteLine(result.Content.ReadAsStringAsync().Result.ToString());
				}
				catch (Exception e)
				{
					Console.WriteLine("Connection error: " + e.Message.ToString());
				}
			}
		}

		public static void OperationAdd(string idUser)
		{
			var list = new List<float>();
			string resp = "";

			float value;
			do
			{
				value = ReadFloat("Value: ");

				list.Add(value);

				Console.WriteLine("Value added\n" +
					"Do you want to add another value? (y/n)");
				resp = Console.ReadLine();
			}
			while (resp.Equals("y", StringComparison.InvariantCultureIgnoreCase));

			AddRequest add = new AddRequest();

			add.Addends = list;

			DoOperation(add, idUser, "add");
		}

		public static void OperationSub(string idUser)
		{
			SubRequest sub = new SubRequest();

			sub.Minuend = ReadFloat("Minuend: ");

			sub.Subtrahend = ReadFloat("Subtrahend: ");

			DoOperation(sub, idUser, "sub");
		}

		public static void OperationMult(string idUser)
		{
			var list = new List<float>();
			string resp = "";
			float value;
			do
			{
				value = ReadFloat("Value: ");

				list.Add(value);

				Console.WriteLine("Value added\n" +
					"Do you want to add another value? (y/n)");
				resp = Console.ReadLine();
			}
			while (resp.Equals("y", StringComparison.InvariantCultureIgnoreCase));

			MultRequest mult = new MultRequest();

			mult.Factors = list;

			DoOperation(mult, idUser, "mult");
		}

		public static void DivOperation(string idUser)
		{
			DivRequest div = new DivRequest();

			div.Dividend = ReadFloat("Dividend: ");

			//que no sea 0
			div.Divisor = ReadFloat("Divisor: ");

			DoOperation(div, idUser, "div");
		}

		public static void SqrtOperation(string idUser)
		{
			//comprobar que el valor sea mayor a 0
			SqrtRequest sqrt = new SqrtRequest();
			sqrt.Number = ReadFloat("Number: ");

			DoOperation(sqrt, idUser, "sqrt");
		}

		public static float ReadFloat(string message)
		{
			float value;
			string strValue = "";

			do
			{
				Console.Write(message);
				strValue = Console.ReadLine();
			} while (!float.TryParse(strValue, out value));

			return value;
		}
	}
}
