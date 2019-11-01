using System;
using System.Collections.Generic;
using System.Text;

namespace ChessSimulator
{
    class Point
    {
        public int X { get; }
        public int Y { get; }
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
