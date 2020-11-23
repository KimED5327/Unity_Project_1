using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] float destroyTime = 4.0f;

    // Start is called before the first frame update
    void Start() => Invoke(nameof(DestoryThisObject), destroyTime);

    void DestoryThisObject()
    {
        Destroy(gameObject);
    }
}
