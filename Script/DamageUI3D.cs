using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageUI3D : PoolAbstract
{
    //ダメージのUIの動きのみを実装
    [SerializeField]
    private float DeleteTime = 1.0f;
    [SerializeField]
    private float MoveRange = 1.0f;
    [SerializeField]
    private float EndAlpha = 0;

    private float TimeC;
    private TextMeshPro NowDamage;

    private Camera lookcamera;

    //pool用
    public override void Init()
    {
        TimeC = DeleteTime;
        NowDamage = this.gameObject.GetComponent<TextMeshPro>();
        gameObject.SetActive(true);
        lookcamera = Camera.main;
        
    }
    public override void Sleep()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var direction = lookcamera.transform.position - transform.position;
        direction.x = 0;//x軸を固定
        var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        //transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 1f);
        //transform.LookAt(Camera.main.transform);
        TimeC += Time.deltaTime;
        gameObject.transform.localPosition += new Vector3(0, MoveRange / DeleteTime * Time.deltaTime, 0);
        //float _alpha = 1.0f - (1.0f - EndAlpha) * (TimeC / DeleteTime);
        //if (_alpha <= 0.0f) _alpha = 0.0f;
        //NowDamage.color = new Color(NowDamage.color.r, NowDamage.color.g, NowDamage.color.b, _alpha);
        if(TimeC > 1.5)
        {
            Pool.Release(this);
        }
        
    }
}
