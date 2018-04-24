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
        // List to contain the current separations
        private List<SeparationEventObject> _currentSeparations;
        // List containing the former separations - this list is updated with information from _currentSeparations
        private List<SeparationEventObject> _priorSeparations;

        // Variables for the limits for horizontal and vertical separation
        private readonly float _horizontalSeperationLimit, _verticalSeperationLimit;

        // Variables to see, if the separation is new or finished
        private bool isNewSeparation;
        private bool isFinishedSeparation;

        public CheckForSeparationEvent()
        {
            // Initialization of lists
            _currentSeparations = new List<SeparationEventObject>();
            _priorSeparations = new List<SeparationEventObject>();

            // Set the limits to the specified values
            _horizontalSeperationLimit = 5000;
            _verticalSeperationLimit = 300;
            
        }

        public void CheckSeparationEvents(List<TrackObject> sortedTracksList)
        {
            // Clearing the list to remove historical data
            _currentSeparations = new List<SeparationEventObject>();

            // Runs through the elements in the received list (sortedTracksList)
            for (int i = 0; i < sortedTracksList.Count; i++)
            {
                // Runs through the elements in sortedTracksList (but "sorts" out the ones from the former loop = no comparing to it self or repetition of compare)
                for (int j = i + 1; j < sortedTracksList.Count; j++)
                {
                    // Check whether the tracks are too close to each other
                    if (Math.Abs(sortedTracksList[i].XCoordinate - sortedTracksList[j].XCoordinate) <= _horizontalSeperationLimit
                        && Math.Abs(sortedTracksList[i].YCoordinate - sortedTracksList[j].YCoordinate) <= _horizontalSeperationLimit
                        && Math.Abs(sortedTracksList[i].Altitude - sortedTracksList[j].Altitude) <= _verticalSeperationLimit)
                    {
                        // If two tracks are on separation course, an eventobject is created and added to list
                        _currentSeparations.Add(new SeparationEventObject(sortedTracksList[i].Tag, sortedTracksList[j].Tag, sortedTracksList[i].TimeStamp));
                    }
                }
            }

            /*
             * Now the current separation events should be compared to the prior ones to see, which events to keep raised, which are new and which are no longer present
             * This is done by running through the two lists and removing objects. The following reference is for removing elements while iterating: https://stackoverflow.com/questions/1582285/how-to-remove-elements-from-a-generic-list-while-iterating-over-it
             */
            // Checking whether there are any prior separations
            if (_priorSeparations.Count != 0)
            {
                // Checking if there are any current separations
                if (_currentSeparations.Count != 0)
                {
                    /*
                     * Compare the current events to the former to see, if there are any new events
                     */
                    // Running through every current event
                    foreach (var newSep in _currentSeparations.Reverse<SeparationEventObject>())
                    {
                        isNewSeparation = true;
                        // Running through every former event
                        foreach (var priorSep in _priorSeparations.Reverse<SeparationEventObject>())
                        {
                            // If the two events are for the same tracks
                            if ((newSep.Tag1 == priorSep.Tag1 && newSep.Tag2 == priorSep.Tag2)
                                || newSep.Tag1 == priorSep.Tag2 && newSep.Tag2 == priorSep.Tag1)
                            {
                                // Remove the event from the current event-list as it has been "used"
                                _currentSeparations.Remove(newSep);

                                // It's not a new separation
                                isNewSeparation = false;
                            }

                        }

                        // If the two elements aren't the same then the event is new and can be added to the prior-list
                        if (isNewSeparation)
                        {
                            _priorSeparations.Add(newSep);
                            OnNewSeparationEvent(new SeparationEventArgs(newSep));
                        }
                    }

                    /*
                     * Compare the former events to the current to see, if there are any old/finished events
                     */
                    // Running through former events
                    foreach (var priorSep in _priorSeparations.Reverse<SeparationEventObject>())
                    {
                        isFinishedSeparation = true;
                        // Running through current events
                        foreach (var newSep in _currentSeparations.Reverse<SeparationEventObject>())
                        {
                            // If the two elements are for the same tracks
                            if ((priorSep.Tag1 == newSep.Tag1 && priorSep.Tag2 == newSep.Tag2
                                 || priorSep.Tag1 == newSep.Tag2 && priorSep.Tag2 == newSep.Tag1))
                            {
                                // It's not a finished separation
                                isFinishedSeparation = false;
                            }
                        }

                        // If the two elements aren't the same, then the event is finished
                        if (isFinishedSeparation)
                        {
                            _priorSeparations.Remove(priorSep);
                        }
                    }
                }
            }
            // If there are no prior separations, then all current separations are new = no need to comparing
            else if(_currentSeparations.Count != 0)
            {
                _priorSeparations = _currentSeparations;
                OnNewSeparationEvent(new SeparationEventArgs(_priorSeparations));
            }

            if (_priorSeparations.Count != 0)
            {
                OnSeparationEvent(new SeparationEventArgs(_priorSeparations));
            }

        }

        private void OnSeparationEvent(SeparationEventArgs conflictedList)
        {
            var handler = SeperationEvents;
            handler?.Invoke(this, conflictedList);
        }

        public event EventHandler<SeparationEventArgs> SeperationEvents;

        private void OnNewSeparationEvent(SeparationEventArgs newEvents)
        {
            var handler = NewSeperationEvents;
            handler?.Invoke(this, newEvents);
        }

        public event EventHandler<SeparationEventArgs> NewSeperationEvents;
    }
}
