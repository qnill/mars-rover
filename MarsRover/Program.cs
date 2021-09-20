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

                Console.Write("rover coordinate: ");
                string coordinate = Console.ReadLine();
                rover.Coordinate = RoverDataConvert.Coordinate(coordinate);

                Console.Write("rover exploration instructions: ");
                string explorationInstructions = Console.ReadLine();
                rover.ExplorationInstructions = RoverDataConvert.ExplorationInstructions(explorationInstructions);

                rovers.Add(rover);

                Console.Write("Do you want to add another rover? (Y/n): ");
                string addMoreRoverAnswer = Console.ReadLine();
                addMoreRover = addMoreRoverAnswer.ToUpper() == "Y" || addMoreRoverAnswer == string.Empty;
            }

            foreach (var rover in RoverExploration.Discover(rovers))
            {
                string coordinate = $"{rover.Coordinate.X} {rover.Coordinate.Y} {rover.Coordinate.Heading}";
                Console.WriteLine(coordinate);
            }
        }
    }
}