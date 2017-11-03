using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CincoEnRaya.Model.Strategies
{
    public abstract class Strategy
    {
        public abstract Move GetMove(Player player, Board board);
    }
}