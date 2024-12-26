using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

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

    private bool verticalCheck(char[,] arr, int row, int column)
    {
        return arr[row, column] == arr[row - 1, column] && arr[row, column] == arr[row + 1, column];
    }

    private bool horizontalCheck(char[,] arr, int row, int column)
    {
        return arr[row, column] == arr[row, column - 1] && arr[row, column] == arr[row, column + 1];
    }

    private bool leftDiagonalCheck(char[,] arr, int row, int column)
    {
        return arr[row, column] == arr[row - 1, column - 1] && arr[row, column] == arr[row + 1, column + 1];
    }

    private bool rightDiagonalCheck(char[,] arr, int row, int column)
    {
        return arr[row, column] == arr[row + 1, column - 1] && arr[row, column] == arr[row - 1, column + 1];
    }

    private void CheckForGameOver(char[,] moveArray)
    {
        string playerWithX = playerNames.GetPlayer1Name();
        string playerWithO = playerNames.GetPlayer2Name();
        char gameWinnerChar = ' ';

        for(int i = 1; i < 4; i++)
        {
            for(int j = 1; j < 4; j++)
            {
                if (moveArray[i, j] == ' ')
                    continue;
                if (verticalCheck(moveArray, i, j) || horizontalCheck(moveArray, i, j) || leftDiagonalCheck(moveArray, i, j) || rightDiagonalCheck(moveArray, i, j))
                {
                    gameIsOver = true;
                    gameWinnerChar = moveArray[i, j];
                    break;
                }
            }
        }

        if (gameIsOver)
        {
            for (int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    Button button = GameObject.Find(i.ToString() + j.ToString()).GetComponent<Button>();
                    if (moveArray[i, j] == ' ')
                        button.image.color = Camera.main.backgroundColor;
                }
            }
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
