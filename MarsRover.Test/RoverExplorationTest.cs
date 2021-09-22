using MarsRover.Dtos;
using MarsRover.Services;
using System.Collections.Generic;
using Xunit;
using static MarsRover.Const.Headings;
using static MarsRover.Const.MoveInstructions;

namespace MarsRover.Test
{
    public class RoverExplorationTest
    {
        [Fact]
        public void NewLocationIsCorrect()
        {
            var plateau = new PlateauDto
            {
                UpperRightX = 5,
                UpperRightY = 5
            };
            var rover = new RoverDto
            {
                Id = 1,
                Coordinate = new RoverCoordinateDto
                {
                    X = 1,
                    Y = 2,
                    Heading = HeadingType.N
                },
                MoveInstructions = new MoveInstructionType[]
                {
                    MoveInstructionType.L,
                    MoveInstructionType.M,
                    MoveInstructionType.L,
                    MoveInstructionType.M,
                    MoveInstructionType.L,
                    MoveInstructionType.M,
                    MoveInstructionType.L,
                    MoveInstructionType.M,
                    MoveInstructionType.M
                },
                Success = true
            };

            RoverExploration.Discover(plateau, new List<RoverDto>() { rover });

            Assert.Equal(1, rover.Coordinate.X);
            Assert.Equal(3, rover.Coordinate.Y);
            Assert.Equal(HeadingType.N, rover.Coordinate.Heading);
        }

        [Fact]
        public void DrawSquare()
        {
            var plateau = new PlateauDto
            {
                UpperRightX = 20,
                UpperRightY = 20
            };
            var rover = new RoverDto
            {
                Id = 1,
                Coordinate = new RoverCoordinateDto
                {
                    X = 3,
                    Y = 2,
                    Heading = HeadingType.E
                },
                MoveInstructions = new MoveInstructionType[]
                {
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.L,
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.L,
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.L,
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.L
                },
                Success = true
            };

            RoverExploration.Discover(plateau, new List<RoverDto>() { rover });

            Assert.Equal(3, rover.Coordinate.X);
            Assert.Equal(2, rover.Coordinate.Y);
            Assert.Equal(HeadingType.E, rover.Coordinate.Heading);
        }
    }
}