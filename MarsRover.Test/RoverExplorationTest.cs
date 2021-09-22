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

        [Fact]
        public void Crash()
        {
            var plateau = new PlateauDto
            {
                UpperRightX = 5,
                UpperRightY = 5
            };

            var roverOne = new RoverDto
            {
                Id = 1,
                Coordinate = new RoverCoordinateDto
                {
                    X = 1,
                    Y = 3,
                    Heading = HeadingType.N
                },
                MoveInstructions = new MoveInstructionType[]
                {
                    MoveInstructionType.M,
                    MoveInstructionType.M
                },
                Success = true
            };
            var roverTwo = new RoverDto
            {
                Id = 2,
                Coordinate = new RoverCoordinateDto
                {
                    X = 2,
                    Y = 4,
                    Heading = HeadingType.S
                },
                MoveInstructions = new MoveInstructionType[]
                {
                    MoveInstructionType.M,
                    MoveInstructionType.R,
                    MoveInstructionType.M,
                    MoveInstructionType.R,
                    MoveInstructionType.M,
                    MoveInstructionType.M
                },
                Success = true
            };
            var roverThree = new RoverDto
            {
                Id = 3,
                Coordinate = new RoverCoordinateDto
                {
                    X = 1,
                    Y = 1,
                    Heading = HeadingType.N
                },
                MoveInstructions = new MoveInstructionType[]
                {
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.R,
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.L,
                    MoveInstructionType.L,
                    MoveInstructionType.L,
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.M
                },
                Success = true
            };

            RoverExploration.Discover(plateau, new List<RoverDto>() { roverOne, roverTwo, roverThree });

            // RoverOne
            Assert.Equal(1, roverOne.Coordinate.X);
            Assert.Equal(5, roverOne.Coordinate.Y);
            Assert.Equal(HeadingType.N, roverOne.Coordinate.Heading);

            // RoverTwo
            Assert.Equal(1, roverTwo.Coordinate.X);
            Assert.Equal(4, roverTwo.Coordinate.Y);
            Assert.Equal(HeadingType.N, roverTwo.Coordinate.Heading);

            // RoverThree
            Assert.Equal(1, roverThree.Coordinate.X);
            Assert.Equal(3, roverThree.Coordinate.Y);
            Assert.Equal(HeadingType.N, roverThree.Coordinate.Heading);
        }

        [Fact]
        public void OutOfPlateau()
        {
            var plateau = new PlateauDto
            {
                UpperRightX = 3,
                UpperRightY = 3
            };

            var roverOne = new RoverDto
            {
                Id = 1,
                Coordinate = new RoverCoordinateDto
                {
                    X = 1,
                    Y = 0,
                    Heading = HeadingType.E
                },
                MoveInstructions = new MoveInstructionType[]
                {
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.M
                },
                Success = true
            };
            var roverTwo = new RoverDto
            {
                Id = 2,
                Coordinate = new RoverCoordinateDto
                {
                    X = 0,
                    Y = 3,
                    Heading = HeadingType.N
                },
                MoveInstructions = new MoveInstructionType[]
                {
                    MoveInstructionType.R,
                    MoveInstructionType.R,
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.M,
                    MoveInstructionType.M
                },
                Success = true
            };

            RoverExploration.Discover(plateau, new List<RoverDto>() { roverOne, roverTwo });

            // RoverOne
            Assert.Equal(3, roverOne.Coordinate.X);
            Assert.Equal(0, roverOne.Coordinate.Y);
            Assert.Equal(HeadingType.E, roverOne.Coordinate.Heading);

            // RoverTwo
            Assert.Equal(0, roverTwo.Coordinate.X);
            Assert.Equal(0, roverTwo.Coordinate.Y);
            Assert.Equal(HeadingType.S, roverTwo.Coordinate.Heading);
        }
    }
}