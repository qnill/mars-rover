using MarsRover.Const;
using MarsRover.Dtos;
using System.Collections.Generic;

namespace MarsRover.Services
{
    public class RoverExploration
    {
        /// <summary>
        /// Rover, moves in the heading it has.
        /// </summary>
        /// <param name="coordinate"></param>
        private static void Move(RoverCoordinateDto coordinate)
        {
            switch (coordinate.Heading)
            {
                case Headings.HeadingType.N:
                    coordinate.Y++;
                    break;
                case Headings.HeadingType.E:
                    coordinate.X++;
                    break;
                case Headings.HeadingType.S:
                    coordinate.Y--;
                    break;
                case Headings.HeadingType.W:
                    coordinate.X--;
                    break;
            }
        }

        /// <summary>
        /// Moves rovers for exploration.
        /// </summary>
        /// <param name="rovers"></param>
        public static void Discover(IList<RoverDto> rovers)
        {
            int headingLength = Headings.ToArray().Length;

            foreach (var rover in rovers)
            {
                int headingIndex = (int)rover.Coordinate.Heading;

                // For new heading, when the rotation instructions comes the index value of heading array is change +/- 1.
                // When the move instructions comes, call move service with the new coordinate value.
                foreach (var instruction in rover.MoveInstructions)
                {
                    if (instruction == MoveInstructions.MoveInstructionType.M)
                        Move(rover.Coordinate);
                    else
                    {
                        if (instruction == MoveInstructions.MoveInstructionType.L)
                            headingIndex--;
                        else if (instruction == MoveInstructions.MoveInstructionType.R)
                            headingIndex++;

                        headingIndex = (headingIndex + headingLength) % headingLength;
                        rover.Coordinate.Heading = (Headings.HeadingType)headingIndex;
                    }
                }
            }
        }
    }
}