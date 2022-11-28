using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolAbstract : MonoBehaviour
{
    public ObjectPool Pool { get; set; }
    public abstract void Init();
    public abstract void Sleep();
}
