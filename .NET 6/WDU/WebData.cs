using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WDU;

internal class WebData
{
	public static string SIGNATURE = "UnityWebData1.0";

	public static WebData? instance;

	private string? signature;

	private uint? headLen;

	private readonly List<data_prop> nodes = new List<data_prop>();

	private string? path;

	public void Load(BinaryReader file, string _name)
	{
		instance = this;
		signature = Utils.ReadStringToNull(file);
		headLen = file.ReadUInt32();
		path = _name + "_dump/";
		Console.WriteLine("headLen:" + headLen);
		while (file.BaseStream.Position < headLen)
		{
			nodes.Add(new data_prop(file.ReadUInt32(), file.ReadUInt32(), Encoding.Default.GetString(file.ReadBytes(file.ReadInt32()))));
		}
		foreach (data_prop node in nodes)
		{
			Console.WriteLine("offset:" + node.ofs);
			Console.WriteLine("size:" + node.size);
			Console.WriteLine("name:" + node.name);
		}
		Dump(file);
	}

	public void Dump(BinaryReader br)
	{
		if (!Directory.Exists(path))
		{
			if (path != null) Directory.CreateDirectory(path);
        }
		foreach (data_prop node in nodes)
		{
            string? directoryName = Path.GetDirectoryName(path + node.name);
			if (!Directory.Exists(directoryName))
			{
				if (directoryName != null) Directory.CreateDirectory(directoryName);
            }
			using FileStream fileStream = File.Open(path + node.name, FileMode.OpenOrCreate);
			fileStream.SetLength(0L);
			using BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			br.BaseStream.Position = node.ofs;
			if (node.size > 26214400)
			{
				long num = 0L;
				while (num < node.size)
				{
					if (node.size - num < 10485760)
					{
						byte[] buffer = br.ReadBytes((int)(node.size - num));
						binaryWriter.Write(buffer);
						num += node.size - num;
					}
					else
					{
						byte[] buffer = br.ReadBytes(10485760);
						binaryWriter.Write(buffer);
						num += 10485760;
					}
				}
			}
			else
			{
				byte[] buffer = br.ReadBytes((int)node.size);
				binaryWriter.Write(buffer);
			}
		}
	}
}
