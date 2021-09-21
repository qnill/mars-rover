using MarsRover.Dtos;
using MarsRover.Services;
using System;
using System.Collections.Generic;

namespace MarsRover
{
    class Program
    {
        static void Main()
        {
            Console.Write("upper-right coordinates: ");
            string upperRightCoordinates = Console.ReadLine();

            List<RoverDto> rovers = new();
            bool addMoreRover = true;
            while (addMoreRover)
            {
                RoverDto rover = new();

                Console.Write("rover start coordinate: ");
                string startCoordinate = Console.ReadLine();
                rover.Coordinate = RoverInputConverter.Coordinate(startCoordinate);

                Console.Write("rover exploration instructions: ");
                string moveInstructions = Console.ReadLine();
                rover.MoveInstructions = RoverInputConverter.MoveInstructions(moveInstructions);

                rovers.Add(rover);

                Console.Write("Do you want to add another rover? (Y/n): ");
                string addMoreRoverAnswer = Console.ReadLine();
                addMoreRover = addMoreRoverAnswer.ToUpper() == "Y" || addMoreRoverAnswer == string.Empty;
            }

            RoverExploration.Discover(rovers);
            foreach (var rover in rovers)
            {
                string lastCoordinate = $"{rover.Coordinate.X} {rover.Coordinate.Y} {rover.Coordinate.Heading}";
                Console.WriteLine(lastCoordinate);
            }
        }
    }
}