using System;
using System.Collections.Generic;
using System.Text;

namespace ChessSimulator
{
    class Chess
    {
        public string Type { get; set; }
        public int Level { get; set; }
        public int Seq { get; set; }
        public int SwitchSeq { get; set; }
        public bool IsSwitch { get; set; }
        public Chess Clone()
        {
            return this.MemberwiseClone() as Chess;
        }
        public override bool Equals(object obj)
        {
            if (obj is null && this is null)
            {
                return true;
            }
            if (obj is Chess chess &&
                chess.Type == this.Type &&
                chess.Level == this.Level)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
