using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] POOLTYPE explosionEffect = 0;
    [SerializeField]protected POOLTYPE type = 0;
    [SerializeField]int score = 50;
    [SerializeField]protected int maxHp = 100;      public int GetMaxHp() { return maxHp; }
    protected int currentHp;                        public int GetHp() { return currentHp; }

    // Start is called before the first frame update
    void OnEnable()
    {
        Initialized();
    }


    virtual public void Initialized()
    {
        currentHp = maxHp;
    }

    public void SetHp(int p_hp)
    {
        currentHp = maxHp = p_hp;
    }

    public void DecreaseHp(int p_damage)
    {
        currentHp -= p_damage;
        if(currentHp <= 0)
        {
            Dead();
        }
    }
    public void IncreaseHp(int p_hp)
    {
        currentHp += p_hp;
        if (currentHp >= maxHp)
            currentHp = maxHp;
    }

    virtual protected void Dead()
    {
        if(maxHp >= 1000)
        {
            AudioManager.instance.PlaySFX("Destroy2");
            GameManager.instance.GameClear();
        }
        else
        {
            AudioManager.instance.PlaySFX("Destroy1");
            ItemDrop();
        }

        DeadEffect();
        ScoreManager.instance.AddScore(score);
        ObjectPooling.instance.PushPool(gameObject, type);
    }

    void ItemDrop()
    {
        int t_random = Random.Range(0, 10);
        if(t_random >= 8)
        {
            int t_itemType = Random.Range(0, 5);
            GameObject t_item = ObjectPooling.instance.GetObject(POOLTYPE.ITEM);
            t_item.transform.position = transform.position;
            t_item.transform.rotation = Quaternion.Euler(new Vector3(-115, 0, 0));
            t_item.GetComponent<Item>().SetItemType(t_itemType);
            t_item.SetActive(true);
        }
    }

    protected void DeadEffect()
    {
        GameObject p_effect = ObjectPooling.instance.GetObject(explosionEffect);
        p_effect.transform.position = transform.position;
        p_effect.SetActive(true);
    }


    public POOLTYPE GetObjectType()
    {
        return type;
    }
}
