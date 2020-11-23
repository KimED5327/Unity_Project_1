using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FIRETYPE
{
    STRAIGHT,
    TARGETING,
    CHASE_TARGET_CURVE_SLOW,
    CHASE_TARGET,
    ARC3,
    ARC3_TARGETING,
    CIRCLE_4,
}

public class AutoFire : MonoBehaviour
{
    [SerializeField] Transform tfPos = null;
    [SerializeField] string targetTag = "";
    [SerializeField] float fireRate = 2.0f;    
    float currentFireTime = 0f;

    Transform tfTarget = null;                         
    
    BulletInfo bulletInfo;
    Vector3 dir = Vector3.down;
    [SerializeField] FIRETYPE fireType = FIRETYPE.STRAIGHT;

    void Start() => bulletInfo = GetComponent<BulletInfo>();

    // Update is called once per frame
    void Update()
    {
        currentFireTime += Time.deltaTime;
        if(currentFireTime >= fireRate)
        {
            currentFireTime = 0;

            // 플레이어를 지나치면 발사 안 함.
            if(tfTarget != null && transform.position.y <= tfTarget.position.y) return;


            // 정면
            if (fireType == FIRETYPE.STRAIGHT)
                dir = (targetTag == "Player") ? Vector3.down : Vector3.up;

            // 타겟팅
            else if (fireType == FIRETYPE.TARGETING || fireType == FIRETYPE.ARC3_TARGETING)
                dir = (tfTarget.position - transform.position).normalized;


            // 정해진 방향으로 발사
            if (fireType != FIRETYPE.CHASE_TARGET_CURVE_SLOW || fireType != FIRETYPE.CHASE_TARGET)
                BulletManager.instance.ShowBullet(bulletInfo, dir, tfPos.position, targetTag);

            // 유도 발사
        }
    }



    public void SetFireType(FIRETYPE p_fireType) => fireType = p_fireType;
    public void SetFireRate(float p_fireRate) => fireRate = p_fireRate; 
    public void SetTarget(Transform p_tfTarget) => tfTarget = p_tfTarget;
}
