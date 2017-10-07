using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Score : MonoBehaviour
{
    public Text highscoretx;
    private int highScore;
    public int score;
    private List<int> ranklist = new List<int>() { 0, 0, 0, 0, 0 };
    private string highScoreKey = "highScore";

    void Start()
    {
        score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>().scorem;
        // キーを使って値を取得
        // キーがない場合は第二引数の値を取得
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);



    }

    public void getrank()
    {
        ranklist.Insert(0, highScore);
        ranklist.Sort();
        ranklist.Reverse();
        highscoretx.text = ranklist[0].ToString() + "\n" + ranklist[1] + "\n" + ranklist[2].ToString() + "\n" + ranklist[3].ToString() + "\n" + ranklist[4].ToString();

    }

    // スコアの加算
    //void AddScore(int s)
    //{
    //    score = score + s;
    //}

    void Update()
    {
        // Scoreが現在のハイスコアを上回ったらhighScoreを更新
        if (highScore < score)
        {

            highScore = score;
        }

        //highscoretx.text = highScore.ToString();

    }

    public void Save()
    {
        // メソッドが呼ばれたときのキーと値をセットする
        PlayerPrefs.SetInt(highScoreKey, highScore);
        // キーと値を保存
        PlayerPrefs.Save();

    }
}
