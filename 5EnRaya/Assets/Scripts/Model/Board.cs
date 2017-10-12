using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Board
{
    private int width;
    private int height;
    private Token[] tokens;

    public Board(int w, int h)
    {
        width = w;
        height = h;
        tokens = new Token[w * h];
    }

    public int Width { get { return width; } }
    public int Height { get { return height; } }

    public void Put(Token token, int column)
    {
        for (int row = 0; row < height; row++)
        {
            int index = IndexOf(column, row);
            if (tokens[index] == null)
            {
                tokens[index] = token;
                return;
            }
        }
        throw new InvalidOperationException("Column is full!");
    }

    public Token Get(int index)
    {
        return tokens[index];
    }
    
    private int IndexOf(int column, int row)
    {
        return column + row * width;
    }
}
