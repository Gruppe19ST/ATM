using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Logic.Handlers
{
    class TrackSpeed : Interfaces.ITrackSpeed
    {
        private List<TrackObject> localTacks;

        public TrackSpeed(ref DTO.DTOTrack dtoTrack)
        {
            //localTacks = dtoTrack.getTrackList();
        }

        public void CalculateSpeed()
        {
            foreach (var track in localTacks)
            {
                // timespan interval = track.t1-track.t0; 
                // Sørg for at t0 og x0 sættes til 0 ved den første måling og at den første måling indlæses i x1 og t1. 

                // track.horizontalVelocity = math.abs(track.x1-track.x0)/(interval.TotalSeconds); 
                
                // x1 er nuværende position, x0 er den tidligere position
                // t0 er nuværende timestamp, t0 er tidligere timestamp 
            }
        }

        public void SaveToDTO()
        {
            // dtoTrack.sortedTracks = localTracks; 
        }
    }
}
