using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤー関連のスクリプト
/// 色とバウンド処理
/// </summary>

public class RandomCriate : MonoBehaviour
{
    float[] objy = { 8f, 12f, 16f, 20f, 24f, 28f, 32f, 36f, 40f, 44f, 48f, 52f, 56f, 60f, 64f, 68f };//オブジェクトが生成される位置
    float[] levely = { 9f, 17f, 25f, 33f, 41f, 49f, 57f, 65f };//レベルバーの生成される位置

    public GameObject[] objselect;//異なる大きさのステージオブジェクト
    public GameObject[] ink;//異なる色のインクオブジェクト
    public GameObject level;//レベルバー
    public GameObject en;//回るオブジェクト

    int number, numberink, numn;//どのオブジェクトを生成するか　
    Vector2 objpos, inkpos, levelpos, enpos;//生成するオブジェクトの位置

    public GameObject box1;//ステージ1
    public GameObject box2;//ステージ2
    public GameObject box3;//ステージ3
    float player;//プレイヤーオブジェクト


    // Use this for initialization
    void Start()
    {


        //-----------------ステージ生成----------------------------------------------//
        List<float> tempNumsx = new List<float>() { -2f, -1f, 1f, 2f };//生成するオブジェクトのx座標
        //ガラガラの中身みたいなもの

        for (int i = 0; i < objy.Length; i++)//オブジェクトが生成される予定のy座標の数だけ繰り返し
        {
            if (i <= 4)//0,1,2,3,4のとき
            {
                for (int j = 0; j < 2; j++)//一つの高さにつき最大2つまで生成できるようにする
                {
                    var parent = GameObject.Find("1").transform;//親オブジェクトを1とする
                    number = Random.Range(0, objselect.Length);//どのステージオブジェクトを生成するかランダムで決めるためのindexを決める
                    int randomIndex = Random.Range(0, tempNumsx.Count);//どの位置に生成するかx座標を決めるためのindexを決める
                    objpos = new Vector2(tempNumsx[randomIndex], objy[i]);//生成するオブジェクトの位置を登録
                    tempNumsx.Remove(randomIndex);//いま生成したx座標を削除する
                    Instantiate(objselect[number], objpos, transform.rotation, parent);//1を親にしてオブジェクトを生成
                }
            }
            else if (i >= 5 && i <= 8)//5,6,7,8のとき
            {
                for (int j = 0; j < 2; j++)//一つの高さにつき最大2つまで生成できるようにする
                {
                    var parent = GameObject.Find("2").transform;//親オブジェクトを2とする
                    number = Random.Range(0, objselect.Length);//どのステージオブジェクトを生成するかランダムで決めるためのindexを決める
                    int randomIndex = Random.Range(0, tempNumsx.Count);//どの位置に生成するかx座標を決めるためのindexを決める
                    objpos = new Vector2(tempNumsx[randomIndex], objy[i]);//生成するオブジェクトの位置を登録
                    tempNumsx.Remove(randomIndex);//いま生成したx座標を削除する
                    Instantiate(objselect[number], objpos, transform.rotation, parent);//2を親にしてオブジェクトを生成
                }
            }
            else if (i >= 9 && i <= 12)//9,10,11,12のとき
            {
                for (int j = 0; j < 2; j++)//一つの高さにつき最大2つまで生成できるようにする
                {
                    var parent = GameObject.Find("3").transform;//親オブジェクトを3とする
                    number = Random.Range(0, objselect.Length);//どのステージオブジェクトを生成するかランダムで決めるためのindexを決める
                    int randomIndex = Random.Range(0, tempNumsx.Count);//どの位置に生成するかx座標を決めるためのindexを決める
                    objpos = new Vector2(tempNumsx[randomIndex], objy[i]);//生成するオブジェクトの位置を登録
                    tempNumsx.Remove(randomIndex);//いま生成したx座標を削除する
                    Instantiate(objselect[number], objpos, transform.rotation, parent);//3を親にしてオブジェクトを生成
                }
            }
            else//それ以外の時
            {
                for (int j = 0; j < 2; j++)//一つの高さにつき最大2つまで生成できるようにする
                {
                    var parent = GameObject.Find("4").transform;//親オブジェクトを4とする
                    number = Random.Range(0, objselect.Length);//どのステージオブジェクトを生成するかランダムで決めるためのindexを決める
                    int randomIndex = Random.Range(0, tempNumsx.Count);//どの位置に生成するかx座標を決めるためのindexを決める
                    objpos = new Vector2(tempNumsx[randomIndex], objy[i]);//生成するオブジェクトの位置を登録
                    tempNumsx.Remove(randomIndex);//いま生成したx座標を削除する
                    Instantiate(objselect[number], objpos, transform.rotation, parent);//4を親にしてオブジェクトを生成
                }
            }
        }
        //-------------------------------------------------------------------------//


        //-----------------インク生成------------------------------------------------//
        List<float> inkMunsy = new List<float>() { 10f, 14f, 28f, 22f, 26f, 30f, 34f, 38f, 42f, 46f, 50f, 54f, 58f, 62f };//インクの生成されるy座標
        List<float> inkMunsx = new List<float>() { -2.5f, -2f, -1.5f, -1.0f, 2.5f, 2f, 1.5f, 1.0f };//インクの生成されるx座標
        for (int i = 0; i < 12; i++)//12回繰り返す
        {
            int randomIndex = Random.Range(0, inkMunsx.Count);//どの位置に生成するかx座標を決めるためのindexを決める
            int randomIndexy = Random.Range(0, inkMunsy.Count);//どの位置に生成するかy座標を決めるためのindexを決める
            numberink = Random.Range(0, ink.Length);//どのインクオブジェクトを生成するかランダムで決めるためのindexを決める
            inkpos = new Vector2(inkMunsx[randomIndex], inkMunsy[randomIndexy]);//生成するオブジェクトの位置を登録
            Instantiate(ink[numberink], inkpos, transform.rotation);//オブジェクトを生成
            inkMunsy.Remove(randomIndexy);//いま生成したy座標を削除する

        }
        //-------------------------------------------------------------------------//

        //-----------------円生成---------------------------------------------------//
        List<float> enNums = new List<float>() { 10f, 18f, 26f, 34f, 42f, 50f, 58f, 66f }; //円の生成されるy座標
        numn = Random.Range(4, enNums.Count); //回す数

        for (int i = 0; i < numn; i++)
        {
            int randomIndex = Random.Range(0, enNums.Count);//どの位置に生成するかy座標を決めるためのindexを決める
            enpos = new Vector2(Random.Range(-2.5f, 2.0f), enNums[randomIndex]);//生成するオブジェクトの位置を登録
            Instantiate(en, enpos, transform.rotation);//オブジェクトを生成
            enNums.Remove(randomIndex);//いま生成したy座標を削除する
        }
        //-------------------------------------------------------------------------//

        //-----------------円生成---------------------------------------------------//
        for (int k = 0; k < levely.Length; k++)//レベルバーの生成予定の位置の数だけ繰り返す
        {
            levelpos = new Vector2(0f, levely[k]);//レベルバーの位置
            Instantiate(level, levelpos, transform.rotation);//生成
        }
        //-------------------------------------------------------------------------//
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("testplayer").transform.position.y;//プレイヤーの位置のy座礁を取得
        if (player > 36f)//もし36より大きくなったら
        {
            box1.SetActive(false);//一つ目のステージは非アクティブに
        }
    }


}
