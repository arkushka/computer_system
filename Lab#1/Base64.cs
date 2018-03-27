using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Base64
{
    class Program
    {
        static void Main(string[] args)
        {
            const string base64alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            List<byte> bytes = new List<byte>();
            int c;
            string end = "";
            using (BinaryReader br = new BinaryReader(new FileStream(args[0], FileMode.Open)))
            {
                for (long i = 0; i < br.BaseStream.Length; i++)
                    bytes.Add(br.ReadByte());
            }
            c = bytes.Count % 3;
            if (c > 0)
                for (; c < 3; c++)
                {
                    bytes.Add(0);
                    end+= "=";
                }

            string foo = "";

            for (int i = 0; i < bytes.Count; i = i + 3)
            {
                foo += base64alphabet[bytes[i] >> 2];
                int secondbyte = ((bytes[i] & 3) << 4) | (bytes[i + 1] >> 4);
                foo += base64alphabet[secondbyte];
                int thirdbyte = ((bytes[i + 1] & 15) << 2) | (bytes[i + 2] >> 6);
                foo += base64alphabet[thirdbyte];
                foo += base64alphabet[bytes[i + 2] & 63];
            }
            foo = foo.Substring(0, foo.Length - end.Length) + end;
            using (StreamWriter swrtr = new StreamWriter(new FileStream(args[1], FileMode.Create)))
            {
                swrtr.Write(foo);
            }
        }
    }
}
