using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] private int buttonNum;
    private Queue<int> inputQueue;
    private bool isInteractable;
    //押したボタンが次に押せるようになるまでの時間
    private float touchCooltime = 0;

    private void Awake()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(this.onClick);
    }

//---------パラメータ設定-----------------
    public void setIsInteractable(bool b)
    {
        isInteractable = b;
    }

    public void setCooltime(float cooltime)
    {
        if(touchCooltime == 0 && cooltime > 0)
        {
            touchCooltime = cooltime;
        }
    }

       public void setInputQueue(Queue<int> q)
        {
            if(inputQueue == null)
            {
                inputQueue = q;
            }
            
        }
//--------------------------------------

    public IEnumerator changeColor(Color color, float duration = 0)
    {
        var image = GetComponent<Image>();
        image.color = color;
        if(duration <= 0)
        {
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(duration);
            image.color = Color.white;
            yield break;
        }
    }

    public void onClick()
    {
        StartCoroutine(enqueueButtonNum());
    }
    
    public IEnumerator enqueueButtonNum()
    {
        if (isInteractable)
        {
            isInteractable = false;
            inputQueue.Enqueue(buttonNum);
            yield return new WaitForSeconds(touchCooltime);
            isInteractable = true;
        }
        
    }
}
