using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    static Enemy _enemy;
    //生成するための敵オブジェクト
    float aliveCount;
    public float hp = 10000;
    public GameObject objective;
    [SerializeField]
    private float speed;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Parallel_GameManager.pManager.isStartBool)
        {
            Vector3 dir = (objective.transform.position - this.transform.position).normalized;
            // その方向へ指定した量で進む
            //float vx = dir.x * speed;
            float vz = dir.z * speed;
            this.transform.Translate(0, 0, vz / 50);
        }
        
        if (hp < 0)
        {
            //これは敵なのでプールしない。
            Destroy(this.gameObject);
        }
    }
}
