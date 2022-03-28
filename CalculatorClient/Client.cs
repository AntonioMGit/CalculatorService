using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using ModelContainer;
using CalculatorServer;

namespace CalculatorClient
{
	public class Cliente
	{
		const string URL = "http://localhost:5000/";

		public static void Main(string[] args)
		{
			String strValor1;
			String strValor2; // quitar y solo usar 1?
			string idUser = "";
			float valor1;
			float valor2;
			float resultado;
			List<float> lista = new List<float>();

			int valorNum = -1; //este es el del switch del menu

			Console.Write("Do you want to save the history? (y/n):");
			strValor1 = Console.ReadLine();

			if (strValor1.Equals("y"))
			{
				Console.Write("Identificator:");
				idUser = Console.ReadLine();
			}

			do
			{
				string menu = "Operation:\n";
				menu += "\t1.Addition\n";
				menu += "\t2.Subtraction\n";
				menu += "\t3.Multiply\n";
				menu += "\t4.Division\n";
				menu += "\t5.Square root\n";
				menu += "\t6.Review history\n";
				menu += "\t7.Change user\n";
				menu += "\t0.Exit";
				Console.WriteLine(menu);

				String valor = Console.ReadLine();

				bool exito = Int32.TryParse(valor, out valorNum);
				//el try parse se carga lo que tenga en la varibale valorNum antes
				if (!exito)
					valorNum = -1;

				if (exito)
				{
					//addition
					//subtraction
					//multiply
					//division
					//square root
					//review history of requested operations

					String r; //colocar arriba o quitar

					switch (valorNum)
					{
						//Addition
						case 1:
							Console.WriteLine("Preparing Addition operation");
							do
							{
								do
								{
									Console.Write("Value: ");
									strValor1 = Console.ReadLine();
								}
								while (!float.TryParse(strValor1, out valor1));

								lista.Add(valor1);

								Console.WriteLine("Value added");
								Console.WriteLine("Do you want to add another value? (y/n)");
								r = Console.ReadLine();
							}
							while (r.Equals("y"));

							Add add = new Add();

							add.Addends = lista;

							DoOperations(add, idUser, "add");

							lista = new List<float>();

							break;
						//Subtraction
						case 2:
							Console.WriteLine("Subtraction");

							Console.WriteLine("Preparing Subtraction operation");

							do
							{
								Console.Write("Minuend: ");
								strValor1 = Console.ReadLine();
							}
							while (!float.TryParse(strValor1, out valor1));

							do
							{
								Console.Write("Subtrahend: ");
								strValor1 = Console.ReadLine();
							}
							while (!float.TryParse(strValor1, out valor2));

							Sub resta = new Sub();

							resta.Minuend = valor1;
							resta.Subtrahend = valor2;

							DoOperations(resta, idUser, "sub");

							break;
						//Multiply
						case 3:
							Console.WriteLine("Preparing Multiply operation");

							do
							{
								do
								{
									Console.Write("Value: ");
									strValor1 = Console.ReadLine();
								}
								while (!float.TryParse(strValor1, out valor1));

								lista.Add(valor1);

								Console.WriteLine("Value added");
								Console.WriteLine("Do you want to add another value? (y/n)");
								r = Console.ReadLine();
							}
							while (r.Equals("y"));

							Mult mult = new Mult();

							mult.Factors = lista;

							DoOperations(mult, idUser, "mult");

							lista = new List<float>();

							break;
						case 4:
							Console.WriteLine("Preparing Division operation");

							do
							{
								Console.Write("Dividend: ");
								strValor1 = Console.ReadLine();
							} while (!float.TryParse(strValor1, out valor1));

							do
							{
								Console.Write("Divisor: ");
								strValor2 = Console.ReadLine();
							} while (!float.TryParse(strValor2, out valor2) || strValor2.Equals("0")); //quitar lo del 0?

							Div div = new Div();

							div.Dividend = valor1;
							div.Divisor = valor2;

							DoOperations(div, idUser, "div");

							break;
						case 5:
							Console.WriteLine("Preparing Square root operation");
							//comprobar que el valor sea mayor a 0
							do
							{
								Console.Write("Number: ");
								strValor1 = Console.ReadLine();
							} while (!float.TryParse(strValor1, out valor1));

							Sqrt sqrt = new Sqrt();
							sqrt.Number = valor1;

							DoOperations(sqrt, idUser, "sqrt");

							break;
						case 6:

							if (idUser != "")
							{
								Console.WriteLine("Review history of " + idUser);

								Query query = new Query();
								query.Id = idUser;

								DoOperations(query, idUser, "query");
							}
							else
							{
								Console.WriteLine("No user logged");
							}

							break;
						case 7:

							if (idUser != "")
								Console.WriteLine("Old user id: " + idUser);
							else
								Console.WriteLine("No user logged");

							Console.Write("New user:");
							idUser = Console.ReadLine();

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
		public static void DoOperations(IOperations operation, string idUser, string url)
		{
			using (var client = new HttpClient())
			{
				var myContent = JsonConvert.SerializeObject(operation);
				Console.WriteLine(myContent);
				var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
				var byteContent = new ByteArrayContent(buffer);
				byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

				byteContent.Headers.Add("X-Evi-Tracking-Id", idUser);

				var result = client.PostAsync(URL + url, byteContent).Result;

				Console.WriteLine(result.Content.ReadAsStringAsync().Result.ToString());
			}
		}
	}
}
