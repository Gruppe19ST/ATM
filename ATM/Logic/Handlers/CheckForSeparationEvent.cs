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
        private List<TrackObject> listOfTracks, xCoorSortedList, yCoorSortedList;

        private float horizontalSeperationLimit, verticalSeperationLimit;
        private List<TrackObject> separationTracks;
        private List<List<TrackObject>> xConflictedTracks, yConflictedTracks, conflictedList;

        private DTO.DTOSeperationEvents DTOSeparationTracks;

        public CheckForSeparationEvent(List<TrackObject> sortedTracksList)
        {
            // Initialization
            listOfTracks = new List<TrackObject>();
            separationTracks = new List<TrackObject>();
            conflictedList = new List<List<TrackObject>>();
            horizontalSeperationLimit = 5000;
            verticalSeperationLimit = 300;
            

            // Put received list in local list
            listOfTracks = sortedTracksList;

        }

        public List<List<TrackObject>> CheckSeparationEvents()
        {
            conflictedList.Clear();

            foreach (var checkTrack in listOfTracks)
            {
                foreach (var compareTrack in listOfTracks)
                {
                    if (!checkTrack.Tag.Equals(compareTrack.Tag))
                    {
                        if (Math.Abs(checkTrack.XCoordinate - compareTrack.XCoordinate) <= horizontalSeperationLimit
                            && Math.Abs(checkTrack.YCoordinate - compareTrack.YCoordinate) <= horizontalSeperationLimit
                            && Math.Abs(checkTrack.Altitude - compareTrack.Altitude) <= verticalSeperationLimit)
                        {
                            separationTracks.Clear();
                            separationTracks.Add(checkTrack);
                            separationTracks.Add(compareTrack);

                            conflictedList.Add(separationTracks);
                        }
                    }
                }
            }

            return conflictedList;
        }

       
        // Muligvis slet
        public void CheckSeperationEvent()
        {
            xConflictedTracks.Clear();
            for (int i = 0; i < xCoorSortedList.Count; i++)
            {
                if (i != 0)
                {
                    if(xCoorSortedList[i].XCoordinate - xCoorSortedList[i - 1].XCoordinate <= verticalSeperationLimit)
                    {
                        separationTracks.Clear();
                        separationTracks.Add(xCoorSortedList[i - 1]);
                        separationTracks.Add(xCoorSortedList[i]);
                        
                        xConflictedTracks.Add(separationTracks);
                    }

                    if (yCoorSortedList[i].YCoordinate - yCoorSortedList[i - 1].YCoordinate <= horizontalSeperationLimit)
                    {
                        separationTracks.Clear();
                        separationTracks.Add(yCoorSortedList[i - 1]);
                        separationTracks.Add(yCoorSortedList[i]);
                        
                        yConflictedTracks.Add(separationTracks);
                    }
                }

                if (xConflictedTracks.Count != 0 && yConflictedTracks.Count != 0)
                {
                    if (xConflictedTracks.Count > yConflictedTracks.Count)
                    {
                        foreach (var xConflict in xConflictedTracks)
                        {
                            for (int j = 0; j < yConflictedTracks.Count; j++)
                            {
                                if (yConflictedTracks[j].Equals(xConflict))
                                {
                                    conflictedList.Add(xConflict);
                                }
                            }
                        }
                    }
                }
            }

            SaveToDTO(xConflictedTracks);
        }

        public void SaveToDTO(List<List<TrackObject>> conflictedTracksList)
        {
            DTOSeparationTracks.SeparationTracks = conflictedTracksList;
        }
    }
}
