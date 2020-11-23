using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	public float speed = 5.0f;

    float camWidth;
    float camHeight;
    float playerHalfWidth;
    float playerHalfHeight;

    float playerOffsetY;

    // 카메라 오프셋
    Vector3 offset;
    [SerializeField] Vector2 margin = new Vector2(0.12f, 0.12f);

    [SerializeField] float intervalSpace = 0.25f;
    [SerializeField] LayerMask layerMask = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerOffsetY = transform.position.y;

        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Screen.width / Screen.height;

        Vector3 colHalfSize = GetComponent<Collider>().bounds.extents;
        playerHalfWidth = colHalfSize.x;
        playerHalfHeight = colHalfSize.z;

        offset = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveControl();
        MoveAtMousePos();
    }

    void MoveControl()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, v, 0f);
        dir = dir.normalized;
        transform.position += dir * speed * Time.deltaTime;

        //ClampInScreen();
        ViewportInScreen();
    }

    void ClampInScreen()
    {
        Vector3 myPos = transform.position;
        float spaceWidth = playerHalfWidth + intervalSpace;
        float spaceHeight = playerHalfHeight + intervalSpace;
        float minWidth = -camWidth + offset.x + spaceWidth;
        float maxWidth = camWidth + offset.x - spaceWidth;
        float minHeight = -camHeight + offset.y + spaceHeight;
        float maxHeight = camHeight + offset.y - spaceHeight;
        myPos.Set(Mathf.Clamp(myPos.x, minWidth, maxWidth),
                  Mathf.Clamp(myPos.y, minHeight, maxHeight),
                  0f);

        transform.position = myPos;
    }

    void ViewportInScreen()
    {
        Vector3 myPos = Camera.main.WorldToViewportPoint(transform.position);
        myPos.x = Mathf.Clamp(myPos.x, 0f + margin.x, 1f - margin.x);
        myPos.y = Mathf.Clamp(myPos.y, 0f + margin.y, 1f - margin.y);
        transform.position = Camera.main.ViewportToWorldPoint(myPos);
    }
    
    void MoveAtMousePos()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;

            Collider[] col = Physics.OverlapSphere(mouseWorldPos, 1, layerMask);


            if (col.Length > 0)
            {
                mouseWorldPos.y = playerOffsetY;
                transform.position = mouseWorldPos;

                ViewportInScreen();
            }
        }
    }
}
