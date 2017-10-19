using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class HumanPlayer : Player
{
    public HumanPlayer(string name) : base(name) {}

    public override void BeginTurn(Game game)
    {
        // Do nothing. Wait for the UI interaction
    }
}
