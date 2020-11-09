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

    // 카메라 오프셋
    Vector3 offset;

    [SerializeField] float intervalSpace = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Screen.width / Screen.height;

        Vector3 colHalfSize = GetComponent<Collider>().bounds.extents;
        playerHalfWidth = colHalfSize.x;
        playerHalfHeight = colHalfSize.y;

        offset = Camera.main.transform.position;
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
        movePosition.Set(Mathf.Clamp(movePosition.x, -camWidth + offset.x + spaceWidth, camWidth + offset.x - spaceWidth),
                         Mathf.Clamp(movePosition.y, -camHeight + offset.y + spaceHeight, camHeight + offset.y - spaceHeight),
                         0f);

        transform.position = movePosition;
    }
}
