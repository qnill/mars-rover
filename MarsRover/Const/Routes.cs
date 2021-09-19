using System;

namespace MarsRover.Const
{
    public class Routes
    {
        public enum RouteType : byte
        {
            M = 1,
            L = 2,
            R = 3
        }

        public static string[] ToArray()
        {
            return Enum.GetNames(typeof(RouteType));
        }
    }
}