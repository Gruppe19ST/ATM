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
        public bool eventRaised = false;

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

        //public List<List<TrackObject>> CheckSeparationEvents()
        public void CheckSeparationEvents()
        {
            _conflictedList.Clear();

            for (int i = 0; i < _listOfTracks.Count; i++)
            {
                for (int j = i+1; j < _listOfTracks.Count; j++)
                {
                    if (Math.Abs(_listOfTracks[i].XCoordinate - _listOfTracks[j].XCoordinate) <= _horizontalSeperationLimit
                        && Math.Abs(_listOfTracks[i].YCoordinate - _listOfTracks[j].YCoordinate) <= _horizontalSeperationLimit
                        && Math.Abs(_listOfTracks[i].Altitude - _listOfTracks[j].Altitude) <= _verticalSeperationLimit)
                    {
                        _conflictedList.Add(new List<TrackObject> { _listOfTracks[i], _listOfTracks[j] });
                    }
                }
            }


            /*foreach (var checkTrack in _listOfTracks)
            {
                foreach (var compareTrack in _listOfTracks)
                {
                    if (!checkTrack.Tag.Equals(compareTrack.Tag))
                    {
                        if (Math.Abs(checkTrack.XCoordinate - compareTrack.XCoordinate) <= _horizontalSeperationLimit
                            && Math.Abs(checkTrack.YCoordinate - compareTrack.YCoordinate) <= _horizontalSeperationLimit
                            && Math.Abs(checkTrack.Altitude - compareTrack.Altitude) <= _verticalSeperationLimit)
                        {
                            _conflictedList.Add(new List<TrackObject> { checkTrack, compareTrack });    
                        }
                    }
                }
            }*/

            if (_conflictedList.Count > 0)
            {
                OnSeparationEvent(new SeparationEventArgs(_conflictedList));
            }

            //return _conflictedList;
        }

        private void OnSeparationEvent(SeparationEventArgs conflictedList)
        {
            var handler = SeperationEvents;
            handler?.Invoke(this,conflictedList);
        }

        public event EventHandler<SeparationEventArgs> SeperationEvents;
    }
}
