using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SetupPhase : MonoBehaviour, IPhase
{
    //次に押すボタンが光る時間
    private float flashDuration = 0;
    private ButtonBehavior[] buttons;

    public void initialize(float duration,ButtonBehavior[] buttons)
    {
        setFlashDuration(duration);
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

    private void setButtons(ButtonBehavior[] buttons)
    {
        if(this.buttons == null)
        {
            this.buttons = buttons;
        }
    }

    public IEnumerator Tick(GameData gameData)
    {
        int eButtonNum = Random.Range(minInclusive:0,maxExclusive:9);
        Debug.Log(eButtonNum);
        gameData.expButtonList.Add(eButtonNum);
        Debug.Log($"buttons:{buttons.Length}, exp:{gameData.expButtonList.Count}, act:{gameData.actButtonList.Count}");
        yield return StartCoroutine(buttons[eButtonNum].changeColor(Color.blue,flashDuration));
        yield break;
    }
}
