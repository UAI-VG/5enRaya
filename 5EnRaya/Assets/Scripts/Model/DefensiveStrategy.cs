﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DefensiveStrategy : Strategy
{
    Random rnd = new Random();

    public DefensiveStrategy(Strategy next) : base(next)
    {}

    public override Move GetMove(Player player, Board board)
    {
        Move move = board.GetGroupsOf(5)
            .Where(group =>
            {
                int count = 0;
                foreach (Square sq in group)
                {
                    Token t = board.Get(sq);
                    if (t != null)
                    {
                        if (t.Player == player)
                        {
                            count--;
                        }
                        else if (t.Player != null)
                        {
                            count++;
                        }
                    }

                }
                return count == 4;
            })
            .Select(group =>
            {
                return group.First(sq => board.Get(sq) == null);
            })
            .Where(sq => board.IsPlaceable(sq))
            .Select(sq => new Move(sq.Column, player))
            .FirstOrDefault();

        // No move was found, try next strategy
        if (move == null)
        {
            move = Next.GetMove(player, board);
        }

        return move;
    }
}