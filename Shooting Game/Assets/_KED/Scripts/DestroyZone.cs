using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    public bool isTop = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isTop)
        {
            if (other.transform.CompareTag("Bullet"))
            {
                ObjectPooling.instance.PushPool(other.gameObject, POOLTYPE.BULLET);
            }
        }
        else
        {
            ObjectPooling.instance.PushPool(other.gameObject, other.GetComponent<Status>().GetObjectType());
        }
    }


}
