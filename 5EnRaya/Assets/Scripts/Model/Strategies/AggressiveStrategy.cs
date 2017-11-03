using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CincoEnRaya.Model.Strategies
{
    public class AggressiveStrategy : Strategy
    {
        private Strategy next;
        private int groupSize;

        public AggressiveStrategy(int groupSize, Strategy next)
        {
            this.groupSize = groupSize;
            this.next = next;
        }

        public override Move GetMove(Player player, Board board)
        {
            Move move = null;
            move = board
                .GetGroupsOf(groupSize)
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
                    return count == groupSize - 1;
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
                move = next.GetMove(player, board);
            }

            return move;
        }
    }
}