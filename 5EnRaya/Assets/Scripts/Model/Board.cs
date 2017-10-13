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

    public bool IsFull { get { return tokens.All(token => token != null); } }

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

    public Token Get(int column, int row)
    {
        return Get(IndexOf(column, row));
    }

    public Token Get(int index)
    {
        if (index < 0 || index >= tokens.Length) return null;
        return tokens[index];
    }
    
    private int IndexOf(int column, int row)
    {
        return column + row * width;
    }

    public Player DetectWinner()
    {
        for (int col = 0; col < width; col++)
        {
            for (int row = 0; row < height; row++)
            {
                Player winner = DetectWinnerOn(col, row);
                if (winner != null) return winner;
            }
        }
        return null;
    }

    private Player DetectWinnerOn(int col, int row)
    {
        Token token = Get(col, row);
        if (token == null) return null;

        Player player = token.Player;

        // Horizontal check
        {
            int count = 1;
            for (int i = 1; i < 5; i++)
            {
                Token t = Get(col + i, row);
                if (t == null) break;
                if (t.Player != player) break;

                count++;
            }
            if (count == 5) return player;
        }

        // Vertical check
        {
            int count = 1;
            for (int i = 1; i < 5; i++)
            {
                Token t = Get(col, row + i);
                if (t == null) break;
                if (t.Player != player) break;

                count++;
            }
            if (count == 5) return player;
        }

        // Ascending diagonal check
        {
            int count = 1;
            for (int i = 1; i < 5; i++)
            {
                Token t = Get(col + i, row + i);
                if (t == null) break;
                if (t.Player != player) break;

                count++;
            }
            if (count == 5) return player;
        }


        // Descending diagonal check
        {
            int count = 1;
            for (int i = 1; i < 5; i++)
            {
                Token t = Get(col + i, row - i);
                if (t == null) break;
                if (t.Player != player) break;

                count++;
            }
            if (count == 5) return player;
        }

        // No winner found
        return null;
    }
}
