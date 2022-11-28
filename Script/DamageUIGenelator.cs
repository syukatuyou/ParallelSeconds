using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageUIGenelator : MonoBehaviour
{
    //このスクリプトは魔法に当たったら敵にダメージが発生するスクリプトなので
    //ダメージを与えたい敵にアタッチする。
    //GameManagerに付けようと思ったが、処理的に重くなるので見送り。
    //[SerializeField]
    //private GameObject DamageObj;
    [SerializeField]
    private GameObject PosObj;
    [SerializeField]
    private Vector3 AdjPos;
    private int a;
    private Enemy enemy;

    /// <summary>
    /// ここから下はボタンに割り振られた攻撃のステータスに関する変数となっている。
    /// </summary>
    //このままだとスクリプトからダメージUIを発生しないので

    public PoolAbstract original;
    ObjectPool pool = new ObjectPool();

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        pool.SetOriginal(original);
    }

    // Update is called once per frame
    void Update()
    {
        //魔法にあたったらダメージ受ける。

    }

    private void OnTriggerStay(Collider other)
    {
        //求める処理：
        //必殺技
        if (other.gameObject.tag == "Magic")
        {
            //出来ればここに
            //ダメージを受ける処理を記述：理由：このスクリプトは敵に記述するものでだから。
            enemy.hp -= SwipeEffect.swipeEffect.GetUltParam().UltAttack;
            
            //Debug.Log("!");
            DamageInc(SwipeEffect.swipeEffect.GetUltParam().UltAttack);
        }
        //左ボタンの技
        if(other.gameObject.tag == "LMagic")
        {
            StartCoroutine(LDamage(SwipeEffect.swipeEffect.GetLeftParam().LeftInterval));
        }
        //中央ボタンの技
        if (other.gameObject.tag == "CMagic")
        {
            StartCoroutine(CDamage(SwipeEffect.swipeEffect.GetCenterParam().CenterInterval));
        }
        //右ボタンの技
        if (other.gameObject.tag == "RMagic")
        {
            StartCoroutine(LDamage(SwipeEffect.swipeEffect.GetRightParam().RightInterval));
        }
    }
    public void DamageInc(float _damage)//ダメージを発生させる関数
    {
        //GameObject _damageObj = Instantiate(DamageObj);
        var _damageObj = pool.Create() as DamageUI3D;

        _damageObj.GetComponent<TextMeshPro>().text = _damage.ToString();
        _damageObj.transform.position = PosObj.transform.position + new Vector3(Random.Range(-10f,10f), Random.Range(2.0f, 4.5f), Random.Range(-5f, 5f));
       
    }
    IEnumerator LDamage(float l_time)//
    {
        var LParam = SwipeEffect.swipeEffect.GetLeftParam();
        enemy.hp -= LParam.LeftAttack;
        DamageInc(LParam.LeftAttack);
        yield return new WaitForSeconds(l_time);

        
    }
    IEnumerator CDamage(float c_time)//
    {
        yield return new WaitForSeconds(c_time);
        var CParam = SwipeEffect.swipeEffect.GetCenterParam();
        enemy.hp -= CParam.CenterAttack;
        DamageInc(CParam.CenterAttack);
        yield return null;
    }
    IEnumerator RDamage(float r_time)//
    {
        yield return new WaitForSeconds(r_time);
        var RParam = SwipeEffect.swipeEffect.GetRightParam();
        enemy.hp -= RParam.RightAttack;
        DamageInc(RParam.RightAttack);
        yield return null;

    }
}
