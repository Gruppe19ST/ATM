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

        private List<SeparationEventObject> _currentSeparations;
        private List<SeparationEventObject> _priorSeparations;
        private List<SeparationEventObject> _finishedSeparations;

        // Variables for the limits for horizontal and vertical separation
        private readonly float _horizontalSeperationLimit, _verticalSeperationLimit;

        public CheckForSeparationEvent()
        {
            // Initialization of lists
            _currentSeparations = new List<SeparationEventObject>();
            _priorSeparations = new List<SeparationEventObject>();
            _finishedSeparations = new List<SeparationEventObject>();

            // Set the limits to the specified values
            _horizontalSeperationLimit = 5000;
            _verticalSeperationLimit = 300;

        }

        public void CheckSeparationEvents(List<TrackObject> sortedTracksList)
        {
            _currentSeparations.Clear();

            // Runs through the elements in _listOfTracks
            for (int i = 0; i < sortedTracksList.Count; i++)
            {
                // Runs through the elements in _listOfTracks (but "sorts" out the ones from the former loop)
                for (int j = i+1; j < sortedTracksList.Count; j++)
                {
                    // Check whether the tracks are too close to eachother
                    if (Math.Abs(sortedTracksList[i].XCoordinate - sortedTracksList[j].XCoordinate) <= _horizontalSeperationLimit
                        && Math.Abs(sortedTracksList[i].YCoordinate - sortedTracksList[j].YCoordinate) <= _horizontalSeperationLimit
                        && Math.Abs(sortedTracksList[i].Altitude - sortedTracksList[j].Altitude) <= _verticalSeperationLimit)
                    {
                        // If two tracks are on separation course, an eventobject is created and added to list
                        SeparationEventObject separationEvent = new SeparationEventObject(sortedTracksList[i].Tag, sortedTracksList[j].Tag, sortedTracksList[i].TimeStamp, sortedTracksList[i].TimeStamp);
                        _currentSeparations.Add(separationEvent);

                       /* // If two tracks are on separation course, they should be added to a list with TrackObjects
                        // And this list should be added to the conflicted-list
                        OnSeparationEvent(new SeparationEventArgs(new List<TrackObject> { _listOfTracks[i], _listOfTracks[j] }));*/
                    }
                }
            }

            // Reference to removing elements while iterating: https://stackoverflow.com/questions/1582285/how-to-remove-elements-from-a-generic-list-while-iterating-over-it
            if (_priorSeparations.Count != 0)
            {
                // Compare _currentSeparations to priorSeparations
                foreach (var newSep in _currentSeparations.Reverse<SeparationEventObject>())
                {

                    foreach (var priorSep in _priorSeparations.Reverse<SeparationEventObject>())
                    {
                        if ((newSep.Tag1 == priorSep.Tag1 && newSep.Tag2 == priorSep.Tag2)
                            || newSep.Tag1 == priorSep.Tag2 && newSep.Tag2 == priorSep.Tag1)
                        {
                            priorSep.LastTime = newSep.LastTime;
                            _currentSeparations.Remove(newSep);
                        }
                        else
                        {
                            _priorSeparations.Add(newSep);
                        }
                    }
                }

                foreach (var priorSep in _priorSeparations.Reverse<SeparationEventObject>())
                {
                    foreach (var newSep in _currentSeparations.Reverse<SeparationEventObject>())
                    {
                        if ((priorSep.Tag1 == newSep.Tag1 && priorSep.Tag2 == newSep.Tag2
                             || priorSep.Tag1 == newSep.Tag2 && priorSep.Tag2 == newSep.Tag1))
                        {
                            priorSep.LastTime = newSep.LastTime;
                            //priorSep.EventTime = newSep.EventTime;
                        }
                        else
                        {
                            _finishedSeparations.Add(priorSep);
                            _priorSeparations.Remove(priorSep);
                        }
                    }
                }
            }
            else
            {
                _priorSeparations = _currentSeparations;
            }

            if (_priorSeparations.Count != 0)
            {
                OnSeparationEvent(new SeparationEventArgs(_priorSeparations));
            }

            if (_finishedSeparations.Count != 0)
            {
                OnFinishedSeparationEvent(new SeparationEventArgs(_finishedSeparations));
            }

        }

        private void OnSeparationEvent(SeparationEventArgs conflictedList)
        {
            var handler = SeperationEvents;
            handler?.Invoke(this,conflictedList);
        }

        public event EventHandler<SeparationEventArgs> SeperationEvents;

        private void OnFinishedSeparationEvent(SeparationEventArgs finishedEvents)
        {
            var handler = FinishedSeperationEvents;
            handler?.Invoke(this, finishedEvents);
        }

        public event EventHandler<SeparationEventArgs> FinishedSeperationEvents;
    }
}
