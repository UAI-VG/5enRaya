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
        for (int col = 0; col < board.Width; col++)
        {
            for (int row = 0; row < board.Height; row++)
            {
                Move move = FindMoveOn(col, row, board);
                if (move != null) return move;
            }
        }

        // No move was found, just move random
        {
            int col = rnd.Next(board.Width);
            return new Move(col, this);
        }
    }

    private Move FindMoveOn(int col, int row, Board board)
    {
        Move move = null;
        Func<int, int, bool> check = (c, r) =>
        {
            move = null;
            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                Token t = board.Get(col + i * c, row + i * r);
                if (t == null)
                {
                    // Acá hay una vacía
                    move = new Move(col + i * c, this);
                }
                else if (t.Player == this)
                {
                    count--; 
                }
                else if (t.Player != null)
                {
                    count++;
                }
            }
            return count == 4;
        };
        if (check(1, 0)) return move; // Horizontal 
        if (check(0, 1)) return move; // Vertical 
        if (check(1, 1)) return move; // Ascending diagonal 
        if (check(1, -1)) return move; // Descending diagonal

        // No move found
        return null;
    }
}
