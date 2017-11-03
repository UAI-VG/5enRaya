using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CincoEnRaya.Model.Strategies
{
    public abstract class Strategy
    {
        private Strategy next;

        public Strategy(Strategy next)
        {
            this.next = next;
        }

        public Strategy Next
        {
            get { return next; }
        }

        public abstract Move GetMove(Player player, Board board);
    }
}