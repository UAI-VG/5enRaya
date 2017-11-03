using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CincoEnRaya.Model.Strategies
{
    public class NullStrategy : Strategy
    {
        public override Move GetMove(Player player, Board board)
        {
            // Wait for human input
            return null;
        }
    }
}