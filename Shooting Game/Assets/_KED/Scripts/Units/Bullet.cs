using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    string targetTag;
    float speed = 10.0f;
    int damage = 10;
    Vector3 dir;
    SpriteRenderer rend = null;

    void Awake() => rend = GetComponent<SpriteRenderer>();
    void Update() => Move();

    void Move()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    


    public void SetBullet(Sprite p_sprite, Vector3 p_scale, int p_damage, float p_speed, Vector3 p_dir, string p_targetTag)
    {
        targetTag = p_targetTag;
        dir = p_dir;
        speed = p_speed;
        rend.sprite = p_sprite;
        transform.localScale = p_scale;
        damage = p_damage;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) return;     // 총알끼리 충돌 무시
        if (!other.CompareTag(targetTag)) return;   // 공격 대상이 아니면 충돌 무시
       
        ShowEffect();
        DestroyBullet(other.gameObject);
    }

    void ShowEffect()
    {
        GameObject p_effect = ObjectPooling.instance.GetObject(POOLTYPE.BULLET_EFFECT);
        p_effect.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        p_effect.SetActive(true);
    }

    void DestroyBullet(GameObject p_target)
    {
        p_target.GetComponent<Status>().DecreaseHp(damage);
        ObjectPooling.instance.PushPool(gameObject, POOLTYPE.BULLET);
    }
}
