using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessSimulator
{
    class Table
    {
        private Chess[,] table;
        private int size;
        public Table(Chess[,] table)
        {
            this.table = table;
            this.size = table.GetLength(0);
        }
        public Solution GetSolution()
        {
            Solution solution = null;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var result = SimulatePoint(new Point(i, j));
                    if (solution == null || solution.Result < result.Result)
                    {
                        solution = result;
                    }
                }
            }
            return solution;
        }
        private Solution SimulatePoint(Point p)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var result = SimulateSolution(p, new Point(i, j));
                }
            }
            return null;
        }

        private int SimulateSolution(Point p1, Point p2)
        {
            int seq = 1;

            var table = this.CloneTable();
            table[p1.X, p1.Y].IsSwitch = true;
            table[p2.X, p2.Y].IsSwitch = true;
            table = this.SwitchChess(table, p1, p2);

            for (int i = 0; i < size; i++)
            {
                for (int k = 0; k < size; k++)
                {
                    var points = TableCheck.Check(table, new Point(i, k));
                    foreach (var ps in points)
                    {
                        if (ps == null)
                        {
                            continue;
                        }

                        if (ps.Count(p => table[p.X, p.Y].Seq != 0) != 0)
                        {
                            var pos = ps.First(p => table[p.X, p.Y].Seq != 0);
                            ps.ForEach(p =>
                            {
                                table[p.X, p.Y].Seq = table[pos.X, pos.Y].Seq;
                            });
                        }
                        else
                        {
                            ps.ForEach(p =>
                            {
                                table[p.X, p.Y].Seq = seq;
                            });
                            var pos = ps[1];
                            table[pos.X, pos.Y].SwitchSeq = seq;
                            table[pos.X, pos.Y].IsSwitch = true;
                            seq++;
                        }
                    }
                }
            }

            foreach (var chess in table)
            {
                if (chess.IsSwitch == false)
                {
                    continue;
                }
                
            }
            return 0;
        }

        private Chess[,] SwitchChess(Chess[,] table, Point p1, Point p2)
        {
            var tempChess = table[p1.X, p1.Y];
            table[p1.X, p1.Y] = table[p2.X, p2.Y];
            table[p2.X, p2.Y] = tempChess;
            return table;
        }


        private Chess[,] CloneTable()
        {
            var table = new Chess[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    table[i, j] = this.table[i, j].Clone();
                }
            }
            return table;
        }

    }
}
