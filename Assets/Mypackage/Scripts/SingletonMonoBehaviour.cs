using UnityEngine;
using System.Collections;

/// <summary>
/// シングルトン
/// デザインパターン(繰り返し現れる経験的な要素を抽出したもの)の一つ
/// インスタンスが一つしか生成されないことを保証
/// </summary>

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    protected static T instance;//インスタンスprotectされているのでこのクラスの中でしかnewされない
    public static T Instance//他のクラスから参照するようのインスタンス　
    {
        get
        {
            if (instance == null)//インスタンスがまだ作られていなかったら
            {
                instance = (T)FindObjectOfType(typeof(T));//シーンないからインスタンスを取得

                if (instance == null)//もしシーン内に存在しなかったら
                {
                    Debug.LogWarning(typeof(T) + "is nothing");//エラーを返す
                }
            }

            return instance;//他のクラス（スクリプトに渡す）
        }
    }

    protected virtual void Awake()
    {
        CheckInstance();//CheckInstanceを実行
    }

    protected bool CheckInstance()//インスタンスがあるか確認
    {
        if (instance == null)//もしなかったら
        {
            instance = (T)this;//シーン内からインスタンスを取得
            return true;//あることを返す
        }
        else if (Instance == this)//もしインスタンスが自身なら
        {
            return true;//あることを返す
        }

        Destroy(this);//それ以外の時は自分を消す
        return false;//なかったと返す
    }
}