﻿using System;
using System.Collections;
using System.Collections.Generic;

public class Game
{
    public event Action TurnEnded = () => { };

    private Board board;
    private Player[] players;
    private int turn = -1;
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
    
    public int IndexOfPlayer(Player player)
    {
        return Array.IndexOf(players, player);
    }

    public void Play(Move move)
    {
        if (IsOver) return;
        try
        {
            move.ExecuteOn(board);
            TurnEnded();
        }
        catch (InvalidOperationException)
        {
            // Do nothing. Mala práctica!            
        }
    }

    public void Play(int column)
    {
        Play(new Move(column, CurrentPlayer));
    }

    public void NextTurn()
    {
        turn = (turn + 1) % players.Length;
        CurrentPlayer.BeginTurn(this);
    }
}
