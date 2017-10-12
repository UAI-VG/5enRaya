using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Token
{
    private Player player;

    public Token(Player player)
    {
        this.player = player;
    }

    public Player Player { get { return player; } }
}

