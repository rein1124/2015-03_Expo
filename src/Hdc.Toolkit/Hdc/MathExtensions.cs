namespace Hdc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MathExtensions
    {
        public static int[] Int32ToInt32Array16(this int y)
        {
            var output = new int[16];
            y = y % 65536;
            if (y > 32767)
            {
                output[15] = 1;
                y -= 32768;
            }
            else
                output[15] = 0;
            if (y > 16383)
            {
                output[14] = 1;
                y -= 16384;
            }
            else
                output[14] = 0;
            if (y > 8191)
            {
                output[13] = 1;
                y -= 8192;
            }
            else
                output[13] = 0;
            if (y > 4095)
            {
                output[12] = 1;
                y -= 4096;
            }
            else
                output[12] = 0;
            if (y > 2047)
            {
                output[11] = 1;
                y -= 2048;
            }
            else
                output[11] = 0;
            if (y > 1023)
            {
                output[10] = 1;
                y -= 1024;
            }
            else
                output[10] = 0;
            if (y > 511)
            {
                output[9] = 1;
                y -= 512;
            }
            else
                output[9] = 0;
            if (y > 255)
            {
                output[8] = 1;
                y -= 256;
            }
            else
                output[8] = 0;
            if (y > 127)
            {
                output[7] = 1;
                y -= 128;
            }
            else
                output[7] = 0;
            if (y > 63)
            {
                output[6] = 1;
                y -= 64;
            }
            else
                output[6] = 0;
            if (y > 31)
            {
                output[5] = 1;
                y -= 32;
            }
            else
                output[5] = 0;
            if (y > 15)
            {
                output[4] = 1;
                y -= 16;
            }
            else
                output[4] = 0;
            if (y > 7)
            {
                output[3] = 1;
                y -= 8;
            }
            else
                output[3] = 0;
            if (y > 3)
            {
                output[2] = 1;
                y -= 4;
            }
            else
                output[2] = 0;
            if (y > 1)
            {
                output[1] = 1;
                y -= 2;
            }
            else
                output[1] = 0;
            if (y > 0)
                output[0] = 1; /*y -= 1;*/
            else
                output[0] = 0;
            return output;
        }

        public static int Int32Array16ToInt32(this int[] input)
        {
            if (input.Length < 16)
                throw new ArgumentException("int[] input.Length<16, Int32Array16ToInt32 ONLY convert int[16] to int");
            int output = input[0];
            int n = 1;
            for (int i = 1; i < 16; i++)
            {
                n = n * 2;
                output += input[i] * n;
            }
            return output;
        }
    }
}