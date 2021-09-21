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
        }
    }
}