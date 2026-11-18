using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private bool isPlaying;
    //ボタン9個の配列
    [SerializeField] private ButtonBehavior[] buttons;

    //使うフェーズ
    [SerializeField] private SetupPhase setupPhase;
    [SerializeField] private GamePlayPhase gamePlayPhase;
    [SerializeField] private ResultPhase resultPhase;

    //ゲームを通してデータを入れておく場所
    public GameData gameData;

    //正解してから次のボタンが押されるまでの時間(ゲームテンポの問題)
    [SerializeField] private float waitDuration;
    //押したボタンが光る時間
    [SerializeField] private float touchFlashDuration;
    //次に押すボタンが光る時間
    [SerializeField] private float indicateFlashDuration;
    //ボタンを押してから次に押せるようになるまでの時間
    [SerializeField] private float touchCooltime;
    //リザルト画面で表示するパネル
    [SerializeField] private GameObject resultPanel;
    //現在のターン数を表示するテキスト
    [SerializeField] private TextMeshProUGUI turnText;

    void Start()
    {
        gameData = new GameData();
        foreach(ButtonBehavior button in buttons){
            button.setInputQueue(gamePlayPhase.inputQueue);
            button.setCooltime(touchCooltime);
            button.setIsInteractable(false);
        }

        setupPhase.setFlashDuration(indicateFlashDuration);
        gamePlayPhase.setFlashDuration(touchFlashDuration);
        gamePlayPhase.setTurnText(turnText);
        resultPhase.setResultPannel(resultPanel);
    }

    private void setIsInteractable(bool b)
    {
        foreach(ButtonBehavior button in buttons)
        {
            button.setIsInteractable(b);
        }
    }
    public void startMainRoop()
    {
        StartCoroutine(MainRoop());
    }
    public IEnumerator MainRoop()
    {
        gameData.setIsPlaying(true);

        yield return StartCoroutine(setupPhase.Tick(gameData,buttons));
        setIsInteractable(true);
        
        yield return StartCoroutine(gamePlayPhase.Tick(gameData,buttons));
        setIsInteractable(false);
        
        while (gameData.getIsPlaying())
        {
            yield return new WaitForSeconds(waitDuration);

            yield return StartCoroutine(setupPhase.Tick(gameData,buttons));
            setIsInteractable(true);
            
            yield return StartCoroutine(gamePlayPhase.Tick(gameData,buttons));
            setIsInteractable(false);
            
        }
        yield return StartCoroutine(resultPhase.Tick(gameData,buttons));
        
    }
}

public class GameData
{
    public List<int> expButtonList = new List<int>();
    public List<int> actButtonList = new List<int>(); 
    private bool isPlaying = false;

    public void setIsPlaying(bool b)
    {
        isPlaying = b;
    }   

    public bool getIsPlaying()
    {
        return isPlaying;
    }  
}

public interface IPhase
{
    public IEnumerator Tick(GameData gameData, ButtonBehavior[] buttons);
}

