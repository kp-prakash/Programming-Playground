namespace Shared
{
    /// <summary>
    /// Represents various constants used.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The default buffer time.
        /// </summary>
        public const int DefaultBufferTime = 60;

        /// <summary>
        /// The default duration of a lightning talk;
        /// </summary>
        public const int LightningTalkDuration = 5;

        /// <summary>
        /// The space
        /// </summary>
        public const string Space = " ";

        /// <summary>
        /// The track format for printing output.
        /// </summary>
        public const string TrackFormat = "Track {0}:";

        /// <summary>
        /// The unscheduled talks format for printing output.
        /// </summary>
        public const string UnscheduledTalksFormat = "{0}. {1} {2}min";

        /// <summary>
        /// The talk start time format for printing output.
        /// </summary>
        public const string TalkStartTimeFormat = "hh:mmtt ";

        /// <summary>
        /// The talk duration format for printing output.
        /// </summary>
        public const string TalkDurationFormat = " {0}min";
    }
}