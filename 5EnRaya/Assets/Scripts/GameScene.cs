using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    public GameObject gridButtonsPanel;
    public GameObject gridPanel;
    public Text turnText;

    public Color[] playerColors;

    public Sprite[] playerCellSprites;
    public Sprite emptyCellSprite;

    private Game game;
    
	void Start ()
    {
        InitializeGame();
        InitializeButtons();
    }

    private void InitializeGame()
    {
        Board board = new Board(8, 6);
        Player[] players = new Player[]
        {
            new Player("Jugador 1"),
            new Player("Jugador 2"),
        };
        game = new Game(board, players);
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
}
