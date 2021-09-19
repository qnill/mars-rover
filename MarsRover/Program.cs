using MarsRover.Const;
using MarsRover.Dtos;
using MarsRover.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    class Program
    {
        private static RoverCoordinateDto Move(RoverCoordinateDto coordinate)
        {
            switch (coordinate.Heading)
            {
                case Headings.HeadingType.N:
                    coordinate.Y++;
                    break;
                case Headings.HeadingType.E:
                    coordinate.X++;
                    break;
                case Headings.HeadingType.S:
                    coordinate.Y--;
                    break;
                case Headings.HeadingType.W:
                    coordinate.X--;
                    break;
            }

            return coordinate;
        }

        private static IList<RoverDto> RotatePosition(IList<RoverDto> rovers)
        {
            var headings = Headings.ToDictionary();

            foreach (var rover in rovers)
            {
                byte? headingIndex = headings.Where(x => x.Value == rover.Coordinate.Heading.ToString()).Select(s => s.Key).FirstOrDefault();
                if (headingIndex == null)
                    return null; /*Send Error*/

                foreach (var instruction in rover.ExplorationInstructions)
                {
                    if (instruction == Routes.RouteType.L)
                    {
                        headingIndex--;
                        if (headingIndex < headings.Min(mn => mn.Key))
                            headingIndex = headings.Max(mx => mx.Key);
                    }
                    else if (instruction == Routes.RouteType.R)
                    {
                        headingIndex++;
                        if (headingIndex > headings.Max(mx => mx.Key))
                            headingIndex = headings.Min(mn => mn.Key);
                    }
                    else if (instruction == Routes.RouteType.M)
                    {
                        rover.Coordinate.Heading = (Headings.HeadingType)headingIndex;
                        Move(rover.Coordinate);
                    }
                }
            }

            return rovers;
        }

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

            foreach (var rover in RotatePosition(rovers))
            {
                string coordinate = $"{rover.Coordinate.X} {rover.Coordinate.Y} {rover.Coordinate.Heading}";
                Console.WriteLine(coordinate);
            }
        }
    }
}