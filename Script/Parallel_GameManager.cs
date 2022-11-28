using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Parallel_GameManager : MonoBehaviour
{
    public static Parallel_GameManager pManager;

    [SerializeField, Header("GameManager")]
    private GameObject gameObj;

    [Header("防衛目標")]
    public GameObject myBase;
    public Transform myBase_Pos;
    [SerializeField, Header("やられたエフェクト")]
    private GameObject explotion;

    [SerializeField, Header("演出用のカメラ")]
    private Camera mainCamera;
    public Vector3 firstPos;
    public Vector3 movePos;//演出があった後のカメラの位置
    public Vector3 lastPos;//最終的なカメラの位置
    [SerializeField, Header("ゲームが開始する時の演出用")]
    private GameObject countDownImage;


    [SerializeField, Header("ゲームが開始する時の演出用")]
    private GameObject backGround_UI;//backgroundにアタッチ


    [SerializeField, Header("ゲームクリア用")]
    private GameObject clearPanel;
    private bool gameClearBool = false;
    //演出用
    [SerializeField]
    private GameObject clear_parts;
    [SerializeField]
    private GameObject clear_img;
    [SerializeField]
    private GameObject clear_button;

    [SerializeField, Header("ゲームオーバー用")]
    private GameObject overPanel;
    private bool gameOverBool = false;
    //演出用
    [SerializeField]
    private GameObject over_parts;
    [SerializeField]
    private GameObject over_img;
    [SerializeField]
    private GameObject over_button;

    [SerializeField, Header("CountTimerに付ける")]
    public GameObject countTimer;
    [SerializeField, Header("制限時間の設定値")]
    public int battleTime;
    [SerializeField, Header("制限時間の表示")]
    public TMP_Text battleTimeText;
    private float timer=0;                  // 時間計測用
    public bool isStartBool = false;


    private void Awake()
    {
        myBase.SetActive(true);
        countDownImage.SetActive(true);
        backGround_UI.SetActive(true);
        clearPanel.SetActive(false);
        clear_button.SetActive(false);
        over_button.SetActive(false);
        overPanel.SetActive(false);
        //over_button.SetActive(false);
        countTimer.SetActive(true);
        if(pManager == null)
        {
            pManager = this;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        RectTransform ImageRect = countDownImage.GetComponent<RectTransform>();
        RectTransform backUIRect = backGround_UI.GetComponent<RectTransform>();
        RectTransform countUIRect = countTimer.GetComponent<RectTransform>();
        mainCamera.transform.position = firstPos;
        mainCamera.transform.DOMove(movePos, 2.5f).SetEase(Ease.InOutQuart);
        mainCamera.transform.DOMove(lastPos, 2.5f).SetDelay(2.5f).SetEase(Ease.InOutQuart);
        //ImageRect：カウントダウン
        ImageRect.DOAnchorPos(new Vector2(0, -1200f), 1f).SetEase(Ease.InOutQuart);
        ImageRect.DOAnchorPos(new Vector2(0, -600f),1f).SetDelay(1f).SetEase(Ease.InOutQuart);
        ImageRect.DOAnchorPos(new Vector2(0, 100f), 1f).SetDelay(2f).SetEase(Ease.InOutQuart);
        ImageRect.DOAnchorPos(new Vector2(0, 600f), 1f).SetDelay(3f).SetEase(Ease.InOutQuart);
        ImageRect.DOAnchorPos(new Vector2(0, 1200f), 1f).SetDelay(4f).SetEase(Ease.InOutQuart).SetLink(countDownImage);

        //backGroundUI
        backUIRect.DOAnchorPos(new Vector2(120, -150f), 0.1f);
        backUIRect.DOAnchorPos(new Vector2(120, 100f), 1f).SetDelay(5f).SetEase(Ease.InOutQuart);

        //countTimer
        countUIRect.DOAnchorPos(new Vector2(0, -100f), 1f).SetDelay(5f).SetEase(Ease.InOutQuart);
        DisplayBattleTime(battleTime);

        DOVirtual.DelayedCall(7f,()=> isStart(), false);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(countDownImage, 5.5f);

        if (isStartBool)
        {

            timer += Time.deltaTime;

            // 1秒経過ごとにtimerを0に戻し、battleTime(currentTime)を減算する
            if (timer >= 1)
            {
                timer = 0;
                battleTime--;  // あるいは、currentTime--;
                               // 時間表示を更新するメソッドを呼び出す
                DisplayBattleTime(battleTime);   // あるいは、DisplayBattleTime(currentTime);
            }
            if (battleTime < 0)
            {
                battleTimeText.text = "0";
                GameClear();
            }
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            GameOver();
        }
    }
    public void GameClear()
    {
        if (!gameOverBool)
        {
            gameClearBool = true;
            isStartBool = false;
            backGround_UI.SetActive(false);
            clearPanel.SetActive(true);
            overPanel.SetActive(false);
            gameObj.GetComponent<SwipeEffect>().enabled = false;
            countTimer.SetActive(false);
            clear_button.SetActive(true);
            //ゲーム演出
            var clear_rect = clear_parts.GetComponent<Image>();
            RectTransform clear_rectimg = clear_img.GetComponent<RectTransform>();
            clear_rect.DOFade(1f, 1.5f);
            clear_rectimg.DOAnchorPos(new Vector2(0, 300f), 4f).SetEase(Ease.InOutQuart);
            clear_rectimg.DOLocalRotate(new Vector3(0, 0, 360f), 6f, RotateMode.FastBeyond360)
            .SetDelay(1f)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
        }    }
    public void GameOver()
    {
        if (!gameClearBool)
        {
            gameOverBool = true;
            isStartBool = false;
            var Explosion = Instantiate(explotion, myBase_Pos) as GameObject;
            Destroy(Explosion, 3f);
            countTimer.SetActive(false);
            over_button.SetActive(true);
            backGround_UI.SetActive(false);
            //over_button.SetActive(true);
            clearPanel.SetActive(false);
            overPanel.SetActive(true);
            gameObj.GetComponent<SwipeEffect>().enabled = false;

            //ゲーム演出
            var over_rect = over_parts.GetComponent<Image>();
            RectTransform over_rectimg = over_img.GetComponent<RectTransform>();
            over_rect.DOFade(1.0f, 1.5f);
            over_rectimg.DOAnchorPos(new Vector2(0, 250f), 4f).SetEase(Ease.InOutQuart);

        }
    }

    private void DisplayBattleTime(int limitTime)
    {
        // 引数で受け取った値を[分:秒]に変換して表示する
        // ToString("00")でゼロプレースフォルダーして、１桁のときは頭に0をつける
        battleTimeText.text = (((int)limitTime % 60).ToString("0"));
    }
    private void isStart()
    {
        isStartBool = true;
    }
    public void Retry()//ゲームのリトライをする関数・データはセーブしない
    {
        SceneManager.LoadScene(0);
        Debug.Log("new");
    }
}
