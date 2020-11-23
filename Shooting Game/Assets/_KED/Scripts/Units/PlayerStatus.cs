using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    [SerializeField] int maxSp = 1000;          public int GetMaxSp() { return maxSp; }
    int currentSp;                              public int GetSP() { return currentSp; }


    Vector3 originPos;

    private void Awake()
    {
        originPos = transform.position;
    }

    void OnEnable()
    {
        Initialized();
    }

    override public void Initialized() 
    {
        base.Initialized();
        transform.position = originPos;
        currentSp = (int)(maxSp * 0.25f);
    }


    public void DecreaseSp(int p_sp)
    {
        currentSp -= p_sp;
        if(currentSp <= 0)
        {
            currentSp = 0;
        }
    }

    public void IncreaseSp(int p_sp)
    {
        currentSp += p_sp;
        if (currentSp >= maxSp)
            currentSp = maxSp;
    }

    protected override void Dead()
    {
        base.DeadEffect();
        GameManager.instance.GameDeadEnd();
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            IncreaseHp(50);
        }
    }
}
