using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Logic.Interfaces;
using ATM.Logic.Handlers;

namespace ATM.Logic.Handlers
{
        public class Sorter : Interfaces.ISorter
        {
            private int[] _xLimit;
            private int[] _yLimit;
            private int[] _altLimit;

            public List<TrackObject> SortedList;
            private List<TrackObject> listOfTrackObjects;

            public event EventHandler<TrackObjectEventArgs> TrackSortedReady;
            public ITrackConverter _trackconverter;


            public Sorter(ITrackConverter trackconverter)
            {
                _trackconverter = trackconverter;
                _trackconverter.TrackObjectsReady += _trackconverter_TrackObjectsReady;

                _xLimit = new int[] { 10000, 90000 };
                _altLimit = new int[] { 500, 20000 };
                _yLimit = new int[] { 10000, 90000 };

                SortedList = new List<TrackObject>();
            }

        public void _trackconverter_TrackObjectsReady(object sender, TrackObjectEventArgs e)
        {
            
            listOfTrackObjects = e.TrackObjects;
            SortTracks(listOfTrackObjects);
            
        }

        public void SortTracks(List<TrackObject> tracks)
            {
                SortedList.Clear();
                foreach (var var in tracks)
                {
                    if (var.XCoordinate > _xLimit[0] && var.XCoordinate < _xLimit[1] && var.YCoordinate > _yLimit[0] &&
                        var.YCoordinate < _yLimit[1] && var.Altitude > _altLimit[0] && var.Altitude < _altLimit[1])
                    {
                        SortedList.Add(var);
                    }
                }

                if (SortedList.Count != 0)
                {
                    OnTrackSortedUpdated(new TrackObjectEventArgs(SortedList));
                }

            }

        public void OnTrackSortedUpdated(TrackObjectEventArgs tracksortedobject)
        {
            var handler = TrackSortedReady;
            handler?.Invoke(this, tracksortedobject);
        }

        }

    }
