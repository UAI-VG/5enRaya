using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CincoEnRaya.Model.Strategies;

namespace CincoEnRaya.Model
{
    public class Player
    {
        private string name;
        private Strategy strategy;

        public Player(string name, Strategy strategy)
        {
            this.name = name;
            this.strategy = strategy;
        }

        public string Name { get { return name; } }
        public Strategy Strategy { get { return strategy; } }

        public void Play(int column, Board board)
        {
            Token token = new Token(this);
            board.Put(token, column);
        }

        public void BeginTurn(Game game)
        {
            Move move = Strategy.GetMove(this, game.Board);
            if (move != null)
            {
                game.Play(move);
            }
        }
    }
}