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

			Console.Write("Quieres guardar el historial? (s/n):");
			strValor1 = Console.ReadLine();

			if (strValor1.Equals("s"))
			{
				Console.Write("Identificator:");
				idUser = Console.ReadLine();
			}

			do
			{
				Console.WriteLine("Operation:");
				Console.WriteLine("\t1.Addition");
				Console.WriteLine("\t2.Subtraction");
				Console.WriteLine("\t3.Multiply");
				Console.WriteLine("\t4.Division");
				Console.WriteLine("\t5.Square root");
				Console.WriteLine("\t6.Review history");
				Console.WriteLine("\t7.Change user");
				Console.WriteLine("\t0.Exit");

				String valor = Console.ReadLine();

				bool exito = Int32.TryParse(valor, out valorNum);
				//el try parse se carga lo que tenga en la varibale valorNum antes
				if (!exito)
					valorNum = -1;

				/*
				 * OJO
				 * The server validates input arguments/operands.
				 * The server returns an invalid request/arguments response, if/when input validation fails.
				*/

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
							//Add TWO or MORE operands and retrieve the result.This method serves UC-CALC - ADD external interface.
							Console.WriteLine("Preparing Addition operation");
							do
							{
								do
								{
									Console.Write("Valor: ");
									strValor1 = Console.ReadLine();
								}
								while (!float.TryParse(strValor1, out valor1));

								lista.Add(valor1);

								Console.WriteLine("Valor añadido");
								Console.WriteLine("Quieres otro valor? (s/n)");
								r = Console.ReadLine();
							}
							while (r.Equals("s"));

							Add add = new Add();

							add.Addends = lista;

							using (var client = new HttpClient())
							{
								var myContent = JsonConvert.SerializeObject(add);
								Console.WriteLine(myContent);
								var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
								var byteContent = new ByteArrayContent(buffer);
								byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

								byteContent.Headers.Add("X-Evi-Tracking-Id", idUser);

								var result = client.PostAsync("http://localhost:5000/add", byteContent).Result;

								Console.WriteLine(result.Content.ReadAsStringAsync().Result.ToString());
							}

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

							using (var client = new HttpClient())
							{
								var myContent = JsonConvert.SerializeObject(resta);
								var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
								var byteContent = new ByteArrayContent(buffer);
								byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

								byteContent.Headers.Add("X-Evi-Tracking-Id", idUser);

								var result = client.PostAsync("http://localhost:5000/sub", byteContent).Result;

								Console.WriteLine(result.Content.ReadAsStringAsync().Result.ToString());
							}

							break;
						//Multiply
						case 3:
							Console.WriteLine("Preparing Multiply operation");

							do
							{
								do
								{
									Console.Write("Valor: ");
									strValor1 = Console.ReadLine();
								}
								while (!float.TryParse(strValor1, out valor1));

								lista.Add(valor1);

								Console.WriteLine("Valor añadido");
								Console.WriteLine("Quieres otro valor? (s/n)");
								r = Console.ReadLine();
							}
							while (r.Equals("s"));

							Mult mult = new Mult();

							mult.Factors = lista;

							using (var client = new HttpClient())
							{
								var myContent = JsonConvert.SerializeObject(mult);
								var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
								var byteContent = new ByteArrayContent(buffer);
								byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

								byteContent.Headers.Add("X-Evi-Tracking-Id", idUser);

								var result = client.PostAsync("http://localhost:5000/mult", byteContent).Result;

								Console.WriteLine(result.Content.ReadAsStringAsync().Result.ToString());
							}

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

							using (var client = new HttpClient())
							{
								var myContent = JsonConvert.SerializeObject(div);
								var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
								var byteContent = new ByteArrayContent(buffer);
								byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

								byteContent.Headers.Add("X-Evi-Tracking-Id", idUser);

								var result = client.PostAsync("http://localhost:5000/div", byteContent).Result;

								Console.WriteLine(result.Content.ReadAsStringAsync().Result.ToString());
							}

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


							using (var client = new HttpClient())
							{
								var myContent = JsonConvert.SerializeObject(sqrt);
								var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
								var byteContent = new ByteArrayContent(buffer);
								byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

								byteContent.Headers.Add("X-Evi-Tracking-Id", idUser);

								var result = client.PostAsync("http://localhost:5000/sqrt", byteContent).Result;

								Console.WriteLine(result.Content.ReadAsStringAsync().Result.ToString());
							}

							break;
						case 6:

							if (idUser != "")
							{
								Console.WriteLine("Review history of " + idUser);

								Query query = new Query();
								query.Id = idUser;

								using (var client = new HttpClient())
								{
									var myContent = JsonConvert.SerializeObject(query);
									var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
									var byteContent = new ByteArrayContent(buffer);
									byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
									var result = client.PostAsync("http://localhost:5000/query", byteContent).Result;

									Console.WriteLine(result.Content.ReadAsStringAsync().Result.ToString());
								}
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
					Console.WriteLine("dsfds");
				}
			}
			while (valorNum != 0);

			Console.WriteLine(valorNum);
			//cuando sale se cierra todo

		}
	}
}
