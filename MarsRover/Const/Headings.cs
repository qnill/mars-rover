using System;

namespace MarsRover.Const
{
    public class Headings
    {
        public enum HeadingType : byte
        {
            N = 0,
            E = 1,
            S = 2,
            W = 3
        }

        public static string[] ToArray()
        {
            return Enum.GetNames(typeof(HeadingType));
        }
    }
}