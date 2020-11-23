using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ItemType
{
    HP,
    SP,
    WEAPON_UP,
    SUB_WEAPON_UP,
    SCORE,
}

public class Item : MonoBehaviour
{
    [SerializeField] Material[] mats = null;
    [SerializeField] float spinSpeed = 90f;
    [SerializeField] float moveSpeed = 2f;
    ItemType itemType;

    Renderer rend;

    void Awake() => rend = GetComponent<Renderer>();

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 90, 0) * spinSpeed * Time.deltaTime, Space.World);
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    public void SetItemType(int p_type)
    {
        itemType = (ItemType)p_type;
        rend.material = mats[(int)itemType];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            AudioManager.instance.PlaySFX("Item");
            ObjectPooling.instance.PushPool(gameObject, POOLTYPE.ITEM);

            

            switch (itemType)
            {
                case ItemType.HP: other.GetComponent<PlayerStatus>().IncreaseHp(10); break;
                case ItemType.SP: other.GetComponent<PlayerStatus>().IncreaseSp(10); break;
                case ItemType.SCORE: ScoreManager.instance.AddScore(25);  break;
                case ItemType.WEAPON_UP: other.GetComponent<BulletInfo>().damage += 5; break;
                case ItemType.SUB_WEAPON_UP: other.GetComponent<PlayerFire>().ActiveSubWeapon(true); break;
            }
        }
    }
}
