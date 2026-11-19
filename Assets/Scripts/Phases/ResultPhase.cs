using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultPhase : MonoBehaviour,IPhase
{
    private ButtonBehavior[] buttons;
    private GameObject resultPannel;
    private GameObject extendGameButton;
    private GameObject titleButton;
    private TextMeshProUGUI resultText;

    public void initialize(ButtonBehavior[] buttons, GameObject resultPanel, GameObject extendGameButton, GameObject titleButton, TextMeshProUGUI resultText)
    {
        setButtons(buttons);
        setResultPannel(resultPanel);
        setExtendGameButton(extendGameButton);
        setTitleButton(titleButton);
        setResultText(resultText);
    }

    private void setResultPannel(GameObject obj)
    {
        if(resultPannel == null)
        {
            resultPannel = obj;
        }
    }

    private void setExtendGameButton(GameObject obj)
    {
        if(extendGameButton == null)
        {
            extendGameButton = obj;
        }
    }

    private void setTitleButton(GameObject obj)
    {
        if(titleButton == null)
        {
            titleButton = obj;
            titleButton.GetComponent<Button>().onClick.AddListener(toTitle);
        }
    }

    private void setResultText(TextMeshProUGUI resultText)
    {
        if(this.resultText == null)
        {
            this.resultText = resultText;
        }
    }

    private void setButtons(ButtonBehavior[] buttons)
    {
        if(this.buttons == null)
        {
            this.buttons = buttons;
        }
    }

    public IEnumerator Tick(GameData gameData)
    {
        UIController.activateResultPanel(gameData, resultPannel, extendGameButton, resultText);
        extendGameButton.GetComponent<Button>().onClick.AddListener(() => extendGame(gameData));

        while (true)
        {
            if (gameData.getIsPlaying())
            {
                UIController.deactivateResultPanel(buttons, resultPannel, extendGameButton);
                yield break;
            }
            yield return null;
            continue;
        }
    }

    private void extendGame(GameData gamedata)
    {
        int lastButton = gamedata.actButtonList[gamedata.actButtonList.Count - 1];
        StartCoroutine(buttons[lastButton].changeColor(Color.white));
        gamedata.setIsPlaying(true);
        gamedata.setExtendedGame(true);
    }

    private void toTitle()
    {
        SceneManager.LoadScene("SampleScene");
    }


}
