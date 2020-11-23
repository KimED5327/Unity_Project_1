using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHpBar : MonoBehaviour
{
    Status myStatus;
    [SerializeField] Image img_HpFilled = null;
    [SerializeField] Text txt_Hp = null;
    // Start is called before the first frame update
    void Start()
    {
        myStatus = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        img_HpFilled.fillAmount = (float)myStatus.GetHp() / myStatus.GetMaxHp();
        int t_curHp = myStatus.GetHp();
        txt_Hp.text = t_curHp > 0 ? t_curHp.ToString() : "0";
    }
}
