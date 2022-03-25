using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using ModelContainer;


namespace CalculatorService
{
	public class Cliente
	{
		public static void Main(string[] args)
		{
			String strValor1;
			String strValor2; // quitar y solo usar 1?
			float valor1;
			float valor2;
			float resultado;

			List<float> lista = new List<float>();

			Server nosecomollamarlo = new Server(); // mirar

			Console.WriteLine("Operation:");
			Console.WriteLine("\t1.Addition");
			Console.WriteLine("\t2.Subtraction");
			Console.WriteLine("\t3.Multiply");
			Console.WriteLine("\t4.Division");
			Console.WriteLine("\t5.Square root");
			Console.WriteLine("\t6.Review history");

			String valor = Console.ReadLine();

			int valorNum =  0;

			bool exito = Int32.TryParse(valor, out valorNum);

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
							var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
							var byteContent = new ByteArrayContent(buffer);
							byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
							var result = client.PostAsync("http://localhost:5000/add", byteContent).Result;

							Console.WriteLine(result.Content.ReadAsStringAsync().Result.ToString());
						}

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
							var result = client.PostAsync("http://localhost:5000/mult", byteContent).Result;

							Console.WriteLine(result.Content.ReadAsStringAsync().Result.ToString());
						}

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
						} while (!float.TryParse(strValor2, out valor2)||strValor2.Equals("0")); //quitar lo del 0?

						Div div = new Div();

						div.Dividend = valor1;
						div.Divisor = valor2;

						using (var client = new HttpClient())
						{
							var myContent = JsonConvert.SerializeObject(div);
							var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
							var byteContent = new ByteArrayContent(buffer);
							byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
							var result = client.PostAsync("http://localhost:5000/div", byteContent).Result;

							Console.WriteLine(result.Content.ReadAsStringAsync().Result.ToString());
						}

						break;
					case 5:
						Console.WriteLine("Square root");
						//comprobar que el valor sea mayor a 0
						do
						{
							Console.Write("Valor 1: ");
							strValor1 = Console.ReadLine();
						} while (!float.TryParse(strValor1, out valor1) || valor1 < 0);
						Console.WriteLine("Valor1: " + valor1);

						Console.WriteLine("El resultado es: " + nosecomollamarlo.SquareRoot(valor1));

						break;
					case 6:
						Console.WriteLine("Review history");
						break;
					default:
						//repetir
						break;
				}
			}
			else
			{
				//repetir
			}

			String salir = Console.ReadLine();

		}
	}
}
