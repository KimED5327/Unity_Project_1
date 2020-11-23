using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    Result theResult;
    StageManager theStage;

    public static GameManager instance;
    void Awake()
    {
        instance = this;
        theResult = FindObjectOfType<Result>();
        theStage = FindObjectOfType<StageManager>();
        StartCoroutine(theStage.ShowStageStart());
    }


    bool isPlaying = false; public bool IsPlay() { return isPlaying; }

    public void GameStart()
    {
        isPlaying = true;
    }

    public void GameDeadEnd()
    {
        StartCoroutine(ShowResultCo(false));
    }

    public void GameClear()
    {
        StartCoroutine(ShowResultCo(true));
    }

    IEnumerator ShowResultCo(bool p_isClear)
    {
        isPlaying = false;
        yield return new WaitForSeconds(2.5f);

        theResult.ShowResult(p_isClear);
    }
}
