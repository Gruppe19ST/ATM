using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using ATM.Logic.Interfaces;

namespace ATM.Logic.Handlers
{
    class CheckForSeparationEvent : ISeperationEventChecker
    {
        private List<TrackObject> xCoorSortedList, yCoorSortedList;

        private float xSeperationLimit, ySeperationLimit;
        private List<TrackObject> separationTracks;
        private List<List<TrackObject>> conflictedTrackObjects;

        private DTO.DTOSeperationEvents DTOSeparationTracks;

        public CheckForSeparationEvent(List<TrackObject> sortedTracksList)
        {
            xCoorSortedList = sortedTracksList.OrderBy(o => o.XCoordinate).ToList();
            yCoorSortedList = sortedTracksList.OrderBy(o => o.YCoordinate).ToList();
            separationTracks = new List<TrackObject>();
            conflictedTrackObjects = new List<List<TrackObject>>();
        }

        public void CheckSeperationEvent()
        {
            for (int i = 0; i < xCoorSortedList.Count; i++)
            {
                if (i != 0)
                {
                    if(xCoorSortedList[i].XCoordinate - xCoorSortedList[i - 1].XCoordinate <= xSeperationLimit && yCoorSortedList[i].YCoordinate - yCoorSortedList[i - 1].YCoordinate <= ySeperationLimit)
                    {
                        separationTracks.Clear();
                        separationTracks.Add(xCoorSortedList[i - 1]);
                        separationTracks.Add(xCoorSortedList[i]);
                        
                        conflictedTrackObjects.Add(separationTracks);
                    }
                }
            }

            SaveToDTO(conflictedTrackObjects);
        }

        public void SaveToDTO(List<List<TrackObject>> conflictedTracksList)
        {
            DTOSeparationTracks.SeparationTracks = conflictedTracksList;
        }
    }
}
