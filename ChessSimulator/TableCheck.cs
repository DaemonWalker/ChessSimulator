using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessSimulator
{
    class TableCheck
    {
        private static readonly List<Func<int, Point, Point>> checks = new List<Func<int, Point, Point>>
        {
            (i, p) => new Point(p.X - i, p.Y),
            (i, p) => new Point(p.X - i, p.Y - i),
            (i, p) => new Point(p.X - i, p.Y + i),
            (i, p) => new Point(p.X, p.Y - i)
        };
        private static bool Check(IEnumerable<Point> points, Chess[,] table, Point p)
        {
            foreach (var point in points)
            {
                if (IsInTable(point, table.GetLength(0)) == false)
                {
                    return false;
                }
                if (table[point.X, point.Y] != table[p.X, p.Y])
                {
                    return false;
                }
            }
            return true;
        }
        private static bool IsInTable(int x, int y, int size) => (0 <= x && x < size && 0 <= y && y < size);
        private static bool IsInTable(Point p, int size) => IsInTable(p.X, p.Y, size);
        private static List<Point> GetPoints(Point point, Func<int, Point, Point> func) => Enumerable
            .Range(0, 3)
            .Select(p => func(p, point))
            .ToList();

        public static List<List<Point>> Check(Chess[,] table, Point p) =>
            checks.Select(c => GetPoints(p, c)).Select(_ =>
            {
                if (Check(_, table, p))
                {
                    return _;
                }
                else
                {
                    return null;
                }
            }).ToList();
    }
}
