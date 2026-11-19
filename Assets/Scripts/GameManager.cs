using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    

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

    //------UI
    //ボタン9個の配列
    [SerializeField] private ButtonBehavior[] buttons;
    //終了時に表示するパネル
    [SerializeField] private GameObject resultPanel;
    //一回だけ押せる、リトライボタン(スコアはそのまま)
    [SerializeField] private GameObject extendGameButton;
    //タイトルへ戻るボタン
    [SerializeField] private GameObject titleButton;
    //最終的なスコアを表示するテキスト
    [SerializeField] private TextMeshProUGUI resultText;
    //現在のターン数を表示するテキスト
    [SerializeField] private TextMeshProUGUI turnText;

    void Start()
    {
        gameData = new GameData();
        UIController.initializeButtons(buttons,gamePlayPhase.inputQueue,touchCooltime);
        
        setupPhase.initialize(indicateFlashDuration,buttons);
        gamePlayPhase.initialize(touchFlashDuration,turnText,buttons);
        resultPhase.initialize(buttons, resultPanel,extendGameButton, titleButton, resultText);
    }

    public void startMainRoop()
    {
        StartCoroutine(MainRoop());
    }
    public IEnumerator MainRoop()
    {
        gameData.setIsPlaying(true);
        
        yield return StartCoroutine(setupPhase.Tick(gameData));
        UIController.changeButtonsInteractability(buttons,true);

        while (gameData.getIsPlaying())
        {

            yield return StartCoroutine(gamePlayPhase.Tick(gameData));
            UIController.changeButtonsInteractability(buttons,false);

            if (gameData.getIsPlaying())
            {
                yield return new WaitForSeconds(waitDuration);

                yield return StartCoroutine(setupPhase.Tick(gameData));
                UIController.changeButtonsInteractability(buttons,true);
            }
            else
            {
                yield return StartCoroutine(resultPhase.Tick(gameData));
            }
            
        }
        
    }

    
}



