using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WDU;

internal class Utils
{
    public static byte[] empty = new byte[]
    {
        5,
        0,
        0,
        0,
        69,
        109,
        112,
        116,
        121,
        0,
        0,
        0,
        30,
        0,
        0,
        0,
        69,
        109,
        112,
        116,
        121,
        69,
        109,
        112,
        116,
        121,
        69,
        109,
        112,
        116,
        121,
        69,
        109,
        112,
        116,
        121,
        69,
        109,
        112,
        116,
        121,
        69,
        109,
        112,
        116,
        121,
        0,
        0,
        0,
        0,
        0,
        0
    };

    public static string GetString(byte[] bytes)
    {
        char[] array = new char[bytes.Length / 2];
        Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
        return new string(array);
    }

    public static short ReadreversedInt16(byte[] b)
    {
        Array.Reverse(b, 0, b.Length);
        return BitConverter.ToInt16(b, 0);
    }

    public static int ReadreversedInt32(byte[] b)
    {
        Array.Reverse(b, 0, b.Length);
        return BitConverter.ToInt32(b, 0);
    }

    public static long ReadreversedInt64(byte[] b)
    {
        Array.Reverse(b, 0, b.Length);
        return BitConverter.ToInt64(b, 0);
    }

    public static byte[] ReversedInt16Bytes(short i)
    {
        byte[] bytes = BitConverter.GetBytes(i);
        Array.Reverse(bytes, 0, bytes.Length);
        return bytes;
    }

    public static byte[] String2Bytes(string str)
    {
        return Encoding.ASCII.GetBytes(str + "\0");
    }

    public static byte[] ReversedInt32Bytes(int i)
    {
        byte[] bytes = BitConverter.GetBytes(i);
        Array.Reverse(bytes, 0, bytes.Length);
        return bytes;
    }

    public static byte[] ReversedInt64Bytes(long i)
    {
        byte[] bytes = BitConverter.GetBytes(i);
        Array.Reverse(bytes, 0, bytes.Length);
        return bytes;
    }

    public static void ReadFile(byte[] bytes, FileStream fs)
    {
        int i = bytes.Length;
        int num = 0;
        while (i > 0)
        {
            int num2 = fs.Read(bytes, num, i);
            num += num2;
            i -= num2;
        }
    }

    public static string ReadStringToNull(BinaryReader reader)
    {
        string text = string.Empty;
        byte b;
        while ((b = reader.ReadByte()) > 0)
        {
            text += (char)b;
        }
        return text;
    }

    public static int Padding(int i, int l)
    {
        int num = i % l;
        if (num > 0)
        {
            return l - num;
        }
        return 0;
    }

    public static void WriteStreamWithPadding(BinaryWriter memWriter, byte[] b, int l)
    {
        int num = b.Length % l;
        memWriter.Write(b);
        if (num > 0)
        {
            memWriter.Write(new byte[l - num]);
        }
    }

    public static string GetMd5Hash(string input)
    {
        byte[] array = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < array.Length; i++)
        {
            stringBuilder.Append(array[i].ToString("x2"));
        }
        return stringBuilder.ToString();
    }
}
