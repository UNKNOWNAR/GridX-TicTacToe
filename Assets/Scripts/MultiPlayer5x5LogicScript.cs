using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiPlayer5x5LogicScript : MonoBehaviour
{
    [SerializeField] private Sprite X;
    [SerializeField] private Sprite O;
    [SerializeField] private Sprite XWIN;
    [SerializeField] private Sprite OWIN;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Text result;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private SavePlayerNamesScript playerNames;

    private int currentButtonRowNumber, currentButtonColumnNumber, numberOfMoves = 0;
    private bool isTurnOfX = true;
    private char[,] moveArray = new char[5, 5];
    bool gameIsOver = false;

    private Button button;
    private int winrange = 0;
    private char CharWin = ' ';
    private char nextCharacter = ' ';

    private void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                moveArray[i, j] = ' ';
            }
        }
    }

    public void OnButtonClicked(Button clickedButton)
    {
        if (gameIsOver)
        {
            return;
        }
        currentButtonRowNumber = int.Parse(clickedButton.name.Substring(0, 1));
        currentButtonColumnNumber = int.Parse(clickedButton.name.Substring(1));
        Debug.Log(currentButtonRowNumber+" "+currentButtonColumnNumber);
        if (isTurnOfX)
        {
            clickedButton.image.sprite = X;
            moveArray[currentButtonRowNumber, currentButtonColumnNumber] = 'X';
            isTurnOfX = false;
        }
        else
        {
            clickedButton.image.sprite = O;
            moveArray[currentButtonRowNumber, currentButtonColumnNumber] = 'O';
            isTurnOfX = true;
        }
        clickedButton.enabled = false;
        numberOfMoves++;
        CheckForGameOver(moveArray);
    }

    private void CheckForGameOver(char[,] moveArray)
    {
        string playerWithX = playerNames.GetPlayer1Name();
        string playerWithO = playerNames.GetPlayer2Name();
        char gameWinnerChar = ' ';
        for(int i = 0;i < 5;i++)
        {
            if (moveArray[i, 0] == moveArray[i, 1] && moveArray[i, 0] == moveArray[i, 2] && moveArray[i, 0] == moveArray[i, 3] && moveArray[i, 0] == moveArray[i, 4] && moveArray[i, 0] != ' ')
            {
                gameIsOver = true;
                gameWinnerChar = moveArray[i, 0];
                break;
            }

            if (moveArray[0, i] == moveArray[1, i] && moveArray[0, i] == moveArray[2, i] && moveArray[0, i] == moveArray[3, i] && moveArray[0, i] == moveArray[4, i] && moveArray[0, i] != ' ')
            {
                gameIsOver = true;
                gameWinnerChar = moveArray[0, i];
                break;
            }
        }
        if (moveArray[0, 0] == moveArray[1, 1] && moveArray[0, 0] == moveArray[2, 2] && moveArray[0, 0] == moveArray[3, 3] && moveArray[0, 0] == moveArray[4, 4] && moveArray[0, 0] != ' ')
        {
            gameIsOver = true;
            gameWinnerChar = moveArray[0, 0];
        }
        if (moveArray[0, 4] == moveArray[1, 3] && moveArray[0, 4] == moveArray[2, 2] && moveArray[0, 4] == moveArray[3, 1] && moveArray[0, 4] == moveArray[4, 0] && moveArray[0, 4] != ' ')
        {
            gameIsOver = true;
            gameWinnerChar = moveArray[0, 4];
        }

        if (gameIsOver)
        {
            if (gameWinnerChar == 'X')
            {
                result.text = playerWithX + " Wins!";
            }
            else if (gameWinnerChar == 'O')
            {
                result.text = playerWithO + " Wins!";
            }

            EnableGameOverScreen();
        }
        else if (numberOfMoves == 25)
        {
            result.text = "Game is a Draw";
            EnableGameOverScreen();
        }
    }

    private void EnableGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }
}
