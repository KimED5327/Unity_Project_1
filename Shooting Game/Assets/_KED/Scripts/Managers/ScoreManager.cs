using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    void Awake() => instance = this;

    [SerializeField] Text txtScore = null;
    [SerializeField] Text txtHighScore = null;

    int score = 0;
    int hitCount = 0;
    int destoryCount = 0;

    int highScore = 0;

    float elapseTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Initialized();
    }

    public void Initialized()
    {
        hitCount = 0;
        score = 0;
        destoryCount = 0;
        elapseTime = 0f;

        highScore = PlayerPrefs.GetInt("HighScore");
        txtHighScore.text = string.Format("{0:#,##0}", highScore);
    }

    // Update is called once per frame
    void Update()
    {
        ProgressAddScore();
    }


    void ProgressAddScore()
    {
        if (GameManager.instance.IsPlay())
        {
            elapseTime += Time.deltaTime;
            if(elapseTime >= 1f)
            {
                elapseTime = 0;
                score += 1;
                txtScore.text = string.Format("{0:#,##0}", score);
            }

        }
    }

    public void SaveHighScore(int p_finalScore)
    {
        if (p_finalScore <= highScore)
        {
            highScore = p_finalScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            txtHighScore.text = string.Format("{0:#,##0}", highScore);
        }
    }


    public void AddScore(int p_score)
    {
        score += p_score;
        txtScore.text = string.Format("{0:#,##0}", score);
    }


    public void IncreaseDestroyCount() { destoryCount++; }
    public void IncreaseHitCount() { hitCount++; }

    public int GetScore() { return score; }
    public int GetDestoryCount() { return destoryCount; }
    public int GetHitCount() { return hitCount; }

}
