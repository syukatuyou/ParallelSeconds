using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Playables;
using UnityEditor;

public class SwipeEffect : MonoBehaviour
{
    public static SwipeEffect swipeEffect;


    [Header("狙う用のエフェクト")]
    [SerializeField]
    private GameObject aimEffect;
    [Header("攻撃発生エフェクト")]
    [SerializeField]
    private GameObject emergeEffect;
    [Header("0が左・1が中央・2が右のボタンにアタッチ")]
    [SerializeField]
    private GameObject[] attackEffect;//3つのボタンに割り振るよう。
    

    private Camera mainCamera;
    private Vector3 currentPosition = Vector3.zero;
    private Vector3 oldPos = Vector3.zero;

    private GameObject spawnEffect;//キャストするための変数
    private GameObject spawnUlt;//必殺技のキャストの変数。
    //private int AttackID=0;//攻撃のクールダウン用：これに対応する

    //エイムエフェクト用
    private Vector3 position;
    private GameObject Aim;

    //エディタを折りたたむ為の変数
    


    [SerializeField, Header("攻撃用のボタン")]
    private Button[] atkBtn;
    [SerializeField, Header("必殺技")]
    private Button[] UltBtn;
    [Header("必殺技のエフェクト")]
    [SerializeField]
    private GameObject[] ultimateEffect;//必殺技強化するために使用。
    private Animator anim;
    public GameObject panel;//必殺技のカットインに使用。
    private int ult_count;//これはanimatorを起動し、実行したら必殺技を2回発動するようになったので1回目はアニメーションを発動しないようにする。


    [Header("左ボタンのステータス")]
    [SerializeField, Header("左ボタンの攻撃力")]
    private float l_attack;
    [SerializeField, Header("左ボタンの攻撃間隔")]
    private float l_Ainterval;
    [SerializeField, Header("左ボタンの攻撃エフェクトの寿命")]
    private float l_lifetime;
    [SerializeField, Header("左ボタンのクールタイム")]
    private float l_cooltime;

    public (float LeftAttack,float LeftInterval,float LeftLifetime,float LeftCooltime) GetLeftParam()
    {
        return (l_attack,l_Ainterval,l_lifetime,l_cooltime);
    }

    [Header("中央ボタンのステータス")]
    [SerializeField, Header("中央ボタンの攻撃力")]
    private float c_attack;
    [SerializeField, Header("中央ボタンの攻撃間隔")]
    private float c_Ainterval;
    [SerializeField, Header("中央ボタンの攻撃エフェクトの寿命")]
    private float c_lifetime;
    [SerializeField, Header("中央ボタンのクールタイム")]
    private float c_cooltime;

    public (float CenterAttack, float CenterInterval, float CenterLifetime, float CenterCooltime) GetCenterParam()
    {
        return (c_attack, c_Ainterval, c_lifetime, c_cooltime);
    }

    [Header("右ボタンのステータス")]
    [SerializeField, Header("右ボタンの攻撃力")]
    private float r_attack;
    [SerializeField, Header("右ボタンの攻撃間隔")]
    private float r_Ainterval;
    [SerializeField, Header("右ボタンの攻撃エフェクトの寿命")]
    private float r_lifetime;
    [SerializeField, Header("右ボタンのクールタイム")]
    private float r_cooltime;

    public (float RightAttack, float RightInterval, float RightLifetime, float RightCooltime) GetRightParam()
    {
        return (r_attack, r_Ainterval, r_lifetime, r_cooltime);
    }

    [Header("必殺技のステータス")]
    [SerializeField, Header("必殺技の攻撃力")]
    private float ult_attack;
    [SerializeField, Header("必殺技の攻撃間隔")]
    private float ult_Ainterval;
    [SerializeField, Header("必殺技の攻撃エフェクトの寿命")]
    private float ult_lifetime;
    [SerializeField, Header("必殺技のクールタイム")]
    private float ult_cooltime;

    public (float UltAttack, float UltInterval, float UltLifetime, float UltCooltime) GetUltParam()
    {
        return (ult_attack, ult_Ainterval, ult_lifetime, ult_cooltime);
    }

    private void Awake()
    {
        anim = panel.GetComponent<Animator>();
        anim.enabled = false;
        if(swipeEffect == null)
        {
            swipeEffect = this;
        }
    }

