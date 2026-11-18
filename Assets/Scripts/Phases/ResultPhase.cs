using System.Collections;
using TMPro;
using UnityEngine;

public class ResultPhase : MonoBehaviour,IPhase
{
    private GameObject resultPannel;

    public void setResultPannel(GameObject obj)
    {
        if(resultPannel == null)
        {
            resultPannel = obj;
        }
    }

    public IEnumerator Tick(GameData gameData, ButtonBehavior[] buttons)
    {
        resultPannel.SetActive(true);

        while (true)
        {
            yield return null;
            continue;
        }
    }


}
