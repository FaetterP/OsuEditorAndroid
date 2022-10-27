using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Assets.Scripts.MapInfo
{
    class Editor
    {
        private List<int> _bookmarks = new List<int>();
        private double _distanceSpacing; // 0.1-6
        private int _beatDivisor; // 1-16
        private int _gridSize; // 1-32
        private double _timelineZoom; // 0.1-8

        public double DistanceSpacing
        {
            get
            {
                return _distanceSpacing;
            }

            set
            {
                if (value < 0.1 || value > 6) { throw new ArgumentException(); }
                _distanceSpacing = value;
            }
        }
        public int BeatDivisor
        {
            get
            {
                return _beatDivisor;
            }

            set
            {
                if (value < 1 || value > 16) { throw new ArgumentException(); }
                _beatDivisor = value;
            }
        }
        public int GridSize
        {
            get
            {
                return _gridSize;
            }

            set
            {
                if (value < 1 || value > 32) { throw new ArgumentException(); }
                _gridSize = value;
            }
        }
        public double TimelineZoom
        {
            get
            {
                return _timelineZoom;
            }

            set
            {
                if (value < 0.1 || value > 8) { throw new ArgumentException(); }
                _timelineZoom = value;
            }
        }

        public ReadOnlyCollection<int> Bookmarks
        {
            get
            {
                return _bookmarks.AsReadOnly();
            }
        }

        public void AddBookmark(int timestamp)
        {
            if (!_bookmarks.Contains(timestamp))
            {
                _bookmarks.Add(timestamp);
                _bookmarks.Sort();
            }
        }

        public void ClearBookmarks()
        {
            _bookmarks = new List<int>();
        }
    }
}