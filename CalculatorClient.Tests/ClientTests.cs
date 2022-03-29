using NUnit.Framework;
using CalculatorClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CalculatorClient.Tests
{
	[TestFixture()]
	public class ClientTests
	{
		[Test()]
		public void MainTest()
		{
			//Assert.Fail();
		}

		[Test]
		public void TestOperationAdd()
		{
			try
			{
				var stringReader = new StringReader("4\r\n" +
													"y\r\n" +
													"5\r\n" +
													"n\r\n");
				Console.SetIn(stringReader);

				//https://makolyte.com/csharp-how-to-unit-test-code-that-reads-and-writes-to-the-console/
				var stringWriter = new StringWriter();
				Console.SetOut(stringWriter);

				Client.OperationAdd("user");

				var outputLines = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

				var output = stringWriter.ToString();
				var cosa = outputLines[outputLines.Length - 1];
				Assert.AreEqual("9", outputLines[outputLines.Length-1]);
			}
			catch (Exception e)
			{
				Assert.Fail(e.StackTrace);
			}
		}

		[Test]
		public void TestOperationSub()
		{
			try
			{
				var stringReader = new StringReader("10\r\n" +
													"1\r\n");
				Console.SetIn(stringReader);

				//https://makolyte.com/csharp-how-to-unit-test-code-that-reads-and-writes-to-the-console/
				var stringWriter = new StringWriter();
				Console.SetOut(stringWriter);

				Client.OperationSub("user");

				var outputLines = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

				var output = stringWriter.ToString();
				var cosa = outputLines[outputLines.Length - 1];
				Assert.AreEqual("9", outputLines[outputLines.Length - 1]);
			}
			catch (Exception e)
			{
				Assert.Fail(e.StackTrace);
			}
		}

		[Test]
		public void TestOperationMult()
		{
			try
			{
				var stringReader = new StringReader("3\r\n" +
													"y\r\n" +
													"3\r\n" +
													"n\r\n");
				Console.SetIn(stringReader);

				//https://makolyte.com/csharp-how-to-unit-test-code-that-reads-and-writes-to-the-console/
				var stringWriter = new StringWriter();
				Console.SetOut(stringWriter);

				Client.OperationMult("user");

				var outputLines = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

				var output = stringWriter.ToString();
				var cosa = outputLines[outputLines.Length - 1];
				Assert.AreEqual("9", outputLines[outputLines.Length - 1]);
			}
			catch (Exception e)
			{
				Assert.Fail(e.StackTrace);
			}
		}

		[Test]
		public void TestOperationDiv()
		{
			try
			{
				var stringReader = new StringReader("18\r\n" +
													"2\r\n");
				Console.SetIn(stringReader);

				//https://makolyte.com/csharp-how-to-unit-test-code-that-reads-and-writes-to-the-console/
				var stringWriter = new StringWriter();
				Console.SetOut(stringWriter);

				Client.OperationDiv("user");

				var outputLines = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

				var output = stringWriter.ToString();
				var cosa = outputLines[outputLines.Length - 1];
				Assert.AreEqual("9", outputLines[outputLines.Length - 1]);
			}
			catch (Exception e)
			{
				Assert.Fail(e.StackTrace);
			}
		}

		[Test]
		public void TestOperationSqrt()
		{
			try
			{
				var stringReader = new StringReader("81\r\n");
				Console.SetIn(stringReader);

				//https://makolyte.com/csharp-how-to-unit-test-code-that-reads-and-writes-to-the-console/
				var stringWriter = new StringWriter();
				Console.SetOut(stringWriter);

				Client.OperationSqrt("user");

				var outputLines = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

				var output = stringWriter.ToString();
				var cosa = outputLines[outputLines.Length - 1];
				Assert.AreEqual("9", outputLines[outputLines.Length - 1]);
			}
			catch (Exception e)
			{
				Assert.Fail(e.StackTrace);
			}
		}

		/*
		[Test()]
		public void ChangeUser(string idUser)
		{
			try
			{
				var stringReader = new StringReader("");
				Console.SetIn(stringReader);
				Client.ChangeUser(idUser);
				Assert.IsTrue();
			}
			catch(Exception e)
			{
				Assert.Fail(e.StackTrace);
			}
		}
		*/
	}
}