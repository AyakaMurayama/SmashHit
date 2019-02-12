using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スタート画面での演出スクリプト
/// </summary>

public class OPScript : MonoBehaviour
{

    float time;//ボールが消えるまでの時間の入れ物　タイマー

    // Use this for initialization
    void Start()
    {
        time = 5;//ボールが消えるまでの時間を入れる

    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;//時間を減らしていく
        if (time <= 0)//もしタイマーが0より小さくなったら
        {
            gameObject.SetActive(false);//プレイヤーを非アクティブにする（このオブジェクトを非アクティブにする）
        }

    }
}
