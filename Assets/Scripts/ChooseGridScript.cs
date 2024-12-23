using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseGridScript : MonoBehaviour
{
    [SerializeField] private GameObject grid3x3Canvas;
    [SerializeField] private GameObject grid5x5Canvas;
    [SerializeField] private GameObject playerNamesCanvas;
    [SerializeField] private Button gridChoiceButton;
    [SerializeField] private Text gridChoiceText;

    private const string PLAYER_CHOICE_GRID = "PlayerChoiceGrid";
    private const string _3X3 = "3x3";
    private const string _5X5 = "5x5";

    private void Start()
    {
        if (PlayerPrefs.HasKey(PLAYER_CHOICE_GRID))
        {
            gameObject.SetActive(false);
            playerNamesCanvas.SetActive(true);
            gridChoiceText.text = PlayerPrefs.GetString(PLAYER_CHOICE_GRID);
        }
    }

    public void On3x3ButtonClicked()
    {
        gameObject.SetActive(false);
        playerNamesCanvas.SetActive(true);
        PlayerPrefs.SetString(PLAYER_CHOICE_GRID, _3X3);
        PlayerPrefs.Save();
        gridChoiceText.text = PlayerPrefs.GetString(PLAYER_CHOICE_GRID);
    }

    public void On5x5ButtonClicked()
    {
        gameObject.SetActive(false);
        playerNamesCanvas.SetActive(true);
        PlayerPrefs.SetString(PLAYER_CHOICE_GRID, _5X5);
        PlayerPrefs.Save();
        gridChoiceText.text = PlayerPrefs.GetString(PLAYER_CHOICE_GRID);
    }

    public void OnGridChoiceButtonClicked()
    {
        grid3x3Canvas.SetActive(false);
        grid5x5Canvas.SetActive(false);
        playerNamesCanvas.SetActive(false);
        gameObject.SetActive(true);
    }

    public void OpenUserChoiceGrid(string value)
    {
        if (value == _3X3)
        {
            grid3x3Canvas.SetActive(true);
        }
        else if (value == _5X5)
        {
            grid5x5Canvas.SetActive(true);
        }
    }
}
