using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Strategy
{
    public abstract Move GetMove(Player player, Board board);
}
