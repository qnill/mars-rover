using MarsRover.Const;
using MarsRover.Dtos;
using System;
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
        /// Parses the string rover input to <see cref="RoverDto"/>
        /// </summary>
        /// <param name="inputMoveInstructions"></param>
        public static RoverDto Set(string inputCoordinate, string inputMoveInstructions)
        {
            RoverDto rover = new()
            {
                Success = false
            };

            (rover.Coordinate, rover.Message) = Coordinate(inputCoordinate);
            if (rover.Message != null)
                return rover;

            (rover.MoveInstructions, rover.Message) = MoveInstructions(inputMoveInstructions);

            rover.Success = rover.Message == null;
            return rover;
        }
    }
}