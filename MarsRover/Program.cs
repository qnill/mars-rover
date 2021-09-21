using MarsRover.Dtos;
using MarsRover.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    class Program
    {
        static void Main()
        {
            Console.Write("upper-right coordinates: ");
            string upperRightCoordinates = Console.ReadLine();

            List<RoverDto> rovers = new();
            int roverId = 1;
            bool addMoreRover = true;
            while (addMoreRover)
            {
                Console.Write($"rover-{roverId} start coordinate: ");
                string startCoordinate = Console.ReadLine();

                Console.Write($"rover-{roverId} exploration instructions: ");
                string moveInstructions = Console.ReadLine();

                RoverDto rover = RoverInputConverter.Set(startCoordinate, moveInstructions);
                rover.Id = roverId;
                rovers.Add(rover);
                roverId++;

                Console.Write("Do you want to add another rover? (Y/n): ");
                string addMoreRoverAnswer = Console.ReadLine();
                addMoreRover = addMoreRoverAnswer.ToUpper() == "Y" || addMoreRoverAnswer == string.Empty;
            }

            Console.WriteLine("\n-----Result-----");

            RoverExploration.Discover(rovers.Where(x => x.Success).ToList());
            foreach (var rover in rovers)
            {
                string result;

                if (!rover.Success)
                    result = rover.Message;
                else
                    result = $"{rover.Coordinate.X} {rover.Coordinate.Y} {rover.Coordinate.Heading}";

                Console.WriteLine($"rover-{rover.Id}: {result}");
            }
        }
    }
}