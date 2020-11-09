using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
	public float speed = 5.0f;

    float camWidth;
    float camHeight;
    float playerHalfWidth;
    float playerHalfHeight;

    [SerializeField] float intervalSpace = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Screen.width / Screen.height;

        Vector3 colHalfSize = GetComponent<Collider>().bounds.extents;
        playerHalfWidth = colHalfSize.x;
        playerHalfHeight = colHalfSize.y;
    }

    // Update is called once per frame
    void Update()
    {
        MoveControl();
    }


    void MoveControl()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.up * v;
        dir = dir.normalized;

        Vector3 movePosition = transform.position + dir * speed * Time.deltaTime;
        float spaceWidth = playerHalfWidth + intervalSpace;
        float spaceHeight = playerHalfHeight + intervalSpace;
        movePosition.Set(Mathf.Clamp(movePosition.x, -camWidth + spaceWidth, camWidth - spaceWidth),
                         Mathf.Clamp(movePosition.y, -camHeight + spaceHeight, camHeight - spaceHeight),
                         0f);

        transform.position = movePosition;
    }
}
