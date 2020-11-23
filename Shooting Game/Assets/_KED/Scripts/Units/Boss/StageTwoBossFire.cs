using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTwoBossFire : MonoBehaviour
{
    public BulletInfo bossBullet;


    [Header("Fire Pattern 1")]
    [SerializeField] Transform[] tfFirePos = null;
    [SerializeField] float angle = 15f;
    [SerializeField] float nextFireAngle = 20f;
    [SerializeField] float fireTime = 5.0f;

    [SerializeField] float fireTermTime = 0.3f;
    [SerializeField] int fireCount = 3;

    [Header("Fire Pattern 2")]
    [SerializeField] Transform tfFireCenterPos = null;
    [SerializeField] float fire2Time = 2.5f;
    [SerializeField] float fire2Angle = -30f;
    [SerializeField] float fire2Term = 0.25f;
    float nextAngle = 15f;

    string targetTag = "Player";


    void Start()
    {
        StartCoroutine(FirePattern1());
        StartCoroutine(FirePattern2());
    }
    IEnumerator FirePattern1()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireTime);
            for (int x = 0; x < fireCount; x++)
            {
                float t_curAngle1 = -nextFireAngle + (nextFireAngle * x);
                float t_curAngle2 = nextFireAngle - (nextFireAngle * x);

                for (int i = 0; i < 3; i++)
                {
                    float t_fireAngle1 = (t_curAngle1 + (angle * i)) / 90;
                    float t_fireAngle2 = (t_curAngle2 - (angle * i)) / 90;
                    Vector3 t_dir1 = (Vector3.down + new Vector3(t_fireAngle1, 0, 0)).normalized;
                    Vector3 t_dir2 = (Vector3.down + new Vector3(t_fireAngle2, 0, 0)).normalized;
                    BulletManager.instance.ShowBullet(bossBullet, t_dir1, tfFirePos[0].position, targetTag);
                    BulletManager.instance.ShowBullet(bossBullet, t_dir2, tfFirePos[1].position, targetTag);
                }

                yield return new WaitForSeconds(fireTermTime);
            }
        }
    }

    IEnumerator FirePattern2()
    {
        while (true)
        {
            yield return new WaitForSeconds(fire2Time);

            for(int i = 0; i < 5; i++)
            {
                float t_fireAngle = (fire2Angle + (nextAngle * i)) / 90;
                Vector3 t_dir = (Vector3.down + new Vector3(t_fireAngle, 0, 0)).normalized;
                BulletManager.instance.ShowBullet(bossBullet, t_dir, tfFireCenterPos.position, targetTag);
                yield return new WaitForSeconds(fire2Term);
            }
        }
    }

}
