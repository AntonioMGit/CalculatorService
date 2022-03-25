using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService
{
	class Server
	{
		/*
		static void Main(string[] args)
		{
		}
		*/

		//addition
		//subtraction
		//multiply
		//division
		//square root
		//review history of requested operations

		public float Adition(List<float> lista)
		{
			return lista.Sum();
		}
		public float Subtraction(float valor1, float valor2)
		{
			return valor1-valor2;
		}
		public float Multiply(float valor1, float valor2)
		{
			return valor1*valor2;
		}
		public float Division(float valor1, float valor2)
		{
			return valor1/valor2;
		}
		public float SquareRoot(float valor1)
		{
			return (float)Math.Sqrt(valor1);
		}
	}
}
