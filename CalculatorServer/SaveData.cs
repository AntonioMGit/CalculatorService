using ModelContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorServer
{
	public static class SaveData
	{
		public static Dictionary<string, Operations> Data = new Dictionary<string, Operations>();

		public static void Add(string key, Operation operation)
		{
			if (Data.ContainsKey(key)) //si existe añade
			{
				List<Operation> o = Data[key].OperationList;
				o.Add(operation);
				Data[key].OperationList = o;
			}
			else //si no, crea y añade
			{
				Operations o = new Operations();
				o.OperationList = new List<Operation>();
				o.OperationList.Add(operation);

				Data.Add(key, o);
			}
		}
		public static Operations GetOperationsByKey(string key)
		{
			Operations oper = new Operations();
			oper.OperationList = new List<Operation>();
			if (Data.ContainsKey(key))//si existe añade
			{
				oper = Data[key];
			}
			return oper;
		}
	}
}
