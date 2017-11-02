using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class CPUPlayer : Player
{
    public CPUPlayer(string name) : base(name) {}

    public override void BeginTurn(Game game)
    {
        game.Play(GetMove(game.Board));
    }

    public abstract Move GetMove(Board b);
}