    void Start()
    {
        
        mainCamera = Camera.main;
        Aim = Instantiate(aimEffect, new Vector3(0, 2, 0), Quaternion.identity) as GameObject;
        ult_count = 0;//必殺技を発生させた回数。
        foreach (var btn in atkBtn)
        {
            btn.GetComponent<Button>();
        }
        foreach (var ult in UltBtn)
        {
            ult.GetComponent<Button>();
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        var raycastHitList = Physics.RaycastAll(ray).ToList();

        if (Input.GetMouseButtonDown(0))
        {
            if (raycastHitList.Any())
            {
                var distance = Vector3.Distance(mainCamera.transform.position, raycastHitList.First().point);
                var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);

                currentPosition = mainCamera.ScreenToWorldPoint(mousePosition);
                position = currentPosition;
                currentPosition.y = 0;
                //spawnEffect = Instantiate(emergeEffect, new Vector3(currentPosition.x, currentPosition.y + 2f, currentPosition.z), Quaternion.identity)as GameObject;
                //Destroy(spawnEffect, 10f);
                if(emergeEffect != null)
                {
                    //Debug.Log("aaa");
                }
                //移動
                Aim.transform.DOMove(new Vector3(currentPosition.x, currentPosition.y + 2f, currentPosition.z), 0.2f).SetEase(Ease.OutBack);
            }
            

        }
        Vector3 camPos = Input.mousePosition;
        camPos.y = 3f;
    }
    public void GetParamerter(int num)
    {
        switch(num){
            //case 0:

        }
    }
    public void AttackButton(string AttackNum)
    {
        switch (AttackNum)//ここにボタンに関連付けたエフェクトを入れる。
        {
            case "button_left":
                emergeEffect = attackEffect[0];

                spawnEffect = Instantiate(emergeEffect, new Vector3(currentPosition.x, currentPosition.y + 2f, currentPosition.z), Quaternion.identity) as GameObject;
                spawnEffect.tag = "LMagic";
               // Debug.Log(spawnEffect);

                Destroy(spawnEffect, l_lifetime);
                StartCoroutine(buttonPush(0,l_cooltime));
                break;
            case "button_center":
                emergeEffect = attackEffect[1];
                spawnEffect = Instantiate(emergeEffect, new Vector3(currentPosition.x, currentPosition.y + 2f, currentPosition.z), Quaternion.identity) as GameObject;
                spawnEffect.tag = "CMagic";
                //Debug.Log(spawnEffect);

                Destroy(spawnEffect, c_lifetime);
                StartCoroutine(buttonPush(1, c_cooltime));
                break;
            case "button_right":
                emergeEffect = attackEffect[2];
                spawnEffect = Instantiate(emergeEffect, new Vector3(currentPosition.x, currentPosition.y + 2f, currentPosition.z), Quaternion.identity) as GameObject;
                spawnEffect.tag = "RMagic";
                //Debug.Log(spawnEffect);

                Destroy(spawnEffect, r_lifetime);
                StartCoroutine(buttonPush(2, r_cooltime));
                break;
            default:
                break;
        }
    }
    public void UltimateButton(int Num)
    {
        switch (Num)
        {
            case 0://ここに必殺技を記述。
                if (panel != null)
                {
                    anim.enabled = true;
                    if (ult_count != 0)
                    {
                        anim.SetTrigger("cutInAnim");//ここでカットインのアニメーションを発動。理由：こうしないと必殺技を実行した時に重複する。
                    }
                    spawnUlt = Instantiate(ultimateEffect[0], new Vector3(0, 2, 0), Quaternion.identity) as GameObject;
                    ult_count += 1;//ここに必殺技を実行した数を加算する。
                    Destroy(spawnUlt, ult_lifetime);;
                    StartCoroutine(UltPush(0, ult_cooltime));

                }
                break;
            //switchで書いたのは時間が経ったら必殺技が強化される仕様にしたいから。
            default:
                break;
        }

    }
    IEnumerator buttonPush(int num,float limitTime)
    {
        atkBtn[num].GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(limitTime);
        atkBtn[num].GetComponent<Button>().interactable = true;
    }
    IEnumerator UltPush(int num, float limitTime)
    {
        UltBtn[num].GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(limitTime);
        UltBtn[num].GetComponent<Button>().interactable = true;
    }
    IEnumerator CutIn(float seconds)
    {
        anim.SetTrigger("cutInAnim");
        yield return new WaitForSeconds(seconds);        
    }


    
    public void StartCutin()
    {
        print("Play");
    }
}
