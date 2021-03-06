﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using CincoEnRaya.Model;
using CincoEnRaya.Model.Strategies;

public class GameScene : MonoBehaviour
{
    public float playerDelay = 1;
    public GameObject gridButtonsPanel;
    public GameObject gridPanel;
    public Text turnText;

    public Color[] playerColors;

    public Sprite[] playerCellSprites;
    public Sprite emptyCellSprite;

    public GameObject winnerDialog;

    private Game game;

    void Start()
    {
        InitializeGame();
        InitializeButtons();
    }

    private void InitializeGame()
    {
        Board board = new Board(8, 6);
        Player[] players = new Player[]
        {
            new Player("Human",
                new NullStrategy()),
            new Player("CPU", 
                new AggressiveStrategy(
                    5,
                    new DefensiveStrategy(
                        new AggressiveStrategy(
                            4, 
                            new AggressiveStrategy(
                                3,
                                new AggressiveStrategy(
                                    2, 
                                    new RandomStrategy())))))),
        };
        game = new Game(board, players);
        game.TurnEnded += game_TurnEnded;
        game.NextTurn();
    }

    private void game_TurnEnded()
    {
        StartCoroutine(NextTurn());
    }

    private IEnumerator NextTurn()
    {
        yield return new WaitForSeconds(playerDelay);
        game.NextTurn();
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < gridButtonsPanel.transform.childCount; i++)
        {
            Transform child = gridButtonsPanel.transform.GetChild(i);
            Button button = child.gameObject.GetComponent<Button>();

            int column = i; // INFO(Richo): Variable is captured by closure below
            button.onClick.AddListener(() => game.Play(column));
        }
    }

    void Update()
    {
        UpdateBoard();
        UpdateTurn();
        UpdateButtons();
        UpdateWinnerDialog();
    }
    
    private void UpdateBoard()
    {
        Board board = game.Board;
        for (int i = 0; i < gridPanel.transform.childCount; i++)
        {
            Transform child = gridPanel.transform.GetChild(i);
            Image img = child.GetComponent<Image>();

            Token token = board.Get(i);
            if (token == null)
            {
                img.sprite = emptyCellSprite;
            }
            else
            {
                Player player = token.Player;
                img.sprite = playerCellSprites[game.IndexOfPlayer(player)];
            }
        }
    }

    private void UpdateButtons()
    {
        int currentPlayerIndex = game.IndexOfPlayer(game.CurrentPlayer);
        for (int i = 0; i < gridButtonsPanel.transform.childCount; i++)
        {
            Transform child = gridButtonsPanel.transform.GetChild(i);
            Image img = child.GetComponent<Image>();
            img.color = playerColors[currentPlayerIndex];
        }
    }

    private void UpdateTurn()
    {
        turnText.text = string.Format("Turno: {0}", game.CurrentPlayer.Name);
    }

    private void UpdateWinnerDialog()
    {
        if (game.IsOver)
        {
            Text winnerText = winnerDialog.GetComponentInChildren<Text>();
            if (game.Winner == null)
            {
                winnerText.text = "¡Empate!";
            }
            else
            {
                winnerText.text = string.Format("¡El ganador es {0}!", game.Winner.Name);
            }
            winnerDialog.SetActive(true);
        }
    }
}
