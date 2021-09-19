using MarsRover.Const;
using MarsRover.Dtos;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MarsRover.Helper
{
    public static class RoverDataConvert
    {
        public static RoverCoordinateDto Coordinate(string coordinate)
        {
            //1 2 N
            RoverCoordinateDto coordinateModel = new();

            var coordinates = coordinate.Split(" ");
            if (coordinates == null || coordinates.Length != 3)
                // coordinate verisi hatalı girildi;
                return null;

            if (Regex.IsMatch(coordinates[0], "^[0-9]*$"))
                coordinateModel.X = Convert.ToInt32(coordinates[0]);

            if (Regex.IsMatch(coordinates[1], "^[0-9]*$"))
                coordinateModel.Y = Convert.ToInt32(coordinates[1]);

            var headings = Headings.ToArray();
            if (!headings.Any(a => a == coordinates[2]))
                // coordinate verisi yön hatalı girildi;
                return null;

            coordinateModel.Heading = (Headings.HeadingType)Enum.Parse(typeof(Headings.HeadingType), coordinates[2]);
            return coordinateModel;
        }

        // !!!?
        public static Routes.RouteType[] ExplorationInstructions(string explorationInstructions)
        {
            if (string.IsNullOrWhiteSpace(explorationInstructions))
                // explorationInstructions verisi boş burakılamaz.
                return null;

            var instructions = explorationInstructions.ToCharArray();
            var routes = Routes.ToArray();

            var routeTypes = new Routes.RouteType[instructions.Length];
            for (int i = 0; i < instructions.Length; i++)
            {
                string instruction = instructions[i].ToString();
                if (!routes.Any(a => a == instruction))
                    continue;

                routeTypes[i] = (Routes.RouteType)Enum.Parse(typeof(Routes.RouteType), instruction);
            }

            return routeTypes;
        }
    }
}