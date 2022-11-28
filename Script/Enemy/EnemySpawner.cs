using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Set Enemy Prefab")]
    //敵プレハブ
    public GameObject enemyPrefab;
    [Header("Set Interval Min and Max")]
    //時間間隔の最小値
    [Range(1f, 3f)]
    public float minTime = 2f;
    //時間間隔の最大値
    [Range(5f, 10f)]
    public float maxTime = 5f;
    [Header("Set X Position Min and Max")]
    //X座標の最小値
    [Range(-10f, 0f)]
    public float xMinPosition = -10f;
    //X座標の最大値
    [Range(0f, 10f)]
    public float xMaxPosition = 10f;
    [Header("Set Y Position Min and Max")]
    //Y座標の最小値
    [Range(-10f, 10f)]
    public float yMinPosition = 0f;
    //Y座標の最大値
    [Range(0f, 30f)]
    public float yMaxPosition = 10f;
    [Header("Set Z Position Min and Max")]
    //Z座標の最小値
    [Range(10f, 50f)]
    public float zMinPosition = 10f;
    //Z座標の最大値
    [Range(20f, 100f)]
    public float zMaxPosition = 80f;
    //敵生成時間間隔
    private float interval;
    //経過時間
    private float time = 0f;

    

    public GameObject MyBase;
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        //時間間隔を決定する
        interval = GetRandomTime();
        
    }


    // Update is called once per frame
    void Update()
    {
        //時間計測
        time += Time.deltaTime;

        //経過時間が生成時間になったとき(生成時間より大きくなったとき)
        if (time > interval)
        {
            
            //enemyをインスタンス化する(生成する)
            enemy = Instantiate(enemyPrefab) as GameObject;
            //var enemyObj = enemy.GetComponent<DamageUIGenelator>();//正直重くなるから止めた方が良い。
            //enemyObj.original = (PoolAbstract)Resources.Load("Assets/ゲーム_ParallelSeconds/Effect/DamageText.prefab");
            //生成した敵の位置をランダムに設定する
            enemy.transform.position = GetRandomPosition();
            //経過時間を初期化して再度時間計測を始める
            time = 0f;
            //次に発生する時間間隔を決定する
            interval = GetRandomTime();
            enemy.transform.position += Vector3.MoveTowards(transform.position, MyBase.transform.position, 10f*Time.deltaTime);
            enemy.transform.LookAt(MyBase.transform.position);
            Destroy(enemy, 60f);
            
            //enemyPrefab.transform.LookAt(MyBase.transform.position);
            //enemyPrefab.transform.position += -transform.forward * 3.5f * Time.deltaTime;
        }
    }

    //ランダムな時間を生成する関数
    private float GetRandomTime()
    {
        return Random.Range(minTime, maxTime);
    }

    //ランダムな位置を生成する関数
    private Vector3 GetRandomPosition()
    {
        //それぞれの座標をランダムに生成する
        float x = Random.Range(xMinPosition, xMaxPosition);
        float y = Random.Range(yMinPosition, yMaxPosition);
        float z = Random.Range(zMinPosition, zMaxPosition);

        //Vector3型のPositionを返す
        return new Vector3(x, y, z);
    }
}
