using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum POOLTYPE
{
    BULLET,
    BULLET_EFFECT,
    ENEMY_BASIC,
    ENEMY_BOSS_1,
    EXPLOSION_EFFECT_1,
    EXPLOSION_EFFECT_2,
    ITEM,
    ENEMY_BOSS_2,
}

public enum VARIATION
{
    BASIC,
    SMALL,
    BOSS,
}

public class ObjectPooling : MonoBehaviour
{

    [System.Serializable]
    public struct Pool
    {
        public string name;
        public int count;
        public GameObject goPrefab;
        public POOLTYPE type;
        public VARIATION variationType;
    }

    [SerializeField] Pool[] objects = null;

    Queue<GameObject>[] qPools;
    Dictionary<POOLTYPE, Queue<GameObject>> poolTable = new Dictionary<POOLTYPE, Queue<GameObject>>();

    public static ObjectPooling instance;
    // Start is called before the first frame update
    void Awake()
    {
        Initialized();
        instance = this;
    }

    void Initialized()
    {
        qPools = new Queue<GameObject>[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            qPools[i] = MakePool(objects[i].goPrefab, objects[i].count);
            poolTable.Add(objects[i].type, qPools[i]);
        }
    }

    Queue<GameObject> MakePool(GameObject p_prefab, int p_count)
    {
        Queue<GameObject> t_Queue = new Queue<GameObject>();
        for(int i = 0; i < p_count; i++)
        {
            GameObject t_clone = Instantiate(p_prefab, Vector3.zero, Quaternion.identity);
            t_clone.SetActive(false);
            t_clone.transform.SetParent(transform);
            t_Queue.Enqueue(t_clone);
        }
        return t_Queue;
    }



    Queue<GameObject> AddPool(POOLTYPE p_type, Queue<GameObject> p_queue)
    {
        for(int i = 0; i < objects.Length; i++)
        {
            if (p_type.Equals(objects[i].type))
            {

                for(int k = 0; k < 5; k++)
                {
                    GameObject t_clone = Instantiate(objects[i].goPrefab, Vector3.zero, Quaternion.identity);
                    t_clone.gameObject.SetActive(false);
                    t_clone.transform.SetParent(transform);
                    p_queue.Enqueue(t_clone);
                }
                return p_queue;
            }
        }
        return null;
    }


    public GameObject GetObject(POOLTYPE p_type)
    {
        if (poolTable[p_type].Count > 0)
            return poolTable[p_type].Dequeue();

        // 풀이 비었다면 새로 생성 후 Dequeue 시도
        else
        {
            AddPool(p_type, poolTable[p_type]);
            return poolTable[p_type].Dequeue();
        }
            
    }

    public void PushPool(GameObject p_object, POOLTYPE p_type)
    {

        p_object.transform.position = Vector3.zero;
        p_object.gameObject.SetActive(false);
        poolTable[p_type].Enqueue(p_object);
    }
}
