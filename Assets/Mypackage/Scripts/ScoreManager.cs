using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;


public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{

    // Use this for initialization

    public int scorem;
    Text timetext;
    public float time = 30.0f;
    bool scenem = false;

    Text highscoretx;
    private int highScore;
    public int score;
    private List<int> ranklist = new List<int>() { 0, 0, 0, 0, 0 };
    private string highScoreKey = "highScore";
    bool rank = false;


    Text endscore;
    //Text endhightx;

    Button button1;
    Button buttonretry;
    Button buttonrank;

    protected override void Awake()//protected publicとかprivateとかかえるなよってこと override 同時？に動く？継承元と動く
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }


    void Start()
    {
        scorem = GameObject.Find("testplayer").GetComponent<PlayerScript>().count;
        //timetext = GameObject.Find("timetx").GetComponent<Text>();


        //GameObject.Find("ScoreManager").GetComponent<Score>().getrank();

    }

    // Update is called once per frame
    void Update()

    {
        if (SceneManager.GetActiveScene().name == "Start")
        {
            button1 = GameObject.Find("Button").GetComponent<Button>();
            UnityAction onClickAction = () => SceneManager.LoadScene("main");
            button1.onClick.AddListener(onClickAction);
        }


        if (SceneManager.GetActiveScene().name == "main")
        {

            time -= Time.deltaTime;
            //timetext.text = time.ToString("f2");

            if (time < 0.03f)
            {
                timetext = GameObject.Find("timetx").GetComponent<Text>();
                scenem = true;
                scorem = GameObject.Find("testplayer").GetComponent<PlayerScript>().count;
                timetext.text = scorem.ToString();
                rank = true;
                SceneManager.LoadScene("End");

            }
        }
        if (scenem == true)
        {
            time = 30.0f;
            scenem = false;
        }

        Ranking();


        if (SceneManager.GetActiveScene().name == "End")
        {
            endscore = GameObject.Find("endscore").GetComponent<Text>();
            //endhightx = GameObject.Find("endhigh").GetComponent<Text>(); なおす
            endscore.text = "SCORE :" + scorem.ToString();


            buttonretry = GameObject.Find("Retry").GetComponent<Button>();
            UnityAction onClickAction = () => SceneManager.LoadScene("main");
            buttonretry.onClick.AddListener(onClickAction);

            buttonrank = GameObject.Find("Button (1)").GetComponent<Button>();
            UnityAction onClickActionr = () => SceneManager.LoadScene("Rank");
            buttonrank.onClick.AddListener(onClickActionr);


        }

    }

    public void Ranking()
    {
        if (SceneManager.GetActiveScene().name == "End" && rank == true)
        {
            score = scorem;
            Debug.Log(score);
            // キーを使って値を取得
            // キーがない場合は第二引数の値を取得

            ranklist.Add(score);
            rank = false;
            //highScore = PlayerPrefs.GetInt(highScoreKey, 0);

            //foreach (var x in ranklist)
            //{
            //    Debug.Log(x);
            //}
        }

        if (SceneManager.GetActiveScene().name == "Rank")
        {
            highscoretx = GameObject.Find("Score").GetComponent<Text>();
            ranklist = ranklist.OrderByDescending(score => score).ToList();//(int かfloatとかの要素を取り出してる　pow(x,p); pでなんじょう
            Debug.Log("--------------");
            Debug.Log(ranklist[0]);
            Debug.Log("--------------");
            highscoretx.text = "stage" + ranklist[0].ToString() + "\n" + "stage" + ranklist[1] + "\n" + "stage" + ranklist[2].ToString() + "\n" + "stage" + ranklist[3].ToString() + "\n" + "stage" + ranklist[4].ToString() + "\n" + "stage" + ranklist[5].ToString();
            rank = false;

        }
    }
}
