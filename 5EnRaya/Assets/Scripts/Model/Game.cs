using System;
using System.Collections;
using System.Collections.Generic;

public class Game
{
    private Board board;
    private Player[] players;
    private int turn = 0;
    public Game(Board board, Player[] players)
    {
        this.board = board;
        this.players = players;
    }

    public Board Board { get { return board; } }
    public IEnumerable<Player> Players { get { return players; } }
    public int Turn { get { return turn; } }
    public Player CurrentPlayer { get { return players[turn]; } }
    
    public int IndexOfPlayer(Player player)
    {
        return Array.IndexOf(players, player);
    }

    public void Play(int column)
    {
        try
        {
            CurrentPlayer.Play(column, board);
            NextTurn();
        }
        catch (InvalidOperationException)
        {
            // Do nothing
        }
    }

    private void NextTurn()
    {
        turn = (turn + 1) % players.Length;
    }
}
