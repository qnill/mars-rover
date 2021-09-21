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
            var rover = new RoverDto
            {
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
                }
            };

            RoverExploration.Discover(new List<RoverDto>() { rover });

            Assert.Equal(1, rover.Coordinate.X);
            Assert.Equal(3, rover.Coordinate.Y);
            Assert.Equal(HeadingType.N, rover.Coordinate.Heading);
        }
    }
}