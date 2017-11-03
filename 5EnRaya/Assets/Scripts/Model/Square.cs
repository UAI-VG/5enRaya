using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CincoEnRaya.Model
{
    public class Square
    {
        private int column;
        private int row;

        public Square(int column, int row)
        {
            this.column = column;
            this.row = row;
        }

        public int Column { get { return column; } }
        public int Row { get { return row; } }
    }
}