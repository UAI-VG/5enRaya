using System;
using System.Collections;
using System.Collections.Generic;

namespace CincoEnRaya.Model
{
    public class Game
    {
        public event Action TurnEnded = () => { };

        private Board board;
        private Player[] players;
        private int turn = -1;
        private bool playing = false;
        public Game(Board board, Player[] players)
        {
            this.board = board;
            this.players = players;
        }

        public Board Board { get { return board; } }
        public IEnumerable<Player> Players { get { return players; } }
        public int Turn { get { return turn; } }
        public Player CurrentPlayer { get { return players[turn]; } }
        public Player Winner { get { return board.DetectWinner(); } }
        public bool IsOver { get { return Winner != null || board.IsFull; } }
        public bool Playing { get { return playing && !IsOver; } }

        public int IndexOfPlayer(Player player)
        {
            return Array.IndexOf(players, player);
        }

        public void Play(Move move)
        {
            if (Playing && move.IsValidOn(board))
            {
                move.ExecuteOn(board);
                playing = false;
                TurnEnded();
            }
        }

        public void Play(int column)
        {
            Play(new Move(column, CurrentPlayer));
        }

        public void NextTurn()
        {
            turn = (turn + 1) % players.Length;
            playing = true;
            CurrentPlayer.BeginTurn(this);
        }
    }
}