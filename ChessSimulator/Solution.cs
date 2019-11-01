using System;
using System.Collections.Generic;
using System.Text;

namespace ChessSimulator
{
    class Solution
    {
        public Point P1 { get; }
        public Point P2 { get; }
        public int Result { get; }
        public Solution(int x1, int y1, int x2, int y2, int result)
        {
            this.P1 = new Point(x1, y1);
            this.P2 = new Point(x2, y2);
            this.Result = result;
        }
    }
}
