using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class EnemyMove : MonoBehaviour
{

    [SerializeField] float speed = 5.0f; //에너미 이동속도 
    public void SetSpeed(float p_speed) { speed = p_speed; }

    [SerializeField] bool isStop = false;
    [SerializeField] float stopPosY = 3.0f;

    // Update is called once per frame
    void Update()
    {
        if (isStop)
        {
            if (transform.position.y <= stopPosY)
            {
                return;
            }
        }
        transform.Translate(Vector3.down * speed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject t_effect = ObjectPooling.instance.GetObject(POOLTYPE.EXPLOSION_EFFECT_1);
            t_effect.transform.position = transform.position;
            t_effect.SetActive(true);
            t_effect = ObjectPooling.instance.GetObject(POOLTYPE.BULLET_EFFECT);
            t_effect.transform.position = other.transform.position;
            t_effect.SetActive(true);

            gameObject.SetActive(false);
            other.GetComponent<Status>().DecreaseHp(10);
        }
    }
}
