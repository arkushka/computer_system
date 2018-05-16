using System;

namespace Lab2_part2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Divide(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())));
            Console.ReadKey();
        }

        static long Divide(int dividend, int divisor)
        {
            int quotient = 0;
            long remainder = 0, longDivisor;

            bool isDividendNegative = dividend < 0;
            bool isDivisorNegative = divisor < 0;

            longDivisor = divisor << 16;

            if (isDividendNegative)
                dividend = ~dividend + 1;

            if (isDivisorNegative)
                longDivisor = ~longDivisor + 1;

            remainder = dividend;

            for (int i = 0; i < 17; i++)
            {
                remainder -= longDivisor;

                if (remainder >= 0)
                {
                    quotient <<= 1;
                    quotient |= 1;
                }
                else
                {
                    quotient <<= 1;
                    remainder += longDivisor;
                }

                longDivisor >>= 1;
            }

            return isDivisorNegative ^ isDividendNegative ? ~quotient + 1 : quotient;
        }
    }
}
