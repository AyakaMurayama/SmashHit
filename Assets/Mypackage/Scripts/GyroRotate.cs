using UnityEngine;
using System.Collections;

/// <summary>
/// ジャイロ機能でプレイヤーが飛んでいく方向を決めるスクリプト
/// </summary>

public class GyroRotate : MonoBehaviour
{

    private Rigidbody2D rb;//プレイヤーのRigidbodyの入れ物

    float xk;//ジャイロえ取得することのできた角度の入れ物

    private void Start()
    {
        Input.gyro.enabled = true;//ジャイロを使う時に必要。アンドロイド用と思われる
        rb = GetComponent<Rigidbody2D>();//プレイヤーのrigidbodyを取得
    }

    private void Update()
    {

    }

    void FixedUpdate()
    //FixedUpdateは呼ばれる回数が一定。動きにばらつきが出ないので、rigid系の移動スクリプトはここの中
    {
        Vector2 pos = transform.position;//プレイヤーの位置を取得
        xk = Input.gyro.attitude.eulerAngles.y;//RotationはQuaternionという型 オイラー角(3軸)にして取得

        if (xk > 180f)//ひねりすぎた時の処理
        {
            xk -= 360f;
        }
        rb.AddForce(new Vector2(xk * 0.1f, 0));//ジャイロの大きさに合わせてプレイヤーに力を加える
        //public static float Clamp (float value, float min, float max);
        transform.position = new Vector2(Mathf.Clamp(pos.x, -2.5f, 2.5f), pos.y);//ある一定の場所以上に移動しないようにする



    }
}

//gyro.attitude→public Quaternion attitude; Y component of the Quaternion. Don't modify this directly unless you know quaternions inside out.
//Rotateはオブジェクトの座標から回転し、eulerAnglesは指定した座標に回転するといった細かい違いがあります。