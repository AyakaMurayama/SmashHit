using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private SpriteRenderer ThisSprite;
    //public SpriteRenderer InkSprite; //なんでここでとれないの？相手だからか
    private Rigidbody2D rb;
    //private Material bounce;//ここあとで聞く
    bool touch = false;
    bool ink = false;

    // Use this for initialization
    void Start()
    {

        ThisSprite = this.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        //rrb = this.GetComponent<Rigidbody2D>().mass;重さ
        //rb.bodyType = RigidbodyType2D.Dynamic;typeと普通のは違う
    }

    // Update is called once per frame
    void Update()
    {
        if (touch == true)//ここの必要性あんまり感じられない　ぶつかったら一回だけにしたい
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.AddForce(new Vector2(0, 300f));
                touch = false;
            }
        }
        if (ink == true && rb.drag >= 0.05)
        {
            rb.drag -= 0.01f * Time.deltaTime; //ここで排出している予定　deltatimeよりほんとは距離でとりたい
            //バウンスの値を型にいれるほうほうがわからない
        }

        Debug.Log(rb.drag);
    }

    private void OnCollisionEnter2D(Collision2D collision)//まずぶつかる
    {
        touch = true;
        Debug.Log("ook");
        if (rb.velocity.magnitude >= 0.0f)//Returns the length of this vector (Read Only).微妙　上向きに行ってる時はかけたくない
        {
            rb.AddForce(new Vector2(0, 500f));
        }
        if (collision.gameObject.CompareTag("ink"))
        {
            ink = true;
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            Debug.Log("getink");
            SpriteRenderer InkSprite = collision.gameObject.GetComponent<SpriteRenderer>();
            Debug.Log(InkSprite.color);
            ThisSprite.color = InkSprite.color;
            rb.drag = 0.2f;//これで空気抵抗とってる　
            //GetComponent<Collider2D>().sharedMaterial.bounciness = 0.8f;
            //あとでここに排出書き足す→べつ

        }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetMouseButtonDown(0))//ぶつかってるときだったらいつもより跳ねる
        {
            rb.AddForce(new Vector2(0, 300f));
        }

    }
}
//    }

//}
//renderer.color = new Color(0f, 0f, 0f, 1f);

//renderc = collision.gameObject.GetComponent<Renderer>();//衝突相手の色コンポーネント
//renderc.material.color = _randomColor.GetBodyColor();//衝突相手の色 ＝ randomcolorscriptのGetBodyColor()の中に入っている色にする

//this.GetComponent<SpriteRenderer>().color = new Color(changeRed, changeGreen, cahngeBlue, cahngeAlpha);

//2dcolliderは2dcolliderがあるらしい
//片方トリガーにすれば重なるからまる



//ぶつかる{
//    いろとる{
//        ていこうつける{
//          いんく排出
//        }
//    }
//    画面をタッチしたら{
//        もし空中だったら{
//            ニダンジャんぷ
//        }
//        もしせっちゃくしてたら{ここ問題　瞬間だからどこでとるかやりながら考える
//            上方向にちから
//        }
//    }
//}