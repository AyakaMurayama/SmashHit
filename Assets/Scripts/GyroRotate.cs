using UnityEngine;
using System.Collections;

public class GyroRotate : MonoBehaviour
{

    //float speed = 5.0f;
    //private Vector3 Pos = new Vector3(0, 0, 0);

    private Rigidbody2D rb;

    float xk;
    float gyroScale = 2.0f;


    private void Start()
    {
        Input.gyro.enabled = true;
        //rigitbody = GetComponent<Rigidbody>();//3d
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //transform.position += new Vector3(xk * 5f, 0);
        //this.transform.position = new Vector2(Mathf.Clamp(0, 3.0f, -3.0f), transform.position.y);//これはじゃいろといっしょにつかえない？
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("mouseget");
        //    rb.AddForce(new Vector2(0, 00f));
        //}
    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;
        xk = Input.gyro.attitude.y;
        Debug.Log(xk);
        //xk = Mathf.Clamp(Input.gyro.gravity.y * gyroScale, -1.0f, 1.0f);
        rb.AddForce(new Vector2(xk * 5f, 0));
        transform.position = new Vector2(Mathf.Clamp(pos.x, -2.5f, 2.5f), pos.y);//聞いてるけどそっちに行きすぎてる感


    }
}

//ジャイロの調子が悪い
//進捗良くない