using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CincoEnRaya.Model
{
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

        public Token Get(Square sq)
        {
            return Get(sq.Column, sq.Row);
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

        public bool IsInside(Square sq)
        {
            return !IsOutside(sq);
        }

        public bool IsOutside(Square sq)
        {
            return sq.Column < 0
                || sq.Column >= Width
                || sq.Row < 0
                || sq.Row >= Height;
        }

        public bool IsPlaceable(Square sq)
        {
            if (sq.Row == 0) return Get(sq) == null;

            for (int i = 0; i < sq.Row; i++)
            {
                if (Get(sq.Column, i) == null) return false;
            }
            return true;
        }

        public bool ColumnIsFull(int col)
        {
            for (int i = 0; i < Height; i++)
            {
                if (Get(col, i) == null) return false;
            }
            return true;
        }

        private int IndexOf(int column, int row)
        {
            return column + row * width;
        }

        public Square[][] GetGroupsOf(int size)
        {
            List<Square[]> groups = new List<Square[]>();
            for (int col = 0; col < width; col++)
            {
                for (int row = 0; row < height; row++)
                {
                    groups.AddRange(GetGroupsOfOn(size, col, row));
                }
            }
            return groups
                .Where(group => group.All(sq => IsInside(sq)))
                .ToArray();
        }

        private Square[][] GetGroupsOfOn(int size, int col, int row)
        {
            List<Square[]> groups = new List<Square[]>();
            Func<int, int, Square[]> group = (c, r) =>
            {
                List<Square> squares = new List<Square>();
                for (int i = 0; i < size; i++)
                {
                    squares.Add(new Square(col + i * c, row + i * r));
                }
                return squares.ToArray();
            };
            groups.Add(group(1, 0)); // Horizontal 
            groups.Add(group(0, 1)); // Vertical 
            groups.Add(group(1, 1)); // Ascending diagonal 
            groups.Add(group(1, -1)); // Descending diagonal

            return groups.ToArray();
        }

        public Player DetectWinner()
        {
            return GetGroupsOf(5)
                .Where(group =>
                {
                    int count = 0;
                    Player player = null;
                    foreach (Square sq in group)
                    {
                        Token t = Get(sq);
                        if (t != null)
                        {
                            if (player == null) { player = t.Player; }
                            if (t.Player == player) { count++; }
                        }
                    }
                    return count == 5;
                })
                .Select(group => Get(group[0]).Player)
                .FirstOrDefault();
        }
    }
}