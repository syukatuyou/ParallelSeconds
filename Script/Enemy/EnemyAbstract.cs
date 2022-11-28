using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    minion01,
    minion02,
    miniboss01,
    miniboss02,
    adds01,
    adds02,
    bigboss
}

public abstract class EnemyAbstract : MonoBehaviour
{
    
    private string naming;
    private int hp;
    private EnemyType type;

    public EnemyAbstract(string naming,EnemyType type)
    {
        this.naming = naming;
        this.type = type;

        switch (type)
        {
            case EnemyType.minion01:
                this.hp = 500;
                break;
            case EnemyType.minion02:
                this.hp = 750;
                break;
            case EnemyType.miniboss01:
                this.hp = 2000;
                break;
            case EnemyType.miniboss02:
                this.hp = 3000;
                break;
            case EnemyType.adds01:
                this.hp = 100;
                break;
            case EnemyType.adds02:
                this.hp = 150;
                break;
            case EnemyType.bigboss:
                this.hp = 10000;
                break;
        }

    }
    public string GetName()
    {
        return this.name;
    }
    public EnemyType GetJob()
    {
        return this.type;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
