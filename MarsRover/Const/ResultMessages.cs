namespace MarsRover.Const
{
    /// <summary>
    /// System messages are designed as 7 characters:
    /// First 2 characters Module Code
    /// Third Character Message Code (E: Error, W: Warning, I: Information)
    /// Last 4 characters error code
    /// </summary>
    internal static class ResultMessages
    {
        internal static class RoverInputConvert
        {
            /// <summary>
            /// Move instructions cannot be left blank.
            /// </summary>
            internal const string RIE0001 = "RIE0001-Move instructions cannot be left blank.";

            /// <summary>
            /// Coordinate input is wrong.
            /// </summary>
            internal const string RIE0002 = "RIE0002-Coordinate input is wrong.";

            /// <summary>
            /// Location must be numbers only.
            /// </summary>
            internal const string RIE0003 = "RIE0003-Location must be numbers only.";

            /// <summary>
            /// The heading could not find.
            /// </summary>
            internal const string RIE0004 = "RIE0004-The heading could not find.";

            /// <summary>
            /// The move instruction could not find.
            /// </summary>
            internal const string RIE0005 = "RIE0005-The move instruction could not find.";

            /// <summary>
            /// The entered coordinates are outside of the range.
            /// </summary>
            internal const string RIE0006 = "RIE0006-The entered coordinates are outside of the range.";

            /// <summary>
            /// Another rover has already been added to this coordinates.
            /// </summary>
            internal const string RIE0007 = "RIE0007-Another rover has already been added to this coordinates.";

            /// <summary>
            /// Upper-right coordinates is wrong.
            /// </summary>
            internal const string RIE0008 = "RIE0008-Upper-right coordinates is wrong.";

            /// <summary>
            /// Upper-right coordinates data must be numbers only.
            /// </summary>
            internal const string RIE0009 = "RIE0009-Upper-right coordinates data must be numbers only.";
        }

        internal static class RoverExploration
        {
            /// <summary>
            /// The rover unable to move as it reaches the range. Final position: {0}.
            /// </summary>
            internal const string REE0001 = "REE0001-The rover unable to move as it reaches the range. Final position: {0}.";

            /// <summary>
            /// Rover could not move because another rover at the target coordinates. Final position: {0}.
            /// </summary>
            internal const string REE0002 = "REE0002-Rover could not move because another rover at the target coordinates. Final position: {0}.";
        }
    }
}