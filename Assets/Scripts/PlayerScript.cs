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
    float rot;
    float red, green, blue;

    Material playermaterial;
    Material inkmaterial;

    public GameObject player;



    // Use this for initialization
    void Start()
    {

        ThisSprite = this.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        //rrb = this.GetComponent<Rigidbody2D>().mass;重さ
        //rb.bodyType = RigidbodyType2D.Dynamic;typeと普通のは違う
        playermaterial = GetComponent<TrailRenderer>().material;
        player = this.gameObject;

        GameObject colorss = gameObject.transform.Find("color").gameObject;

        inkmaterial = colorss.GetComponent<CollisionPainter>().GetComponent<MeshRenderer>().material.color;
        //なおす！

        //GetComponent<MeshRenderer>().brush.Color;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 prevPos = GameObject.Find("testplayer").transform.position;

        float x = this.transform.position.x - prevPos.x;
        float y = this.transform.position.y - prevPos.y;

        Vector2 vec = new Vector2(x, y).normalized;

        float rot = Mathf.Atan2(vec.y, vec.x) * 180 / Mathf.PI;
        if (rot > 180) rot -= 360;
        if (rot < -180) rot += 360;

        //Debug.Log("Angle = " + rot);

        prevPos = this.transform.position;


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
            ThisSprite.color += new Color(red + 0.5f / 255f, green + 0.5f / 255f, blue + 0.5f / 255f);
            //Debug.Log(ThisSprite.color);
        }

        //Debug.Log(rb.drag);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        touch = true;
        Debug.Log("ook");

        if (rot >= 0.0f)//Returns the length of this vector (Read Only).微妙　上向きに行ってる時はかけたくない
        {
            rb.AddForce(new Vector2(0, 500f));
        }
    }



    private void OnTriggerEnter2D(Collider2D other)//まずぶつかる
    {

        if (other.gameObject.CompareTag("ink"))
        {
            ink = true;
            Debug.Log("getink");
            player.AddComponent<TrailRenderer>();

            SpriteRenderer InkSprite = other.gameObject.GetComponent<SpriteRenderer>();
            Debug.Log(InkSprite.color);
            ThisSprite.color = InkSprite.color;
            playermaterial.color = ThisSprite.color;
            Debug.Log(ThisSprite.color);

            //red = GetComponent<SpriteRenderer>().color.r * 255f;
            //green = GetComponent<SpriteRenderer>().color.g * 255f;
            //blue = GetComponent<SpriteRenderer>().color.b * 255f;
            //Debug.Log(red);
            //Debug.Log(green);
            //Debug.Log(blue);
            rb.drag = 0.2f;//これで空気抵抗とってる　
                           //GetComponent<Collider2D>().sharedMaterial.bounciness = 0.8f;
                           //あとでここに排出書き足す→べつ
                           //ぶつかったときの処理をどこでかくか微妙

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