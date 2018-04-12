﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Logic.Handlers
{
        public class Sorter : Interfaces.ISorter
        {
            private int[] _xLimit;
            private int[] _yLimit;
            private int[] _altLimit;
            public List<TrackObject> SortedList;
            public DTO.DTOTrack DTOTrack;

            public Sorter()
            {
                _xLimit = new int[] { 10000, 90000 };
                _altLimit = new int[] { 500, 20000 };
                _yLimit = new int[] { 10000, 90000 };
                SortedList = new List<TrackObject>();

            }
            public void SortTracks(List<TrackObject> tracks)
            {
                foreach (var var in tracks)
                {
                    if (var.XCoordinate > _xLimit[0] && var.XCoordinate < _xLimit[1] && var.YCoordinate > _yLimit[0] &&
                        var.YCoordinate < _yLimit[1] && var.Altitude > _altLimit[0] && var.Altitude < _altLimit[1])
                    {
                        SortedList.Add(var);
                    }
                }

                UpdateDTO(SortedList);
            }

            public void UpdateDTO(List<TrackObject> sortedlist)
            {
                DTOTrack.SortedTracks = SortedList;
            }
        }

    }