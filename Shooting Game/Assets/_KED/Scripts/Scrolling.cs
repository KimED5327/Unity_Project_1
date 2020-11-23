using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    [SerializeField] Vector2 offset = Vector2.zero;
    Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        mat.mainTextureOffset += offset * Time.deltaTime;
    }
}
