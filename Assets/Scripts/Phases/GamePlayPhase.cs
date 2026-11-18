using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.UI;
using UnityEngine;


public class GamePlayPhase : MonoBehaviour,IPhase
{
    //押されたボタンが光る時間
    private float flashDuration = 0;
    private TextMeshProUGUI turnText;
    public Queue<int> inputQueue = new Queue<int>();
    
    //パラメータ設定
    public void setFlashDuration(float duration)
    {
        if(flashDuration == 0 && duration > 0)
        {
            flashDuration = duration;
        }
    }
    public void setTurnText(TextMeshProUGUI text)
    {
        if(turnText == null)
        {
            turnText = text;
        }
    }

    
    public IEnumerator Tick(GameData gameData, ButtonBehavior[] buttons)
    {
        turnText.text = "0 / " + gameData.expButtonList.Count.ToString();
        for(int i = 0; i < gameData.expButtonList.Count; i++)
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
                    gameData.actButtonList.Append(aButton);

                    if(aButton == gameData.expButtonList[i])
                    {
                        Debug.Log("correct");
                        turnText.text =  $"{i + 1} / {gameData.expButtonList.Count}";
                        yield return StartCoroutine(buttons[aButton].changeColor(Color.blue,flashDuration));
                        break;
                    }
                    else
                    {
                        Debug.Log("miss");
                        yield return StartCoroutine(buttons[aButton].changeColor(Color.red));
                        gameData.setIsPlaying(false);
                        inputQueue.Clear();
                        yield break;
                    }
                    
                    
                }
            }
            
            
        }
        gameData.actButtonList.Clear();
        yield break;

    }
}
