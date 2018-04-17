using System;

namespace ATM.Logic.Handlers.Converter
{
    public interface ITrackConverter
    {
        event EventHandler<TrackObjectEventArgs> TrackObjectsReady;
    }
}
