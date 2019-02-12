using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter.Sample;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// プレイヤー関連のスクリプト
/// 色とバウンド処理
/// </summary>

public class PlayerScript : MonoBehaviour
{

    private SpriteRenderer ThisSprite;//プレイヤーのスプライトの入れ物
    private Rigidbody2D rb;//プレイヤーのRigidbodyの入れ物
    bool touch = false;//ステージに触れているか否か
    bool ink = false;//インクを吸っている状態か否か
    float rot;//前フレームとの角度の差
    float red, green, blue;//プレイヤーのスプライトのRGBで管理するための入れ物
    public int count = 0;//何個目のステージまであがることができたか管理するための入れ物
    public float force = 300f;//デフォルトで上むきにかける力
    public GameObject player;//プレイヤーオブジェクト
    public Text stagetex = null;//何ステージ目か表示するためのテキスト
    Text timetext;//時間を表示するためのテキスト
    public Vector2 prevPos;//１フレーム前のプレイヤーの位置

    // Use this for initialization
    void Start()
    {

        ThisSprite = this.GetComponent<SpriteRenderer>();//プレイヤーのスプライトを取得
        rb = GetComponent<Rigidbody2D>();//プレイヤーのRigidbodyを取得
        //rrb = this.GetComponent<Rigidbody2D>().mass;重さがとれる
        //rb.bodyType = RigidbodyType2D.Dynamic;typeと普通のは違う

        player = this.gameObject;//プレイヤーはこのスクリプトが付いているオブジェクト
        GameObject colorss = gameObject.transform.Find("color").gameObject;//colorを取得、プレイヤーの子オブジェクト
        GetComponentInChildren<CollisionPainter>().brush.Color = new Color(1f, 1f, 1f, 0);//こオブジェクト(=color)のCollisionPainterのブラシカラーを取得、透明

        stagetex = GameObject.Find("stagetx").GetComponent<Text>();//テキストを登録
        stagetex.text = "Stage:0";//初期値を表示

        if ((SceneManager.GetActiveScene().name == "main"))//もし今のシーンがmainだったら
        {
            timetext = GameObject.Find("timetx").GetComponent<Text>();//タイムテキストを登録
        }

        AudioManager.Instance.PlayBGM("bgmpp");//BGMを再生
        prevPos = GameObject.Find("testplayer").transform.position;//カリ置きとして、1フレーム前のプレイヤーの位置
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //-----------------前のフレームとどれくらいの角度動いたかをとる処理-----------------//
        //-----------------少し必要性を感じない----------------------------------------//
        prevPos = GameObject.Find("testplayer").transform.position;//<<<<<ーーーーーーーーーーーーーーーここ要検討
        float x = this.transform.position.x - prevPos.x;//プレイヤーの今のxの位置 - 前のxの位置
        float y = this.transform.position.y - prevPos.y;//プレイヤーの今のyの位置 - 前のyの位置

        Vector2 vec = new Vector2(x, y).normalized;//それを向きのベクトルにする　どれくらい前の位置から動いたか

        rot = Mathf.Atan2(vec.y, vec.x) * 180 / Mathf.PI;//変数の宣言場所フィールドの被ってるけど表示できない
        //ラジアンから普通の角度に帰る

        //行きすぎた時は修正する
        if (rot > 180) rot -= 360;
        if (rot < -180) rot += 360;

        prevPos = this.transform.position;//一つ前の位置として登録
        //-------------------------------------------------------------------------//


        //-----------------ステージいぶつかった時に画面タッチされたら---------------------//
        if (touch == true)//もしステージに触れていたら
        {
            if (Input.GetMouseButtonDown(0))//画面タッチされたら
            {
                rb.AddForce(new Vector2(0, 200f));//上方向に力をかける
                touch = false;//触れていないことにする
            }
        }
        //-------------------------------------------------------------------------//


        //-----------------インクを吸っている状態の処理---------------------------------//
        if (ink == true)//もしインクを吸った状態だったら
        {
            ThisSprite.color += new Color(red + 0.5f / 255f, green + 0.5f / 255f, blue + 0.5f / 255f, 1);
            //吸ったインクの色に少しずつ色を足して白にする
            if (force <= 500f)//もしforce(上むきにかけている力)が500より小さかったら
            {
                force += 1.5f;//1.5ずつ足していく
            }
        }

        if (force >= 500f)//もし上向きの力が500以上になったらink処理をやめる
        {

            ink = false;
        }
        //-------------------------------------------------------------------------//



        //-----------------ステージ表示の処理-----------------------------------------//
        stagetex.text = "Stage:" + count.ToString();//ステージテキストを更新
        if ((SceneManager.GetActiveScene().name == "main"))//もし今のシーンがmainだったら
        {
            timetext.text = GameObject.Find("ScoreManager").GetComponent<ScoreManager>().time.ToString("f2");
            //ゲームシーンにあるスコアマネージャーで管理している時間を取得し、タイマーの表示を行う
        }
        //-------------------------------------------------------------------------//
    }

    //-----------------ステージを他のスクリプトに渡す処理----------------------------//
    //-----------------もしかしたらいらない----------------------------------------//
    public int GetCount()//
    {
        return count;
    }
    //-------------------------------------------------------------------------//

    //-----------------何かに接触した時-------------------------------------------//
    //-----------------もしかしたらいらない----------------------------------------//
    private void OnCollisionEnter2D(Collision2D collision)
    {
        touch = true;//触れている状態に

        if (rot >= 0.0f)//もし前フレームとの差が0以上だったら
        {
            rb.AddForce(new Vector2(0, force));//上向きに行ってる時は横向きに力をかけないようにする
        }
    }
    //-------------------------------------------------------------------------//

    //-----------------何かに接触している間---------------------------------------//
    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetMouseButtonDown(0))//ぶつかってるときだったらいつもより跳ねる
        {
            if (ink == false)//もしインクを吸っていなかったら
            {
                rb.AddForce(new Vector2(0, 50f));//上むき方向に50
            }
            if (ink == true)//インクを吸っていたら
            {
                rb.AddForce(new Vector2(0, 10f));//上むき方向に10

            }
        }
    }
    //-------------------------------------------------------------------------//


    //-----------------何かに侵入した時-------------------------------------------//
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("ink"))//侵入したもののオブジェクトのタグがinkだったら
        {
            AudioManager.Instance.PlaySE("wsound");//音を再生

            ink = true;//インクを吸った状態にする
            player.AddComponent<TrailRenderer>();//トレイルレンダラーを追加

            SpriteRenderer InkSprite = other.gameObject.GetComponent<SpriteRenderer>();//侵入したインクのレンダラーを取得
            ThisSprite.color = InkSprite.color;//自身の色をインクの色と同じにする
            GetComponentInChildren<CollisionPainter>().brush.Color = ThisSprite.color;//自身のこオブジェクトのコンポーネントを取得、自分と同じ色にする
            force = 250f;//上むきにはねる時の力を減らす
        }

        if (other.gameObject.CompareTag("level"))//もし侵入したのがレベル判定バーだったら
        {
            count++;//カウントを増やす
            Destroy(other);//もう一度侵入しても問題ないようにする
        }
    }
    //-------------------------------------------------------------------------//
}
