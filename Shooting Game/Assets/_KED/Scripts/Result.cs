using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    [SerializeField] Text txt_Context = null;
    [SerializeField] Text txt_FinalScore = null;
    [SerializeField] Text txt_NextOrRetry = null;

    [SerializeField] Color clr_Clear = new Color();
    [SerializeField] Color clr_Fail = new Color();
    [SerializeField] Text txt_ClearOrFail = null;


    bool isClear = false;

    Animator myAnim;
    StatusManager theStatus;
    StageManager theStage;

    void Awake()
    {
        myAnim = GetComponent<Animator>();
        theStatus = FindObjectOfType<StatusManager>();
        theStage = FindObjectOfType<StageManager>();
    }
  
    public void ShowResult(bool p_isClear)
    {
        isClear = p_isClear;
        txt_NextOrRetry.text = isClear ? "NEXT" : "RETRY";
        txt_ClearOrFail.text = isClear ? "Clear!!" : "Fail!!";
        txt_ClearOrFail.color = isClear ? clr_Clear : clr_Fail;

        // 현재점수
        int t_curScore = ScoreManager.instance.GetScore();
        txt_Context.text = string.Format("{0:#,##0}" , t_curScore);

        // 피격수
        int t_hitCount = ScoreManager.instance.GetHitCount();
        txt_Context.text += "\n" + t_hitCount;

        // 격추수
        int t_destroyCount = ScoreManager.instance.GetDestoryCount();
        txt_Context.text += "\n" + t_destroyCount;


        // 체력 잔여량
        Status t_playerStatus = theStatus.GetStatus();
        int t_curHp = t_playerStatus.GetHp();
        int t_maxHp = t_playerStatus.GetMaxHp();
        float t_hpRatio = (float)t_curHp / t_maxHp;
        txt_Context.text += "\n" + string.Format("{0:#,##0.00}" ,t_hpRatio * 100) + "%";

        // 최종점수
        int t_finalScore = t_curScore;
        t_finalScore -= t_hitCount * 2;
        t_finalScore += t_destroyCount * 5;
        t_finalScore = (int)(t_finalScore * (1.0f + t_hpRatio));
        txt_FinalScore.text = string.Format("{0:#,##0}", t_finalScore);

        ScoreManager.instance.SaveHighScore(t_finalScore);

        myAnim.SetTrigger("ShowResult");
    }

    public void BtnNextOrRetry()
    {
        myAnim.SetTrigger("HideResult");


        if (isClear)
            theStage.NextStage();
        else
            theStage.Retry();


    }

    public void BtnBack()
    {
        myAnim.SetTrigger("HideResult");
        SceneManager.LoadScene(0);
    }
}
