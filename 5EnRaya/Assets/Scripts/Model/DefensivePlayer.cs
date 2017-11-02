using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DefensivePlayer : CPUPlayer
{
    Random rnd = new Random();
    public DefensivePlayer(string name) : base(name) {}

    public override Move GetMove(Board board)
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
                        if (t.Player == this)
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
            .Select(sq => new Move(sq.Column, this))
            .FirstOrDefault();

        // No move was found, just move random
        if (move == null)
        {
            int col = rnd.Next(board.Width);
            move = new Move(col, this);
        }

        return move;
    }
}
