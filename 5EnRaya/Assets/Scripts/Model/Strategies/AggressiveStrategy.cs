using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CincoEnRaya.Model.Strategies
{
    public class AggressiveStrategy : Strategy
    {
        Random rnd = new Random();

        public AggressiveStrategy(Strategy next) : base(next)
        { }

        public override Move GetMove(Player player, Board board)
        {
            Move move = null;
            int size = 5;
            while (move == null && size >= 2)
            {
                move = board
                    .GetGroupsOf(size)
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
                                    count++;
                                }
                                else if (t.Player != null)
                                {
                                    count--;
                                }
                            }

                        }
                        return count == size - 1;
                    })
                    .Select(group =>
                    {
                        return group.First(sq => board.Get(sq) == null);
                    })
                    .Where(sq => board.IsPlaceable(sq))
                    .Select(sq => new Move(sq.Column, player))
                    .FirstOrDefault();
                size--;
            }

            // No move was found, try next strategy
            if (move == null)
            {
                move = Next.GetMove(player, board);
            }

            return move;
        }
    }
}