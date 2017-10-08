using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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


    protected override void Awake()//protected publicとかprivateとかかえるなよってこと override 同時？に動く？継承元と動く
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }


    void Start()
    {
        scorem = GameObject.Find("testplayer").GetComponent<PlayerScript>().count;
        timetext = GameObject.Find("timetx").GetComponent<Text>();


        //GameObject.Find("ScoreManager").GetComponent<Score>().getrank();

    }

    // Update is called once per frame
    void Update()

    {
        if (SceneManager.GetActiveScene().name == "main")
        {

            time -= Time.deltaTime;
            //timetext.text = time.ToString("f2");

            if (time < 0.03f)
            {
                scenem = true;
                scorem = GameObject.Find("testplayer").GetComponent<PlayerScript>().count;
                timetext = null;
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
            highscoretx.text = ranklist[0].ToString() + "\n" + ranklist[1] + "\n" + ranklist[2].ToString() + "\n" + ranklist[3].ToString() + "\n" + ranklist[4].ToString() + "\n" + ranklist[5].ToString();
            rank = false;

        }
    }
}
