using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Stage : MonoBehaviour
{
    [SerializeField] GameObject goWarning = null;

    [System.Serializable]
    public struct Pattern
    {
        [Header("Enemy Type")]
        public POOLTYPE type;
        public VARIATION varType;

        [Header("Coordinate")]
        [Range(-2.5f, 2.5f)]
        public float offsetX;
        public float offsetY;

        [Header("Pattern")]
        public int count;
        public Vector2[] offsetDistance;
        
        [Header("Next Pattern")][Tooltip("less then 0 is Finish")]
        public float nextPatternWaitTime;
    }

    [SerializeField] float defaultPosY = 6.5f;
    [Space(5)]
    public Pattern[] patterns;
    int patternCount = 0;


    // Player Transform
    Transform tfPlayer = null;

    void Awake() => tfPlayer =GameObject.FindGameObjectWithTag("Player").transform;


    public void StopStage() => StopAllCoroutines();
    public IEnumerator PatternCo()
    {
        yield return new WaitForSeconds(2f);

        while(patternCount <= patterns.Length)
        {
            // 게임이 끝났으면 그대로 패턴 종료
            if (!GameManager.instance.IsPlay()) break;

            // 패턴 하나 시작
            Pattern t_curPattern = patterns[patternCount];
            for(int i = 0; i < t_curPattern.count; i++)
            {
                // 에너미 풀링 Get -> ReFactory
                GameObject t_enemy = ObjectPooling.instance.GetObject(t_curPattern.type);
                t_enemy = ReFactoringEnemy(t_enemy, EnemyManager.instance.SearchEnemyType(t_curPattern.type, t_curPattern.varType));

                // 보스 출현 시 경고문구 출력
                if(t_curPattern.varType == VARIATION.BOSS)
                {
                    for(int x = 0; x < 8; x++)
                    {
                        goWarning.SetActive(!goWarning.activeSelf);
                        yield return new WaitForSeconds(0.5f);
                    }
                    goWarning.SetActive(false);
                }

                // 위치 셋팅 후 활성화
                Vector2 t_offset = new Vector3(t_curPattern.offsetX, t_curPattern.offsetY + defaultPosY);
                if(t_curPattern.offsetDistance.Length == 1)
                    t_offset += t_curPattern.offsetDistance[0] * i;
                else
                    t_offset += t_curPattern.offsetDistance[i % t_curPattern.offsetDistance.Length];
                t_enemy.transform.position = t_offset;
                t_enemy.SetActive(true);
            }
            
            // 다음 패턴 준비 (없으면 종료)
            if (t_curPattern.nextPatternWaitTime >= 0)
                yield return new WaitForSeconds(t_curPattern.nextPatternWaitTime);
            else
            {
                yield return new WaitForSeconds(5f);
                break;
            }

            patternCount++;
        }
    }


    GameObject ReFactoringEnemy(GameObject t_enemy, EnemyType t_type)
    {

        if(t_type.varType != VARIATION.BOSS)
        {
            t_enemy.GetComponent<Status>().SetHp(t_type.hp);
            t_enemy.GetComponent<EnemyMove>().SetSpeed(t_type.speed);
            t_enemy.GetComponent<BulletInfo>().damage = t_type.damage;
            t_enemy.GetComponent<BulletInfo>().speed = t_type.bulletSpeed;

            AutoFire t_autoFire = t_enemy.GetComponent<AutoFire>();
            t_autoFire.SetFireRate(t_type.fireRateSpeed);
            t_autoFire.SetFireType(t_type.fireType);
            if (t_type.fireType == FIRETYPE.TARGETING || t_type.fireType == FIRETYPE.CHASE_TARGET_CURVE_SLOW || t_type.fireType == FIRETYPE.CHASE_TARGET)
                t_autoFire.SetTarget(tfPlayer);
            else
                t_autoFire.SetTarget(null);

            t_enemy.transform.localScale = t_type.scale;
        }
        return t_enemy;
    }
}
