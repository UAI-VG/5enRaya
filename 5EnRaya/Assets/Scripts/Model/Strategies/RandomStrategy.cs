﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RandomStrategy : Strategy
{
    private static Random rnd = new Random();

    public RandomStrategy() : base(null)
    {}

    public override Move GetMove(Player player, Board board)
    {
        int col;
        do
        {
            col = rnd.Next(board.Width);
        }
        while (board.ColumnIsFull(col));
        return new Move(col, player);
    }
}
