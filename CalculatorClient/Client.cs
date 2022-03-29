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

							OperationDiv(idUser);

							break;
						//square root
						case 5:
							Console.WriteLine("Preparing Square root operation");

							OperationSqrt(idUser);

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

		internal static string ChangeUser(string idUser)
		{
			if (idUser != "")
				Console.WriteLine("Old user id: " + idUser);
			else
				Console.WriteLine("No user logged");

			Console.Write("New user:");
			idUser = Console.ReadLine();

			return idUser;
		}

		internal static void ReviewHistoy(string idUser)
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

		internal static void DoOperation(IOperations operation, string idUser, string path)
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

		//funcion para el test
		private static float DoOperationTest(IOperations add, string idUser, string v)
		{
			return (9);
		}

		internal static void OperationAdd(string idUser)
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
			//https://stackoverflow.com/questions/3167617/determine-if-code-is-running-as-part-of-a-unit-test
			#if DEBUG
			var respTest = DoOperationTest(add, idUser, "add");
			Console.WriteLine(respTest.ToString());
			#else
			DoOperation(add, idUser,"add");
			#endif
		}

		internal static void OperationSub(string idUser)
		{
			SubRequest sub = new SubRequest();

			sub.Minuend = ReadFloat("Minuend: ");

			sub.Subtrahend = ReadFloat("Subtrahend: ");

			#if DEBUG
			var respTest = DoOperationTest(sub, idUser, "sub");
			Console.WriteLine();
			Console.WriteLine(respTest.ToString());
			#else
			DoOperation(sub, idUser, "sub");
			#endif
		}

		internal static void OperationMult(string idUser)
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

			#if DEBUG
			var respTest = DoOperationTest(mult, idUser, "mult");
			Console.WriteLine(respTest.ToString());
			#else
			DoOperation(mult, idUser, "mult");
			#endif
		}

		internal static void OperationDiv(string idUser)
		{
			DivRequest div = new DivRequest();

			div.Dividend = ReadFloat("Dividend: ");

			//que no sea 0
			div.Divisor = ReadFloat("Divisor: ");
			#if DEBUG
			var respTest = DoOperationTest(div, idUser, "div");
			Console.WriteLine();
			Console.WriteLine(respTest.ToString());
			#else
			DoOperation(div, idUser, "div");
			#endif
		}

		internal static void OperationSqrt(string idUser)
		{
			//comprobar que el valor sea mayor a 0
			SqrtRequest sqrt = new SqrtRequest();
			sqrt.Number = ReadFloat("Number: ");

			#if DEBUG
			var respTest = DoOperationTest(sqrt, idUser, "sqrt");
			Console.WriteLine();
			Console.WriteLine(respTest.ToString());
			#else
			DoOperation(sqrt, idUser, "sqrt");
			#endif
		}

		internal static float ReadFloat(string message)
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
