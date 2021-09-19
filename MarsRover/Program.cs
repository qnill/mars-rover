using System;
using System.Collections.Generic;

namespace MarsRover
{
    public class Rover
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Heading { get; set; }
        public string Location { get; set; }
    }

    class Program
    {
        private static readonly string[] _headings = new string[] { "N", "E", "S", "W" };

        private static (int x, int y) Move(int x, int y, int headingIndex)
        {
            switch (_headings[headingIndex])
            {
                case "N":
                    y++;
                    break;
                case "E":
                    x++;
                    break;
                case "S":
                    y--;
                    break;
                case "W":
                    x--;
                    break;
            }

            return (x, y);
        }

        private static string RotatePosition(Rover rover)
        {
            int headingIndex = Array.FindIndex(_headings, row => row == rover.Heading);
            if (headingIndex == -1)
                return null; /*Send Error*/

            var spins = rover.Location.ToCharArray();
            foreach (var spin in spins)
            {
                if (spin == char.Parse("L"))
                {
                    headingIndex--;
                    if (headingIndex == -1)
                        headingIndex = _headings.Length - 1;
                }
                else if (spin == char.Parse("R"))
                {
                    headingIndex++;
                    if (headingIndex > _headings.Length - 1)
                        headingIndex = 0;
                }
                else
                {
                    (rover.X, rover.Y) = Move(rover.X, rover.Y, headingIndex);
                }
            }

            var result = $"{rover.X} {rover.Y} {_headings[headingIndex]}";
            return result;
        }

        static void Main()
        {
            Console.WriteLine("Executing...");

            string range = "5 5";

            var rovers = new List<Rover>
            {
                new Rover
                {
                    X = 1,
                    Y = 2,
                    Heading = "N",
                    Location = "LMLMLMLMM"
                },
                new Rover
                {
                    X = 3,
                    Y = 3,
                    Heading = "E",
                    Location = "MMRMMRMRRM"
                },
                new Rover
                {
                    X = 3,
                    Y = 5,
                    Heading = "S",
                    Location = "LMMRMRRML"
                }
            };
            
            foreach (var rover in rovers)
            {
                var newPosition = RotatePosition(rover);
                Console.WriteLine(newPosition);
            }
        }
    }
}