using System;

namespace MarsRover.Const
{
    public class MoveInstructions
    {
        public enum MoveInstructionType : byte
        {
            M = 1,
            L = 2,
            R = 3
        }

        public static string[] ToArray()
        {
            return Enum.GetNames(typeof(MoveInstructionType));
        }
    }
}