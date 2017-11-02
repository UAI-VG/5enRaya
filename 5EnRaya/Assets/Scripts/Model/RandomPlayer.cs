using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RandomPlayer : CPUPlayer
{
    Random rnd = new Random();

    public RandomPlayer(string name) : base(name) {}

    public override Move GetMove(Board b)
    {
        int col;
        do
        {
            col = rnd.Next(b.Width);
        }
        while (b.ColumnIsFull(col));
        return new Move(col, this);
    }
}

