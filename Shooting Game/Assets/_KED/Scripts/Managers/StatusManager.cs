using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    PlayerStatus playerStatus;

    [SerializeField] Image imgFilledHp = null;
    [SerializeField] Image imgFilledSp = null;

    [SerializeField] float recoverSpTime = 0f;
    [SerializeField] float recoverHpTime = 0f;
    [SerializeField] int recoverSpNum = 0;
    [SerializeField] int recoverHpNum = 0;

    float curSpTime;
    float curHpTime;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStatus.GetHp() > 0)
        {
            RecoverHp();
            RecoverSp();
        }

    }

    void RecoverHp()
    {
        curHpTime += Time.deltaTime;
        if (curHpTime >= recoverHpTime)
        {
            curHpTime = 0f;
            playerStatus.IncreaseHp(recoverHpNum);
        }

        imgFilledHp.fillAmount = (float)playerStatus.GetHp() / playerStatus.GetMaxHp();
    }
    void RecoverSp()
    {
        curSpTime += Time.deltaTime;
        if (curSpTime >= recoverSpTime)
        {
            curSpTime = 0f;
            playerStatus.IncreaseSp(recoverSpNum);
        }

        imgFilledSp.fillAmount = (float)playerStatus.GetSP() / playerStatus.GetMaxSp();
    }


    public Status GetStatus() { return playerStatus; }
}
