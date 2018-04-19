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

        private List<SeparationEventObject> _listOfSeparations;
        private List<SeparationEventObject> _currentSeparations;
        private List<SeparationEventObject> _priorSeparations;

        // Variables for the limits for horizontal and vertical separation
        private readonly float _horizontalSeperationLimit, _verticalSeperationLimit;

        public CheckForSeparationEvent(List<TrackObject> sortedTracksList)
        {
            // Initialization of lists
            _listOfTracks = new List<TrackObject>();
            _listOfSeparations = new List<SeparationEventObject>();
            _currentSeparations = new List<SeparationEventObject>();
            _priorSeparations = new List<SeparationEventObject>();

            // Set the limits to the specified values
            _horizontalSeperationLimit = 5000;
            _verticalSeperationLimit = 300;

            // Put received list in local list
            _listOfTracks = sortedTracksList;
        }

        public void CheckSeparationEvents()
        {
            _listOfSeparations.Clear();

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
                        // If two tracks are on separation course, an eventobject is created and added to list
                        SeparationEventObject separationEvent = new SeparationEventObject(_listOfTracks[i].Tag, _listOfTracks[j].Tag, _listOfTracks[i].TimeStamp);
                        _listOfSeparations.Add(separationEvent);

                       /* // If two tracks are on separation course, they should be added to a list with TrackObjects
                        // And this list should be added to the conflicted-list
                        OnSeparationEvent(new SeparationEventArgs(new List<TrackObject> { _listOfTracks[i], _listOfTracks[j] }));*/
                    }
                }
            }

            // Compare _listOfSeparations to priorSeparations
            foreach (var newSep in _listOfSeparations)
            {
                foreach (var priorSep in _priorSeparations)
                {
                    if ((newSep.Tag1 == priorSep.Tag1 && newSep.Tag2 == priorSep.Tag2)
                        || newSep.Tag1 == priorSep.Tag2 && newSep.Tag2 == priorSep.Tag1)
                    {
                        priorSep.EventTime = newSep.EventTime;
                        _listOfSeparations.Remove(newSep);
                    }
                    else
                    {
                        _priorSeparations.Add(newSep);
                    }
                }
            }

            foreach (var priorSep in _priorSeparations)
            {
                foreach (var newSep in _listOfSeparations)
                {
                    if ((priorSep.Tag1 == newSep.Tag1 && priorSep.Tag2 == newSep.Tag2
                         || priorSep.Tag1 == newSep.Tag2 && priorSep.Tag2 == newSep.Tag1))
                    {
                        priorSep.EventTime = newSep.EventTime;
                    }
                    else
                    {
                        _priorSeparations.Remove(priorSep);
                    }
                }
            }

        }

        private void OnSeparationEvent(SeparationEventArgs conflictedList)
        {
            var handler = SeperationEvents;
            handler?.Invoke(this,conflictedList);
        }

        public event EventHandler<SeparationEventArgs> SeperationEvents;
    }
}
