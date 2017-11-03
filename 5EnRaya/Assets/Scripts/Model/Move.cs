using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CincoEnRaya.Model
{
    public class Move
    {
        private int column;
        private Player player;

        public Move(int column, Player player)
        {
            this.column = column;
            this.player = player;
        }

        public int Column { get { return column; } }
        public Player Player { get { return player; } }

        public bool IsValidOn(Board board)
        {
            return !board.ColumnIsFull(Column);
        }

        public void ExecuteOn(Board board)
        {
            Player.Play(Column, board);
        }
    }
}