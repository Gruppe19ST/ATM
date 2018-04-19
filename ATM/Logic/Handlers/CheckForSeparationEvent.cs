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

        // Variables for the limits for horizontal and vertical separation
        private readonly float _horizontalSeperationLimit, _verticalSeperationLimit;

        // List to contain lists of conflicted tracks
        //private readonly List<List<TrackObject>> _conflictedList;

        public CheckForSeparationEvent(List<TrackObject> sortedTracksList)
        {
            // Initialization of lists
            _listOfTracks = new List<TrackObject>();
            //_conflictedList = new List<List<TrackObject>>();

            // Set the limits to the specified values
            _horizontalSeperationLimit = 5000;
            _verticalSeperationLimit = 300;

            // Put received list in local list
            _listOfTracks = sortedTracksList;
        }

        public void CheckSeparationEvents()
        {
           // _conflictedList.Clear();

            // Runs through the elements in _listOfTracks
            for (int i = 0; i < _listOfTracks.Count; i++)
            {
                // Runs through the elements in _listOfTracks (but "sorts" out the ones from the former loop)
                for (int j = i+1; j < _listOfTracks.Count; j++)
                {
                    // Check whether the tracks are too close to eachother
                    if (Math.Abs(_listOfTracks[i].XCoordinate - _listOfTracks[j].XCoordinate) <= _horizontalSeperationLimit
                        && Math.Abs(_listOfTracks[i].YCoordinate - _listOfTracks[j].YCoordinate) <= _horizontalSeperationLimit
                        && Math.Abs(_listOfTracks[i].Altitude - _listOfTracks[j].Altitude) <= _verticalSeperationLimit)
                    {
                        // If two tracks are on separation course, they should be added to a list with TrackObjects
                        // And this list should be added to the conflicted-list
                        //_conflictedList.Add(new List<TrackObject> { _listOfTracks[i], _listOfTracks[j] });
                        OnSeparationEvent(new SeparationEventArgs(new List<TrackObject> { _listOfTracks[i], _listOfTracks[j] }));
                    }
                }
            }

            /*
            // If there are any conflicted tracks, then an event should be raised
            if (_conflictedList.Count > 0)
            {
                OnSeparationEvent(new SeparationEventArgs(_conflictedList));
            }*/
        }

        private void OnSeparationEvent(SeparationEventArgs conflictedList)
        {
            var handler = SeperationEvents;
            handler?.Invoke(this,conflictedList);
        }

        public event EventHandler<SeparationEventArgs> SeperationEvents;
    }
}
