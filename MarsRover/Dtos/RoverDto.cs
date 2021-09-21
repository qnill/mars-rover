using static MarsRover.Const.Headings;
using static MarsRover.Const.MoveInstructions;

namespace MarsRover.Dtos
{
    public class RoverDto
    {
        public RoverCoordinateDto Coordinate { get; set; }
        public MoveInstructionType[] MoveInstructions { get; set; }

        public RoverDto()
        {
            Coordinate = new RoverCoordinateDto();
        }
    }

    public class RoverCoordinateDto
    {
        public int X { get; set; }
        public int Y { get; set; }
        public HeadingType Heading { get; set; }
    }
}