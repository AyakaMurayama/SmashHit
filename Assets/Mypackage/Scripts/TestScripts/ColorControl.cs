using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// インクオブジェクトに色をつけるためのスクリプト
/// おそらく未使用
/// </summary>

public class ColorControl : MonoBehaviour
{
    SpriteRenderer inkSprite;
    SpriteRenderer playerSprite;
    GameObject ink;
    Renderer inkcolor;
    Renderer mycolor;

    // Use this for initialization
    private void Awake()
    {
        inkSprite = GameObject.FindWithTag("ink").GetComponent<SpriteRenderer>();
        playerSprite = GameObject.Find("testplayer").GetComponent<SpriteRenderer>();

    }

    void Start()
    {
        inkSprite.color = new Color(Random.value, Random.value, Random.value, 1.0f);//色スクライトにランダムに色をつける
        Debug.Log(inkSprite.color);
        ink = this.gameObject;
        inkcolor = ink.GetComponent<Renderer>();
        mycolor = GameObject.Find("pcolor").GetComponent<Renderer>();
        inkcolor.material.color = inkSprite.color;//箱に渡す
        Debug.Log(inkcolor);

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter(Collision collision)//ぶつかって
    {

        if (collision.gameObject.CompareTag("ink"))//インクというタグがついてたら
        {
            mycolor = collision.gameObject.GetComponent<Renderer>();//プレイヤー３dのいろをぶつかったものの色に変えて
            playerSprite.color = mycolor.material.color;//そのいろをスプライトの自分にあげる

        }
    }
}
