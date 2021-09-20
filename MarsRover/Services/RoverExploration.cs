using MarsRover.Const;
using MarsRover.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover.Services
{
    public static class RoverExploration
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

        public static IList<RoverDto> Discover(IList<RoverDto> rovers)
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
    }
}