using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBase : MonoBehaviour
{
    // Start is called before the first frame update
    //ここでは敵にあったら爆発してゲームオーバーの処理をはさむ。
    //本当はParallel_Managerの方で実装したかったけど拡張する必要があるのでここで処理

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Parallel_GameManager.pManager.GameOver();
        }
        
    }
}
