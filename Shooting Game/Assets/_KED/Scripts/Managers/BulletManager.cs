using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;

    void Awake() => instance = this;



    public void ShowBullet(BulletInfo p_bulletInfo, Vector3 p_dir, Vector3 p_pos, string p_targetTag = "Player", bool p_isAngle = false)
    {
        GameObject t_bullet = ObjectPooling.instance.GetObject(p_bulletInfo.type);
        t_bullet.transform.position = p_pos;

        if (!p_isAngle)
        {
            t_bullet.transform.Rotate(new Vector3(0, 0, 0));
            t_bullet.GetComponent<Bullet>().SetBullet(p_bulletInfo.sprite,
                                p_bulletInfo.scale,
                                p_bulletInfo.damage,
                                p_bulletInfo.speed,
                                p_dir,
                                p_targetTag);
        }
        else
        {
            t_bullet.transform.Rotate(p_dir);
            t_bullet.GetComponent<Bullet>().SetBullet(p_bulletInfo.sprite,
                    p_bulletInfo.scale,
                    p_bulletInfo.damage,
                    p_bulletInfo.speed,
                    p_dir,
                    p_targetTag);
        }
        

        t_bullet.SetActive(true);
    }

}
