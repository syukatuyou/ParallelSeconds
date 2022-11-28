using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Effect_Control : MonoBehaviour
{
    public static Effect_Control effectControl;

    //これはエフェクト自体を動かしたい時にアタッチするスクリプト
    //基本的には動かしたいオブジェクトを親オブジェクトの下に入れて、そこにこのスクリプトをアタッチ
    //実際に行うこと。当たり判定のON・OFFをエフェクト側から変更出来るようにする。
    [SerializeField]
    private float spped;
    private BoxCollider Effectcollider;
    //
    [Header("最後の要素はプログラム上実行しないので注意")]
    [SerializeField]
    private float[] _colliderIndex;//コライダーをオンオフする時間を記述。
    private int t;
    [Header("エフェクトが発生した際、当たり判定をオンにしておくか否か：Onでチェック・Offでチェックを外す")]
    [SerializeField]
    private bool effectEnable;
    private float seconds = 0;

    /*
    [Header("魔法のステータス：SwipeEffectを変更する用でもある")]
    [SerializeField, Header("魔法の攻撃力")]
    private float m_attack;
    [SerializeField, Header("魔法の攻撃間隔")]
    private float m_Ainterval;
    [SerializeField, Header("魔法の攻撃エフェクトの寿命")]
    private float m_lifetime;
    [SerializeField, Header("魔法のクールタイム")]
    private float m_cooltime;
    */
    private void Awake()
    {
        ///もし_colliderIndex(エフェクトの当たり判定のオンオフの
        ///切り替えをする時間が入った配列)が設定されていなかったら
        ///そのままでも実行出来るようにした。
        ///因みに2つ入れているのはプログラムの実装上最後の配列が実行出来ないから。
        seconds = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        Effectcollider = GetComponent<BoxCollider>();
        Effectcollider.enabled = effectEnable;
        Array.Sort(_colliderIndex);
        t = 0;
        Debug.Log(_colliderIndex.Length);
        if (_colliderIndex.Length == 0)
        {
            _colliderIndex = new float[2];
            _colliderIndex[0] = 1000;
            _colliderIndex[1] = 1050;
        }

        ///  出来ればここでSwipeEffectの魔法ステータスの書き換えを行いたい。
        ///  SwipeEffectというスクリプトでは魔法のステータスを記述していたが、
        ///  そのままだと魔法を変更する度、ステータスをInspectorで変更する必要がある
        ///  それだと作業がめんどくさくなるので、魔法自体にステータスを付け
        ///  ゲームが始まればその魔法にあったステータスへ自動的に変更できるようにする。
        ///  (SwipeEffectの欄のステータスは初期値として残しておく)

    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        //例外処理を書いていたが、Start()に例外処理を書いたので省略

        if(seconds > _colliderIndex[t] && t < _colliderIndex.Length-1)
        {
            
            Debug.Log("Changed");
            ChangeCollider();
            t += 1;
        }

        transform.Translate(0, 0, spped*Time.deltaTime);
    }
    void ChangeCollider()
    {
        if(Effectcollider.enabled == false)
        {
            Debug.Log("falseTotrue");
            Effectcollider.enabled =true;
        }
        else if (Effectcollider.enabled == true)
        {
            Debug.Log("trueTofalse");
            Effectcollider.enabled = false;
        }

    }
}
