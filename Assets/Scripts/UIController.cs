using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static void initializeButtons(ButtonBehavior[] buttons, Queue<int> inputQueue, float coolTime)
    {
        foreach(ButtonBehavior button in buttons)
        {
            button.setInputQueue(inputQueue);
            button.setCooltime(coolTime);
            button.setIsInteractable(false);
        }
    }

    public static void changeButtonsInteractability(ButtonBehavior[] buttons, bool isInteractable)
    {
        foreach(ButtonBehavior button in buttons)
        {
            button.setIsInteractable(isInteractable);
        }
    }

    public static void refreshText(TextMeshProUGUI turnText, int currentScore, int currentNum = -1)
    {
        turnText.text = $"{currentNum + 1} / {currentScore}";
    }

// -------------
    public static void activateResultPanel(GameData gamedata,GameObject resultPanel, GameObject extendGameButton, TextMeshProUGUI resultText)
    {
        resultPanel.SetActive(true);

        resultText.text = $"score : {gamedata.expButtonList.Count}";

        if (!gamedata.getExtendedGame())
        {
            extendGameButton.SetActive(true);
        }
        
    }

    public static void deactivateResultPanel(ButtonBehavior[] buttons, GameObject resultPanel, GameObject extendGameButton)
    {
        changeButtonsInteractability(buttons,true);
        Debug.Log("白に戻してtrueにした");
        extendGameButton.SetActive(false);
        resultPanel.SetActive(false);
    }

}
