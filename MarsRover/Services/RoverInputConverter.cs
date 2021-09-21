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
        public static RoverCoordinateDto Coordinate(string inputCoordinate)
        {
            RoverCoordinateDto coordinate = new();

            var coordinates = inputCoordinate.Split(" ");
            if (coordinates == null || coordinates.Length != 3)
                // coordinate verisi hatalı girildi;
                return null;

            if (Regex.IsMatch(coordinates[0], "^[0-9]*$"))
                coordinate.X = Convert.ToInt32(coordinates[0]);
            // throw error, if is a not number.

            if (Regex.IsMatch(coordinates[1], "^[0-9]*$"))
                coordinate.Y = Convert.ToInt32(coordinates[1]);
            // throw error, if is a not number.

            // Get static heading types.
            var staticHeadings = Headings.ToArray();
            if (staticHeadings.Any(a => a == coordinates[2]))
                coordinate.Heading = (Headings.HeadingType)Enum.Parse(typeof(Headings.HeadingType), coordinates[2]);
            // coordinate verisi yön hatalı girildi;

            return coordinate;
        }

        /// <summary>
        /// Parses the string move instructions input to <see cref="MoveInstructions.MoveInstructionType"/>
        /// </summary>
        /// <param name="inputMoveInstructions"></param>
        public static MoveInstructions.MoveInstructionType[] MoveInstructions(string inputMoveInstructions)
        {
            if (string.IsNullOrWhiteSpace(inputMoveInstructions))
                // moveInstructions verisi boş burakılamaz.
                return null;

            // Get static move instructions types.
            var staticMoveInstructionsTypes = Const.MoveInstructions.ToArray();

            var moveInstructions = inputMoveInstructions.ToCharArray();

            var result = new MoveInstructions.MoveInstructionType[moveInstructions.Length];
            for (int i = 0; i < moveInstructions.Length; i++)
            {
                string moveInstruction = moveInstructions[i].ToString();
                if (!staticMoveInstructionsTypes.Any(a => a == moveInstruction))
                    continue;
                // throw error, not find move instruction type.

                result[i] = (MoveInstructions.MoveInstructionType)Enum.Parse(typeof(MoveInstructions.MoveInstructionType), moveInstruction);
            }

            return result;
        }
    }
}