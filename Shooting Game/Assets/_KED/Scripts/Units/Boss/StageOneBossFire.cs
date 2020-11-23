using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageOneBossFire : MonoBehaviour
{
    public BulletInfo bossBullet1;
    public BulletInfo bossBullet2;

    
    [Header("Fire Pattern 1")]
    [SerializeField] Transform[] tfFirePos = null;
    [SerializeField] float angle = 15f;

    [SerializeField] float fireTime = 5.0f;
    float curFireTime;

    [SerializeField] float fireTermTime = 0.3f;
    [SerializeField] int fireCount = 5;



    [Header("Fire Pattern 2")]
    [SerializeField] Transform tfFireCenterPos = null;
    [SerializeField] float fire2Time = 1.5f;
    float curFire2Time = 0;
    int curfireCount = 0;

    bool isFire = false;
    string targetTag = "Player";

    Transform tfTarget;

    void Awake() => tfTarget = GameObject.FindGameObjectWithTag("Player").transform;

    void Update()
    {

        FirePattern1();
        FirePattern2();

    }

    void FirePattern1()
    {
        if (!isFire)
        {
            curFireTime += Time.deltaTime;
            if (curFireTime > fireTime)
            {
                isFire = true;
                curFireTime = 0;
                StartCoroutine(FireSkill1Co());
            }
        }
    }

    void FirePattern2()
    {
        curFire2Time += Time.deltaTime;
        if(curFire2Time >= fire2Time)
        {
            curFire2Time = 0;
            Vector3 t_dir = (tfTarget.position - transform.position).normalized;
            BulletManager.instance.ShowBullet(bossBullet2, t_dir, tfFireCenterPos.position, targetTag);
        }
    }


    IEnumerator FireSkill1Co()
    {
        for(curfireCount = 0; curfireCount < fireCount; curfireCount++)
        {
            if(curfireCount < fireCount - 1)
            {
                BulletManager.instance.ShowBullet(bossBullet1, Vector3.down, tfFirePos[0].position, targetTag);
                BulletManager.instance.ShowBullet(bossBullet1, Vector3.down, tfFirePos[1].position, targetTag);
            }
            else
            {
                for(int i = 0; i < 3; i++)
                {
                    float t_fireAngle = (-angle + (angle * i)) / 90;
                    Vector3 t_dir = (Vector3.down + new Vector3(t_fireAngle,0,0)).normalized;
                    BulletManager.instance.ShowBullet(bossBullet1, t_dir, tfFirePos[0].position, targetTag);
                    BulletManager.instance.ShowBullet(bossBullet1, t_dir, tfFirePos[1].position, targetTag);
                }
            }
            

            yield return new WaitForSeconds(fireTermTime);
        }

        isFire = false;
    }
}
