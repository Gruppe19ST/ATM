using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using ATM.Logic.Interfaces;

namespace ATM.Logic.Handlers
{
    public class CheckForSeparationEvent : ISeperationEventChecker
    {
        // List to hold received list of tracks
        private readonly List<TrackObject> _listOfTracks;

        // Variabels for the limits for horizontal and vertical separation
        private readonly float _horizontalSeperationLimit, _verticalSeperationLimit;

        // If two tracks are on separation course, they should be added to this list
        private readonly List<TrackObject> _separationTracks;

        // List to contain lists of conflicted tracks
        private readonly List<List<TrackObject>> _conflictedList;

        public CheckForSeparationEvent(List<TrackObject> sortedTracksList)
        {
            // Initialization of lists
            _listOfTracks = new List<TrackObject>();
            _separationTracks = new List<TrackObject>();
            _conflictedList = new List<List<TrackObject>>();

            // Set the limits to the specified values
            _horizontalSeperationLimit = 5000;
            _verticalSeperationLimit = 300;
            

            // Put received list in local list
            _listOfTracks = sortedTracksList;

        }

        public List<List<TrackObject>> CheckSeparationEvents()
        {
            _conflictedList.Clear();

            foreach (var checkTrack in _listOfTracks)
            {
                foreach (var compareTrack in _listOfTracks)
                {
                    if (!checkTrack.Tag.Equals(compareTrack.Tag))
                    {
                        if (Math.Abs(checkTrack.XCoordinate - compareTrack.XCoordinate) <= _horizontalSeperationLimit
                            && Math.Abs(checkTrack.YCoordinate - compareTrack.YCoordinate) <= _horizontalSeperationLimit
                            && Math.Abs(checkTrack.Altitude - compareTrack.Altitude) <= _verticalSeperationLimit)
                        {
                            _separationTracks.Clear();
                            _separationTracks.Add(checkTrack);
                            _separationTracks.Add(compareTrack);

                            _conflictedList.Add(_separationTracks);
                        }
                    }
                }
            }

            return _conflictedList;
        }
    }
}
