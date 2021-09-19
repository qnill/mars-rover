using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover.Const
{
    public class Headings
    {
        public enum HeadingType : byte
        {
            N = 1,
            E = 2,
            S = 3,
            W = 4
        }

        public static string[] ToArray()
        {
            return Enum.GetNames(typeof(HeadingType));
        }

        public static Dictionary<byte, string> ToDictionary()
        {
            return Enum
                .GetValues(typeof(HeadingType))
                .Cast<HeadingType>()
                .ToDictionary(t => (byte)t, t => t.ToString());
        }
    }
}