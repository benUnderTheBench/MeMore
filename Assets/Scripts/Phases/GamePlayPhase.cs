using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class GamePlayPhase : MonoBehaviour,IPhase
{
    //押されたボタンが光る時間
    private float flashDuration = 0;
    private TextMeshProUGUI turnText;
    public Queue<int> inputQueue = new Queue<int>();
    private ButtonBehavior[] buttons;
    
    public void initialize(float duration,TextMeshProUGUI text,ButtonBehavior[] buttons)
    {
        setFlashDuration(duration);
        setTurnText(text);
        setButtons(buttons);
    }

    //パラメータ設定
    private void setFlashDuration(float duration)
    {
        if(flashDuration == 0 && duration > 0)
        {
            flashDuration = duration;
        }
    }
    private void setTurnText(TextMeshProUGUI text)
    {
        if(turnText == null)
        {
            turnText = text;
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
        int currentScore = gameData.expButtonList.Count;

        UIController.refreshText(turnText,currentScore);
        for(int i = 0; i < currentScore; i++)
        {
            while (gameData.getIsPlaying())
            {
                if(inputQueue.Count <= 0)
                {
                    yield return null;
                    continue;
                }
                else
                {
                    int aButton = inputQueue.Dequeue();
                    gameData.actButtonList.Add(aButton);

                    if(aButton == gameData.expButtonList[i])
                    {
                        Debug.Log("correct");
                        UIController.refreshText(turnText,currentScore,i);
                        yield return StartCoroutine(buttons[aButton].changeColor(Color.blue,flashDuration));
                        if(i == currentScore - 1)
                        {
                            gameData.actButtonList.Clear();
                        }
                        break;
                    }
                    else
                    {
                        Debug.Log(gameData.actButtonList[gameData.actButtonList.Count - 1]);
                        Debug.Log("miss");
                        yield return StartCoroutine(buttons[aButton].changeColor(Color.red));
                        gameData.setIsPlaying(false);
                        inputQueue.Clear();
                        yield break;
                    }
                    
                    
                }
            }
            
            
        }
        
        yield break;

    }
}
