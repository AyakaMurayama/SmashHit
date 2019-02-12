using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// スコア、時間、ボタン関係を管理しているスクリプト
/// シングルトンを継承している
/// </summary>

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{

    // Use this for initialization

    public int scorem;//スコアを管理する入れ物
    Text timetext;//時間を表示する入れ物
    public float time = 30.0f;//タイマーの残り時間
    bool scenem = false;//シーン移動する状態か否か

    Text highscoretx;//ハイスコアを表示する入れ物
    private int highScore;//ハイスコア
    public int score;//最後のスコア
    private List<int> ranklist = new List<int>() { 0, 0, 0, 0, 0 };//今までのスコアの入れ物
    bool rank = false;//ランキング処理しているか否か


    Text endscore;//最後のスコアのれもの
    //Text endhightx;

    Button button1;//ゲーム開始ボタン
    Button buttonretry;//リトライボタン
    Button buttonrank;//ランキングに繋がるボタン
    Button buttonhome;//ホーム画面にもどるボタン

    public GameObject panel;//シーン移動の際のオブジェクト
    public Text paneltx;//シーン移動の際のテキスト


    protected override void Awake()
    //protected publicとかprivateとかかえるなよってこと override 同時？に動く？継承元と動く
    {
        base.Awake();//継承元のAwakeを実行
        DontDestroyOnLoad(this);//新しいシーンを読み込んでもオブジェクトが自動で破壊されないように設定
    }


    void Start()
    {
        scorem = GameObject.Find("testplayer").GetComponent<PlayerScript>().count;//スコアをPlayerScriptから取得

        SceneManager.sceneLoaded += OnSceneLoaded;//イベントにメゾットを登録


        if (SceneManager.GetActiveScene().name == "Start")//もし今のシーンがスタートだったら
        {
            panel = GameObject.Find("Loadingp");//ローディング用パネル
            panel.SetActive(false);//非表示
            paneltx = GameObject.Find("Loading").GetComponent<Text>();//ローディング用テキスト
            paneltx.text = "";//非表示
        }

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.buildIndex == 0)//もし今のシーンがスタートだったら
        {
            panel = GameObject.Find("Loadingp");
            panel.SetActive(false);//ローディング用パネル
            paneltx = GameObject.Find("Loading").GetComponent<Text>();//ローディング用テキスト
            paneltx.text = "";//非表示
        }
    }

    // Update is called once per frame
    void Update()

    {
        if (SceneManager.GetActiveScene().name == "Start")//もし今のシーンがスタートだったら
        {
            button1 = GameObject.Find("Button").GetComponent<Button>();//button1にボタンを登録

            UnityAction onClickAction = () => SceneManager.LoadScene("main");//押されたら何をするか
            UnityAction onClickActionpanei = () => panel.SetActive(true);
            UnityAction onClickActiontx = () => paneltx.text = "NowLoading...";
            button1.onClick.AddListener(onClickActionpanei);//押されたら実行する
            button1.onClick.AddListener(onClickActiontx);
            button1.onClick.AddListener(onClickAction);

        }



        if (SceneManager.GetActiveScene().name == "main")//もし今のシーンがメインだったら
        {

            time -= Time.deltaTime;//毎フレームごとに減らす

            if (time < 0.03f)//もし0.03より小さくなったら
            {
                timetext = GameObject.Find("timetx").GetComponent<Text>();//時間のテキストを取得
                scenem = true;//ゲーム終了し移動するフラグ
                scorem = GameObject.Find("testplayer").GetComponent<PlayerScript>().count;//スコアを取得
                timetext.text = scorem.ToString();//表示
                rank = true;//スコア処理
                SceneManager.LoadScene("End");//Endへ移動

            }
        }
        if (scenem == true)//もしゲームが終了したら
        {
            time = 30.0f;//タイマーを再度増やす
            scenem = false;//ゲーム開始時に戻す
        }

        Ranking();//ランキング処理をする


        if (SceneManager.GetActiveScene().name == "End")//もしエンドシーンだったら
        {
            endscore = GameObject.Find("endscore").GetComponent<Text>();//スコア表示場所を取得

            endscore.text = "SCORE :" + scorem.ToString();//スコアを表示


            buttonretry = GameObject.Find("Retry").GetComponent<Button>();//リトライボタンを取得
            UnityAction onClickAction = () => SceneManager.LoadScene("main");//押されたら何をするか
            buttonretry.onClick.AddListener(onClickAction);//実行する

            buttonrank = GameObject.Find("Rank").GetComponent<Button>();//ランクボタンを取得
            UnityAction onClickActionr = () => SceneManager.LoadScene("Rank");//押されたら何をするか
            buttonrank.onClick.AddListener(onClickActionr);//実行する

            buttonhome = GameObject.Find("Home").GetComponent<Button>();//ホームボタンを取得
            UnityAction onClickActionrr = () => SceneManager.LoadScene("Start");//押されたら何をするか
            buttonhome.onClick.AddListener(onClickActionrr);//実行する




        }

    }

    public void Ranking()//ランキング処理
    {
        if (SceneManager.GetActiveScene().name == "End" && rank == true)//もし今のシーンがEndでランク処理が必要だったら
        {
            score = scorem;//スコアに最後のスコアを登録

            ranklist.Add(score);//今までのスコアに追加
            rank = false;//ランク処理を不必要とする
        }

        if (SceneManager.GetActiveScene().name == "Rank")//もし今のシーンがRankだったら
        {
            highscoretx = GameObject.Find("Score").GetComponent<Text>();//表示テキストを取得
            ranklist = ranklist.OrderByDescending(score => score).ToList();//降順に並べ替え
            highscoretx.text = "stage" + ":" + ranklist[0].ToString() + "\n" + "stage" + ":" + ranklist[1] + "\n" + "stage" + ":" + ranklist[2].ToString() + "\n" + "stage" + ":" + ranklist[3].ToString() + "\n" + "stage" + ":" + ranklist[4].ToString() + "\n" + "stage" + ":" + ranklist[5].ToString();
            //並べ替えた結果を表示
            rank = false;//ランク処理を不必要とする

        }
    }
}
