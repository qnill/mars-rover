using MarsRover.Const;
using MarsRover.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MarsRover.Services
{
    public class RoverInputConverter
    {
        /// <summary>
        /// Parses the string coordinate input to <see cref="RoverCoordinateDto"/>.
        /// </summary>
        /// <param name="inputCoordinate"></param>
        private static (RoverCoordinateDto coordinate, string message) Coordinate(string inputCoordinate)
        {
            RoverCoordinateDto coordinate = new();

            var coordinates = inputCoordinate.Split(" ");
            if (coordinates == null || coordinates.Length != 3)
                return (null, ResultMessages.RoverInputConvert.RIE0002);

            if (!Regex.IsMatch(coordinates[0], "^[0-9]*$") || !Regex.IsMatch(coordinates[1], "^[0-9]*$"))
                return (null, ResultMessages.RoverInputConvert.RIE0003);

            // Get static heading types.
            var staticHeadings = Headings.ToArray();
            if (!staticHeadings.Any(a => a == coordinates[2]))
                return (null, ResultMessages.RoverInputConvert.RIE0004);

            coordinate.X = Convert.ToInt32(coordinates[0]);
            coordinate.Y = Convert.ToInt32(coordinates[1]);
            coordinate.Heading = (Headings.HeadingType)Enum.Parse(typeof(Headings.HeadingType), coordinates[2]);

            return (coordinate, null);
        }

        /// <summary>
        /// Parses the string move instructions input to <see cref="MoveInstructions.MoveInstructionType"/>
        /// </summary>
        /// <param name="inputMoveInstructions"></param>
        private static (MoveInstructions.MoveInstructionType[] moveInstructions, string message) MoveInstructions(string inputMoveInstructions)
        {
            if (string.IsNullOrWhiteSpace(inputMoveInstructions))
                return (null, ResultMessages.RoverInputConvert.RIE0001);

            // Get static move instructions types.
            var staticMoveInstructionsTypes = Const.MoveInstructions.ToArray();

            var moveInstructions = inputMoveInstructions.ToCharArray();

            var result = new MoveInstructions.MoveInstructionType[moveInstructions.Length];
            for (int i = 0; i < moveInstructions.Length; i++)
            {
                string moveInstruction = moveInstructions[i].ToString();
                if (!staticMoveInstructionsTypes.Any(a => a == moveInstruction))
                    return (null, ResultMessages.RoverInputConvert.RIE0005);

                result[i] = (MoveInstructions.MoveInstructionType)Enum.Parse(typeof(MoveInstructions.MoveInstructionType), moveInstruction);
            }

            return (result, null);
        }

        /// <summary>
        /// Parses the string upper right coordinates input to <see cref="PlateauDto"/>.
        /// </summary>
        /// <param name="inputUpperRightCoordinates"></param>
        public static (PlateauDto plateau, string message) Plateau(string inputUpperRightCoordinates)
        {
            PlateauDto plateau = new();

            var upperRightCoordinates = inputUpperRightCoordinates.Split(" ");
            if (upperRightCoordinates == null || upperRightCoordinates.Length != 2)
                return (null, ResultMessages.RoverInputConvert.RIE0008);

            if (!Regex.IsMatch(upperRightCoordinates[0], "^[0-9]*$") || !Regex.IsMatch(upperRightCoordinates[1], "^[0-9]*$"))
                return (null, ResultMessages.RoverInputConvert.RIE0009);

            plateau.UpperRightX = Convert.ToInt32(upperRightCoordinates[0]);
            plateau.UpperRightY = Convert.ToInt32(upperRightCoordinates[1]);

            return (plateau, null);
        }

        /// <summary>
        /// Parses the string rover input to <see cref="RoverDto"/>
        /// </summary>
        /// <param name="inputs"></param>
        public static IList<RoverDto> Set(PlateauDto plateau, IList<(int roverId, string inputCoordinate, string inputMoveInstructions)> inputs)
        {
            List<RoverDto> rovers = new();

            foreach (var (roverId, inputCoordinate, inputMoveInstructions) in inputs)
            {
                RoverDto rover = new()
                {
                    Id = roverId
                };

                (rover.Coordinate, rover.Message) = Coordinate(inputCoordinate);
                if (rover.Coordinate != null)
                {
                    (rover.MoveInstructions, rover.Message) = MoveInstructions(inputMoveInstructions);

                    // Checks if there are any more rovers added to these coordinates.
                    if (rovers.Any(a => a.Coordinate.X == rover.Coordinate.X && a.Coordinate.Y == rover.Coordinate.Y))
                        rover.Message = ResultMessages.RoverInputConvert.RIE0007;

                    // Checks if these coordinates are within the plateau.
                    if (rover.Coordinate.X > plateau.UpperRightX || rover.Coordinate.Y > plateau.UpperRightY)
                        rover.Message = ResultMessages.RoverInputConvert.RIE0006;
                }

                rover.Success = rover.Message == null;
                rovers.Add(rover);
            }

            return rovers;
        }
    }
}