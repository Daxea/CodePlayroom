using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePlayroom.Math
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Gets whether the integer <paramref name="value"/> is within the range <paramref name="min"/>..<paramref name="max"/>.
        /// </summary>
        /// <param name="value">The integer to test.</param>
        /// <param name="min">The minimum bound.</param>
        /// <param name="max">The maximum bound.</param>
        /// <param name="inclusion">If Exclusive, the value must be within the range min...max (less than, but not equal to, max).</param>
        /// <returns></returns>
        public static bool IsWithin(this int value, int min, int max, BoundInclusion inclusion = BoundInclusion.Inclusive)
        {
            if (inclusion == BoundInclusion.Exclusive)
                max--;
            return value >= min && value <= max;
        }
    }
}