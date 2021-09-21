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

            int roverId = 1;
            IList<(int roverId, string coordinate, string moveInstructions)> inputs = new List<(int, string, string)>();

            bool addMoreRover = true;
            while (addMoreRover)
            {
                Console.Write($"rover-{roverId} start coordinate: ");
                string startCoordinate = Console.ReadLine();

                Console.Write($"rover-{roverId} exploration instructions: ");
                string moveInstructions = Console.ReadLine();

                inputs.Add((roverId, startCoordinate, moveInstructions));
                roverId++;

                Console.Write("Do you want to add another rover? (Y/n): ");
                string addMoreRoverAnswer = Console.ReadLine();
                addMoreRover = addMoreRoverAnswer.ToUpper() == "Y" || addMoreRoverAnswer == string.Empty;
            }

            var rovers = RoverInputConverter.Set(inputs);

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