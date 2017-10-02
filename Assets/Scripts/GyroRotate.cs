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
        xk = Input.gyro.attitude.eulerAngles.y;//オイラー角に入力？
                                               //Quaternion.yは
        Debug.Log(xk);
        //xk = Mathf.Clamp(Input.gyro.gravity.y * gyroScale, -1.0f, 1.0f);
        if (xk > 180f)
        {
            xk -= 360f;
        }
        rb.AddForce(new Vector2(xk * 0.2f, 0));
        transform.position = new Vector2(Mathf.Clamp(pos.x, -2.5f, 2.5f), pos.y);


    }
}

//ジャイロの調子が悪い
//進捗良くない
//gyro.attitude→public Quaternion attitude; Y component of the Quaternion. Don't modify this directly unless you know quaternions inside out.
//Rotateはオブジェクトの座標から回転し、eulerAnglesは指定した座標に回転するといった細かい違いがあります。
//Quaternion系要復習