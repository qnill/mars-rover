using MarsRover.Const;
using MarsRover.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover.Services
{
    public class RoverExploration
    {
        private static IList<RoverDto> _rovers = new List<RoverDto>();

        /// <summary>
        /// Checks the direction of rover will move before it moves.
        /// </summary>
        private static void CheckDirection(PlateauDto plateau, int virtualX, int virtualY, RoverDto rover)
        {
            // Checks the status of being out of plateau.
            if (virtualX < 0 || virtualX > plateau.UpperRightX || virtualY < 0 || virtualY > plateau.UpperRightY)
            {
                rover.Message = string.Format(ResultMessages.RoverExploration.REE0001, $"{rover.Coordinate.X} {rover.Coordinate.Y} {rover.Coordinate.Heading}");
                rover.Success = false;
                return;
            }

            // Checks the crash to another rover.
            var obstacleRover = _rovers.Where(a => a.Coordinate.X == virtualX && a.Coordinate.Y == virtualY).Select(s => s.Id.ToString()).FirstOrDefault();
            if (obstacleRover != null)
            {
                rover.Message = string.Format(ResultMessages.RoverExploration.REE0002, obstacleRover, $"{rover.Coordinate.X} {rover.Coordinate.Y} {rover.Coordinate.Heading}");
                rover.Success = false;
                return;
            }
        }

        /// <summary>
        /// Rover, moves in the heading it has.
        /// </summary>
        /// <param name="coordinate"></param>
        private static void Move(PlateauDto plateau, RoverDto rover)
        {
            var coordinate = rover.Coordinate;

            switch (coordinate.Heading)
            {
                case Headings.HeadingType.N:
                    {
                        CheckDirection(plateau, coordinate.X, rover.Coordinate.Y + 1, rover);
                        if (rover.Success)
                            coordinate.Y++;
                        break;
                    }
                case Headings.HeadingType.E:
                    {
                        CheckDirection(plateau, coordinate.X + 1, rover.Coordinate.Y, rover);
                        if (rover.Success)
                            coordinate.X++;
                        break;
                    }
                case Headings.HeadingType.S:
                    {
                        CheckDirection(plateau, coordinate.X, rover.Coordinate.Y - 1, rover);
                        if (rover.Success)
                            coordinate.Y--;
                        break;
                    }
                case Headings.HeadingType.W:
                    {
                        CheckDirection(plateau, coordinate.X - 1, rover.Coordinate.Y, rover);
                        if (rover.Success)
                            coordinate.X--;
                        break;
                    }
            }
        }

        /// <summary>
        /// Moves rovers for exploration.
        /// </summary>
        /// <param name="rovers"></param>
        public static void Discover(PlateauDto plateau, IList<RoverDto> rovers)
        {
            _rovers = rovers;

            int headingLength = Headings.ToArray().Length;

            foreach (var rover in rovers)
            {
                int headingIndex = (int)rover.Coordinate.Heading;

                // For new heading, when the rotation instructions comes the index value of heading array is change +/- 1.
                // When the move instructions comes, call move service with the new coordinate value.
                foreach (var instruction in rover.MoveInstructions)
                {
                    if (instruction == MoveInstructions.MoveInstructionType.M)
                        Move(plateau, rover);
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