using System;
using System.Collections.Generic;
using System.IO;

namespace Info
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, double> frequencies = new Dictionary<char, double>();
            int charCount = 0;
            using (StreamReader sr = new StreamReader(new FileStream(args[0], FileMode.Open)))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    charCount += line.Length;
                    for (int i = 0; i < line.Length; ++i)
                    {
                        double val;
                        if (frequencies.TryGetValue(line[i], out val))
                        {
                            frequencies[line[i]] = val + 1.0;
                        }
                        else
                        {
                            frequencies.Add(line[i], 1.0);
                        }
                    }
                }
            }
            List<char> keys = new List<char>(frequencies.Keys);
            for (int i = 0; i < keys.Count; ++i)
            {
                frequencies[keys[i]] /= charCount;
            }

            double ent = 0.0;
            foreach (char key in frequencies.Keys)
            {
                ent += frequencies[key] * Math.Log(frequencies[key], 2.0);
            }
            ent *= -1;

            double infoAmount = ent * charCount/8;

            foreach (char key in frequencies.Keys)
            {
                Console.WriteLine(key + " - " + frequencies[key]);
            }
            Console.WriteLine();
            Console.WriteLine("Enthropy: " + ent);
            Console.WriteLine("Info amount: " + infoAmount);
            Console.WriteLine("File size: " + new FileInfo(args[0]).Length);
        }
    }
}
