using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonEvent : MonoBehaviour
{
    public void OnClickButtonStart()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnClickButtonExit()
    {
        Application.Quit();
    }
    public void OnClickButtonCredit()
    {
        SceneManager.LoadScene("EndingScene");
    }

}
