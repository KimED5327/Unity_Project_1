using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //보스등장
    //1. 일정시간이 지난후 등장
    //2. 에너미 몇마리 이상을 잡고나면 등장

    //보스 총알발사 (총알패턴)
    //1. 플레이어를 향해서 총알발사
    //2. 회전총알 발사

    public GameObject BulletFactory;        //총알 프리팹
    public GameObject target;               //타겟(플레이어)
    public float fireTime = 1.0f;             //1초에 한번씩 플레이어를 향해서 총알발사
    float curTime = 0.0f;                    //누적시간
    public float fireTime1 = 1.5f;            //1.5초에 한번씩 회전총알 발사
    float curTime1 = 0.0f;
    public int bulletMax = 10;              //회전총알 최대갯수


    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //AutoFire1();
        AutoFire2();
    }

    //플레이어를 향해서 총알발사
    private void AutoFire1()
    {
        //타겟(플레이어)이 없을때 에러 발생할 수 있으니 예외처리하기
        if(target != null)
        {
            curTime += Time.deltaTime;
            if(curTime > fireTime)
            {
                curTime = 0.0f;

                //총알 공장에서 총알생성
                GameObject bullet = Instantiate(BulletFactory);
                //총알 생성 위치
                bullet.transform.position = transform.position;
                //플레이어를 향하는 방향 구하기 (벡터의 뺄셈)
                Vector3 dir = target.transform.position - transform.position;
                dir.Normalize();

                //총구의 방향도 맞춰준다(이게 중요함)
                bullet.transform.up = dir;

            }
        }
    }

    //회전 총알발사
    private void AutoFire2()
    {
        //타겟이 없을때는 총알 발사 중지 시키기
        if(target != null)
        {
            curTime1 += Time.deltaTime;
            if(curTime1 > fireTime1)
            {
                //총알 최대갯수만큼
                for(int i = 0;i < bulletMax; i++)
                {
                    //총알공장에서 총알생성
                    GameObject bullet = Instantiate(BulletFactory);
                    //총알생성 위치
                    bullet.transform.position = transform.position;
                    //360도 방향으로 총알발사
                    float angle = 360.0f / bulletMax;
                    bullet.transform.eulerAngles = new Vector3(0, 0, i * angle);
                }

                //타이머 초기화
                curTime1 = 0.0f;
            }
        }
    }
}
