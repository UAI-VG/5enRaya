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
        Func<int, int, bool> check = (c, r) =>
        {
            int count = 1;
            for (int i = 1; i < 5; i++)
            {
                Token t = Get(col + i * c, row + i * r);
                if (t == null) break;
                if (t.Player != player) break;

                count++;
            }
            return count == 5;
        };
        if (check(1,  0)) return player; // Horizontal 
        if (check(0,  1)) return player; // Vertical 
        if (check(1,  1)) return player; // Ascending diagonal 
        if (check(1, -1)) return player; // Descending diagonal

        // No winner found
        return null;
    }
}
