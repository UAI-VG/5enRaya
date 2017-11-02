﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class NullStrategy : Strategy
{
    public NullStrategy() : base(null) {}

    public override Move GetMove(Player player, Board board)
    {
        // Wait for human input
        return null;
    }
}
