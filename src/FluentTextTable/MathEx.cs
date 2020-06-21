using System;

namespace FluentTextTable
{
    public static class MathEx
    {
        private static readonly int IntSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(int));

        /// <summary>
        /// Least common multiple
        /// </summary>
        /// <returns></returns>
        internal static int Lcm(params int[] values) {
            if (values.Length == 2)
            {
                return values[0] * values[1] / Gcd(values[0], values[1]);
            }
            else
            {
                var newValues = new int[values.Length - 1];
                Buffer.BlockCopy(values, 0, newValues, 0, newValues.Length * IntSize);
                return Lcm(Lcm(newValues), values[values.Length - 1]);
            }
        }

        /// <summary>
        /// Euclidean algorithm
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private static int Gcd(int left, int right) {
            if (left < right)
                return Gcd(right, left);
            while (right != 0) {
                var remainder = left % right;
                left = right;
                right = remainder;
            }
            return left;
        }

    }
}