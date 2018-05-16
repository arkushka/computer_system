using System;

namespace Lab2_part1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Multiply(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())));
            Console.ReadKey();
        }

        static long Multiply(int multiplicand, int multiplier)
        {
            bool multiplierIsNegtive = multiplier < 0;
            bool multiplicandIsNegative = multiplicand < 0;

            if (multiplicandIsNegative)
                multiplicand = ~multiplicand + 1;

            if (multiplierIsNegtive)
                multiplier = ~multiplier + 1;

            long product = multiplier;

            for (int i = 0; i < 32; i++)
            {
                if ((product & 1) == 1)
                    product = (product & 0xFFFFFFFF) | (multiplicand + (product >> 32)) << 32;

                product >>= 1;

                Console.WriteLine("Multiplicand: {0,-64} Multiplier: {1,-32}",
                        Convert.ToString(multiplicand, 2), Convert.ToString(multiplier, 2));
            }

            return multiplicandIsNegative ^ multiplierIsNegtive ? ~product + 1 : product;
        }
    }
}
