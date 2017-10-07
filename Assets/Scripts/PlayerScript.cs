using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter.Sample;
using UnityEngine.UI;

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
    float inktime = 15f;

    public int count = 0;

    public float force = 500f;

    AudioSource inksound;

    Material playermaterial;
    Material inkmaterial;

    public GameObject player;

    public Text stagetex = null;
    Text timetext;

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

        GetComponentInChildren<CollisionPainter>().brush.Color = new Color(1f, 1f, 1f, 0);

        stagetex = GameObject.Find("stagetx").GetComponent<Text>();
        stagetex.text = "Stage:0";
        inksound = GetComponent<AudioSource>();

        timetext = GameObject.Find("timetx").GetComponent<Text>();

        //inkmaterial = colorss.GetComponent<CollisionPainter>().GetComponent<MeshRenderer>().material.color;
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

        float rot = Mathf.Atan2(vec.y, vec.x) * 180 / Mathf.PI;//変数の宣言場所フィールドの被ってるけど表示できない

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
        if (ink == true && rb.drag >= 0.05f && force <= 500f)
        {
            rb.drag -= 0.03f * Time.deltaTime; //ここで排出している予定　deltatimeよりほんとは距離でとりたい
            //バウンスの値を型にいれるほうほうがわからない
            ThisSprite.color += new Color(red + 0.5f / 255f, green + 0.5f / 255f, blue + 0.5f / 255f, 1);
            force += 1f;
            //Debug.Log(ThisSprite.color);
            //if (ThisSprite.color.r >= 0.01f || ThisSprite.color.g >= 0.01f || ThisSprite.color.b >= 0.01f)
            //{
            //    GetComponentInChildren<CollisionPainter>().brush.Color = new Color(1, 1, 1, 0);
            //}
        }

        //Debug.Log(rb.drag);


        stagetex.text = "Stage:" + count.ToString();
        timetext.text = GameObject.Find("ScoreManager").GetComponent<ScoreManager>().time.ToString("f2");
    }


    public int GetCount()
    {
        return count;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        touch = true;
        Debug.Log("ook");

        if (rot >= 0.0f)//Returns the length of this vector (Read Only).微妙　上向きに行ってる時はかけたくない
        {
            rb.AddForce(new Vector2(0, force));
        }
    }



    private void OnTriggerEnter2D(Collider2D other)//まずぶつかる
    {

        if (other.gameObject.CompareTag("ink"))
        {
            //PlayOneShot(inksound);

            ink = true;
            //GetComponentInChildren<CollisionPainter>().enabled = true;
            Debug.Log("getink");
            player.AddComponent<TrailRenderer>();

            SpriteRenderer InkSprite = other.gameObject.GetComponent<SpriteRenderer>();
            Debug.Log(InkSprite.color);
            ThisSprite.color = InkSprite.color;
            playermaterial.color = ThisSprite.color;

            GetComponentInChildren<CollisionPainter>().brush.Color = ThisSprite.color;
            Debug.Log(ThisSprite.color);

            //if(inktime >= 0){
            //for (int i = 15; i > 0; i--)
            //{
            //    GetComponentInChildren<CollisionPainter>().brush.Color = ThisSprite.color;

            //}インク排出してその色も出したかった
            rb.drag = 0.4f;//これで空気抵抗とってる　
                           //GetComponent<Collider2D>().sharedMaterial.bounciness = 0.8f;
                           //あとでここに排出書き足す→べつ
                           //ぶつかったときの処理をどこでかくか微妙
                           //常に上向きに力かかってるから意味がない
            force = 400f;


        }

        if (other.gameObject.CompareTag("level"))
        {
            count++;
            Destroy(other);
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