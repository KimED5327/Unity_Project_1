using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject goBullet;
    public Transform tfFirePos;

    [SerializeField] GameObject goLeftPlane = null;
    [SerializeField] GameObject goRightPlane = null;

    BulletInfo bulletInfo;

    [SerializeField] float subWeaponTime = 15f;
    float curSubWeaponTime = 0f;
    bool isActivateSubWeapon = false;

    [SerializeField] float fireRate = 0.3f;
    float curFireRate = 0;
    
    void Start() => bulletInfo = GetComponent<BulletInfo>();
    
    // Update is called once per frame
    void Update()
    {
        Fire();
        CalcSubWeapon();
    }

    void CalcSubWeapon()
    {
        if (isActivateSubWeapon)
        {
            curSubWeaponTime += Time.deltaTime;
            if(curSubWeaponTime >= subWeaponTime)
            {
                ActiveSubWeapon(false);
            }
        }
    }

    void Fire()
    {

        curFireRate += Time.deltaTime;
        if(curFireRate >= fireRate)
        {
            curFireRate = 0f;
            AudioManager.instance.PlaySFX("Bullet1");
            BulletManager.instance.ShowBullet(bulletInfo, Vector3.up, tfFirePos.position, "Enemy");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.instance.PlaySFX("Bullet1");
            BulletManager.instance.ShowBullet(bulletInfo, Vector3.up, tfFirePos.position, "Enemy");
        }
    }

    public void ActiveSubWeapon(bool isActive)
    {
        curSubWeaponTime = 0f;
        isActivateSubWeapon = isActive;
        goLeftPlane.SetActive(isActive);
        goRightPlane.SetActive(isActive);
    }
}
