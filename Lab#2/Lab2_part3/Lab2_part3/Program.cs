using System;

namespace Lab2_part3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Multiply result: {0}",
                Multiply(float.Parse(Console.ReadLine()), float.Parse(Console.ReadLine())));
            Console.ReadKey();
        }

        static float Multiply(float multiplicand, float multiplier)
        {
            if (multiplicand == 0 || multiplier == 0)
                return 0f;

            long multiplicandInInt = BitConverter.ToInt32(BitConverter.GetBytes(multiplicand), 0);
            long multiplierInInt = BitConverter.ToInt32(BitConverter.GetBytes(multiplier), 0);

            long exponent = ((multiplicandInInt & 0x7F800000) >> 23) +
                ((multiplierInInt & 0x7F800000) >> 23) - 127; // 127 - bias
            long sign = (multiplicandInInt >> 31) ^ (multiplierInInt >> 31);
            long mantisaLong = MantissaMultiply(multiplicandInInt & 0x7FFFFF | 0x800000,
                multiplierInInt & 0x7FFFFF | 0x800000);

            int mantisa = 0;

            if ((mantisaLong & 0x800000000000) == 0x800000000000)
                exponent++;
            else
                mantisaLong <<= 1;

            for (int i = 0; i < 24; i++)
            {
                if ((mantisaLong & 0x1000000) == 0x1000000)
                    mantisa |= 0x800000;

                if (i == 23)
                    break;

                mantisa >>= 1;
                mantisaLong >>= 1;
            }

            mantisa &= ~(1 << 23);

            return BitConverter.ToSingle(
                BitConverter.GetBytes(sign << 31 | (exponent << 23) | mantisa), 0);
        }
        static long MantissaMultiply(long multiplicand, long multiplier)
        {
            bool isMultiplierNegative = multiplier < 0;

            if (isMultiplierNegative)
                multiplier = ~multiplier + 1;

            long shiftedMultiplicand = multiplicand;
            long product = 0;

            shiftedMultiplicand <<= 32;

            for (int i = 0; i < 32; i++)
            {
                if ((multiplier & 1) == 1)
                    product += shiftedMultiplicand;

                product >>= 1;
                multiplier >>= 1;
            }

            if (isMultiplierNegative)
                product = ~product + 1;

            return product;
        }
    }
}
