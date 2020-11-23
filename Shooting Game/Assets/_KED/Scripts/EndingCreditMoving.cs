using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCreditMoving : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;


    void Start() => Invoke(nameof(End), 50.0f);

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
        if (Input.GetMouseButton(0))
        {
            End();
        }
    }


    void End()
    {
        SceneManager.LoadScene("StartScene");
    }
}
