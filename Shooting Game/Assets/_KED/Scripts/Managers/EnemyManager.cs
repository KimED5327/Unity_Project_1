
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyType
{
    public string name;
    public POOLTYPE type;
    public VARIATION varType;
    public FIRETYPE fireType;
    public int damage;
    public int hp;
    public float speed;
    public float bulletSpeed;
    public float fireRateSpeed;
    public Vector3 scale;
}

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    void Awake() => instance = this;

    public EnemyType[] enemys;

    public EnemyType SearchEnemyType(POOLTYPE p_type, VARIATION p_varType)
    {
        for(int i = 0 ; i < enemys.Length; i++)
        {
            if(p_type == enemys[i].type && p_varType == enemys[i].varType)
            {
                return enemys[i];
            }
        }

        EnemyType t_enemy = new EnemyType();
        return t_enemy;
    }


}
