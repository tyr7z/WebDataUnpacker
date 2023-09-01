using System;
using System.IO;

namespace WDU;

internal class Program
{
	private static void Main(string[] args)
	{
		if (args.Length <= 0)
		{
			return;
		}
		string text = args[0];
		WebData webData = new WebData();
		if (!File.Exists(text))
		{
			return;
		}
		try
		{
			using FileStream input = File.Open(text, FileMode.Open);
			using BinaryReader file = new BinaryReader(input);
			webData.Load(file, text);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
	/*
	private static byte[] StreamToByteArray(Stream ms)
	{
		byte[] array = new byte[ms.Length];
		ms.Seek(0L, SeekOrigin.Begin);
		ms.Read(array, 0, array.Length);
		return array;
	}

	private static byte[] GetBytes(string str)
	{
		byte[] array = new byte[str.Length * 2];
		Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
		return array;
	}
	*/
}
