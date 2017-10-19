using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CPUPlayer : Player
{
    Random rnd = new Random();
    public CPUPlayer(string name) : base(name) {}

    public override void BeginTurn(Game game)
    {
        game.Play(GetMove(game.Board));
    }
    
    public Move GetMove(Board b)
    {
        int col = rnd.Next(b.Width);
        return new Move(col, this);
    }
}
