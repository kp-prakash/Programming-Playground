using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared;

namespace Model
{
    /// <summary>Represents the conference.</summary>
    public class Conference : IConference
    {
        /// <summary>Gets or sets the unscheduled talks. </summary>
        /// <value>The unscheduled talks.</value>
        public List<ITalk> UnscheduledTalks { get; set; }

        /// <summary>Gets or sets the tracks.</summary>
        /// <value>The tracks.</value>
        public List<ITrack> Tracks { get; set; }

        /// <summary>Gets or sets the conference start date.</summary>
        /// <value>The conference start date.</value>
        public DateTime Date { get; set; }

        /// <summary>Gets the total duration of unscheduled talks.</summary>
        /// <value>The total duration of unscheduled talks.</value>
        public int RemainingUnscheduledTime
        {
            get { return UnscheduledTalks.Sum(talk => talk.Duration); }
        }

        /// <summary> Gets a value indicating whether any talks are remaining. </summary>
        /// <value><c>true</c> if [talks are remaining to be scheduled]; otherwise, <c>false</c>.</value>
        private bool AreTalksRemaining
        {
            get { return UnscheduledTalks.Any(talk => !talk.IsScheduled); }
        }

        /// <summary> Initializes a new instance of the <see cref="Conference" /> class. </summary>
        /// <param name="unscheduledTalks">The talks to be scheduled.</param>
        /// <param name="tracks">The tracks.</param>
        /// <param name="date">The conference start date.</param>
        public Conference(List<ITalk> unscheduledTalks, List<ITrack> tracks, DateTime date)
        {
            //Use only date component and excluded time.
            date = date.Date;
            UnscheduledTalks = unscheduledTalks;
            Tracks = tracks;
            Date = date;
        }

        /// <summary>Initializes a new instance of the <see cref="Conference" /> class.</summary>
        public Conference()
        {
            UnscheduledTalks = new List<ITalk>();
            Tracks = new List<ITrack>();
            Date = DateTime.Today;
        }

        /// <summary>Schedules the talks.</summary>
        /// <exception cref="TrackManagementException">Unable to find matching subset.</exception>
        public void ScheduleTalks()
        {
            try
            {
                if (null == UnscheduledTalks || null == Tracks) return;
                while (AreTalksRemaining)
                {
                    if (TrySchedulingUsingBufferTrackTime()) break;
                    ITrack newTrack = Factory.Instance.GetNewTrack(Date);
                    bool scheduleResult = newTrack.ScheduleTrack(UnscheduledTalks);
                    if (!scheduleResult)
                    {
                        var trackContent = newTrack.ToString();
                        if(!string.IsNullOrWhiteSpace(trackContent))
                        {
                            Console.WriteLine(Resource.TrackSchedulingError);
                            Console.WriteLine(trackContent);
                        }
                        throw new TrackManagementException(Resource.UnableToFindSubsetMessage);
                    }
                    Tracks.Add(newTrack);
                    Date = Date.AddDays(1); //Next track will begin on next day.
                }
            }
            catch (TrackManagementException trackManagementException)
            {
                PrintException(trackManagementException);
            }
        }

        /// <summary> Returns a <see cref="System.String" /> that represents this instance. </summary>
        /// <returns> A <see cref="System.String" /> that represents this instance. </returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            int trackIndex = 1;
            foreach (ITrack track in Tracks)
            {
                if (track == null) continue;
                stringBuilder.Append(string.Format(Constants.TrackFormat, trackIndex));
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append(track.ToString());
                stringBuilder.Append(Environment.NewLine);
                trackIndex++;
            }
            return stringBuilder.ToString();
        }

        /// <summary>Prints the exception message to console.</summary>
        /// <param name="trackManagementException">The track management exception.</param>
        private void PrintException(TrackManagementException trackManagementException)
        {
            Console.WriteLine(trackManagementException.Message);
            if (null == UnscheduledTalks) return;
            Console.WriteLine(Resource.UnableToSchedule);
            for (int index = 0; index < UnscheduledTalks.Count; index++)
            {
                ITalk unscheduledTalk = UnscheduledTalks[index];
                Console.WriteLine(Constants.UnscheduledTalksFormat, index + 1, unscheduledTalk.Title, unscheduledTalk.Duration);
            }
        }

        /// <summary>Tries the scheduling talks using buffer track time in afternoon session.</summary>
        /// <returns>True if there is possiblity to schedule talks in buffer time.</returns>
        private bool TrySchedulingUsingBufferTrackTime()
        {
            bool isPossible = CheckPossibilityToUseBufferDuration();
            int remainingUnscheduledTime = RemainingUnscheduledTime;
            //Try to see if the talks could be fit into the buffer slot.
            if (Tracks.Count != 0 && isPossible)
            {
                bool result = ScheduleMiscellaneousTalks(remainingUnscheduledTime);
                if (result) return true;
            }
            return false;
        }

        /// <summary> Checks the possibility to use buffer duration.</summary>
        /// <returns> If there is any task which could be fit into a buffer duration of a track.</returns>
        private bool CheckPossibilityToUseBufferDuration()
        {
            return Tracks.Any(track => UnscheduledTalks.Any(talk => talk.Duration <= track.BufferDuration));
        }

        /// <summary>Schedules the miscellaneous talks using the buffer time in various tracks.</summary>
        /// <param name="remainingUnscheduledTime">The remaining unscheduled time.</param>
        /// <returns>True if all remaining unscheduled time is 0 and all talks are scheduled.</returns>
        private bool ScheduleMiscellaneousTalks(int remainingUnscheduledTime)
        {
            foreach (ITrack track in Tracks)
            {
                if (remainingUnscheduledTime == 0) return true;
                //Miscellaneous talks are scheduled in the buffer time (4PM-5PM) in the after noon session.
                if (track == null || track.AfternoonSession == null) continue;
                int availableBufferDuration = track.AfternoonSession.BufferDuration;
                if (availableBufferDuration == 0) continue;
                var selectedDuration = Math.Min(availableBufferDuration, remainingUnscheduledTime);
                List<ITalk> miscTalks = TalkScheduler.Instance.ScheduleTalks(UnscheduledTalks, selectedDuration);
                if (miscTalks != null && miscTalks.Count > 0)
                {
                    track.AfternoonSession.AddTalks(miscTalks, UnscheduledTalks);
                    remainingUnscheduledTime = remainingUnscheduledTime - miscTalks.Sum(talk => talk.Duration);
                }
            }
            return remainingUnscheduledTime == 0;
        }
    }
}