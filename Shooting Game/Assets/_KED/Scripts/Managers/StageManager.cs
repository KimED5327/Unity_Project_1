using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] Animator myAnim = null;

    int currentStage = 0;

    [SerializeField] Text txtCurrentStage = null;

    [SerializeField] GameObject[] stages = null;

    PlayerStatus playerStatus;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
        Initialized();
    }

    void Initialized()
    {
        playerStatus.IncreaseHp(1000);
        playerStatus.gameObject.SetActive(true);
        currentStage = 0;
    }

    public void Retry()
    {
        stages[currentStage].SetActive(false);
        Initialized();
        stages[currentStage].SetActive(true);
        StartCoroutine(ShowStageStart());
    }

    public void NextStage()
    {
        stages[currentStage++].SetActive(false);
        if(currentStage < stages.Length)
        {
            stages[currentStage].SetActive(true);
            StartCoroutine(ShowStageStart());
        }
        else
        {
            SceneManager.LoadScene("EndingScene");
        }
    }

    public IEnumerator ShowStageStart()
    {
        yield return new WaitForSeconds(1.0f);
        txtCurrentStage.text = "Stage " + (currentStage + 1);
        myAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1.0f);


        GameManager.instance.GameStart();
        StartCoroutine(stages[currentStage].GetComponent<Stage>().PatternCo());
    }


}
