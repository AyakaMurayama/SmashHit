﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCriate : MonoBehaviour
{
    float[] objy = { 8f, 12f, 16f, 20f, 24f, 28f, 32f, 36f, 40f, 44f, 48f, 52f, 56f, 60f, 64f, 68f };
    float[] levely = { 9f, 17f, 25f, 33f, 41f, 49f, 57f, 65f };
    //float[] inky = { 10f, 14f, 28f, 22f, 26f, 30f, 34f, 38f, 42f, 46f, 50f, 54f, 58f, 62f };

    //float[] objx = { -2.83f, -1.0f, 1.0f, 2.83f };
    int randomy, randomx1;
    float randomx2;
    public GameObject[] objselect;
    public GameObject[] ink;
    public GameObject level;
    public GameObject en;

    int number, numberink, numn;
    Vector2 objpos, inkpos, levelpos, enpos;

    public GameObject box1;
    public GameObject box2;
    public GameObject box3;
    float player;

    int num, numink;

    // Use this for initialization
    void Start()
    {
        //for (int i = 1; i <= 10; i++)
        //{
        //    var parent = GameObject.Find("1").transform;
        //    number = Random.Range(0, objselect.Length);
        //    //number = Enumerable.Range(0, objselect.Length).OrderBy(n => Guid.NewGuid()).Take(5);
        //    //objx = Random.Range(-2.83f, 2.83f);
        //    randomy = Random.Range(0, 4);
        //    randomx1 = Random.Range(0, 3);
        //    randomx2 = Random.Range(objx[randomx1], objx[randomx1 + 1]);
        //    objpos = new Vector2(randomx2, objy[randomy]);

        //    Instantiate(objselect[number], objpos, transform.rotation, parent);
        //}

        //num = Random.Range(1, 8); //回す数
        List<float> tempNumsx = new List<float>() { -2f, -1f, 1f, 2f }; //ガラガラの中身
        //List<float> tempNumsy = new List<float>() { 8f, 12f, 16f, 20f, 24f, 28f, 32f, 36f }; //ガラガラの中身


        for (int i = 0; i < objy.Length; i++)
        {
            if (i <= 4)
            {
                for (int j = 0; j < 2; j++)
                {
                    var parent = GameObject.Find("1").transform;
                    number = Random.Range(0, objselect.Length);
                    int randomIndex = Random.Range(0, tempNumsx.Count);
                    Debug.Log(tempNumsx[randomIndex]);
                    objpos = new Vector2(tempNumsx[randomIndex], objy[i]);
                    tempNumsx.Remove(randomIndex);
                    Instantiate(objselect[number], objpos, transform.rotation, parent);
                }
            }
            else if (i >= 5 && i <= 8)
            {
                for (int j = 0; j < 2; j++)
                {
                    var parent = GameObject.Find("2").transform;
                    number = Random.Range(0, objselect.Length);
                    randomy = Random.Range(0, 4);
                    int randomIndex = Random.Range(0, tempNumsx.Count);
                    Debug.Log(tempNumsx[randomIndex]);


                    objpos = new Vector2(tempNumsx[randomIndex], objy[i]);
                    tempNumsx.Remove(randomIndex);
                    Instantiate(objselect[number], objpos, transform.rotation, parent);
                }
            }
            else if (i >= 9 && i <= 12)
            {
                for (int j = 0; j < 2; j++)
                {
                    var parent = GameObject.Find("3").transform;
                    number = Random.Range(0, objselect.Length);
                    randomy = Random.Range(0, 4);
                    int randomIndex = Random.Range(0, tempNumsx.Count);
                    Debug.Log(tempNumsx[randomIndex]);


                    objpos = new Vector2(tempNumsx[randomIndex], objy[i]);
                    tempNumsx.Remove(randomIndex);
                    Instantiate(objselect[number], objpos, transform.rotation, parent);
                }
            }
            else
            {
                for (int j = 0; j < 2; j++)
                {
                    var parent = GameObject.Find("4").transform;
                    number = Random.Range(0, objselect.Length);
                    randomy = Random.Range(0, 4);
                    int randomIndex = Random.Range(0, tempNumsx.Count);
                    Debug.Log(tempNumsx[randomIndex]);


                    objpos = new Vector2(tempNumsx[randomIndex], objy[i]);
                    tempNumsx.Remove(randomIndex);
                    Instantiate(objselect[number], objpos, transform.rotation, parent);
                }
            }
        }
        //numink = Random.Range(1, 8); //回す数
        List<float> inkMunsy = new List<float>() { 10f, 14f, 28f, 22f, 26f, 30f, 34f, 38f, 42f, 46f, 50f, 54f, 58f, 62f };
        List<float> inkMunsx = new List<float>() { -2.5f, -2f, -1.5f, -1.0f, 2.5f, 2f, 1.5f, 1.0f };
        for (int i = 0; i < 12; i++)
        {
            int randomIndex = Random.Range(0, 8);
            int randomIndexy = Random.Range(0, inkMunsy.Count);
            numberink = Random.Range(0, ink.Length);
            //Debug.Log(inkMunsy[randomIndex]);
            //inkMunsy.Remove(randomIndex);
            //inkMunsy.Remove(randomIndex);
            inkpos = new Vector2(inkMunsx[randomIndex], inkMunsy[randomIndexy]);
            Instantiate(ink[numberink], inkpos, transform.rotation);
            inkMunsy.Remove(randomIndexy);

        }

        List<float> enNums = new List<float>() { 10f, 18f, 26f, 34f, 42f, 50f, 58f, 66f }; //ガラガラの中身
        numn = Random.Range(4, enNums.Count); //回す数

        for (int i = 0; i < numn; i++)
        {
            int randomIndex = Random.Range(0, enNums.Count);
            Debug.Log(enNums[randomIndex]);
            enpos = new Vector2(Random.Range(-2.5f, 2.0f), enNums[randomIndex]);
            Instantiate(en, enpos, transform.rotation);
            enNums.Remove(randomIndex);
        }

        for (int k = 0; k < levely.Length; k++)
        {
            levelpos = new Vector2(0f, levely[k]);
            Instantiate(level, levelpos, transform.rotation);
        }




        //for (int i = 1; i <= 10; i++)
        //{
        //    var parent = GameObject.Find("2").transform;
        //    number = Random.Range(0, objselect.Length);
        //    //number = Enumerable.Range(0, objselect.Length).OrderBy(n => Guid.NewGuid()).Take(5);
        //    //objx = Random.Range(-2.83f, 2.83f);
        //    randomy = Random.Range(4, 7);
        //    randomx1 = Random.Range(0, 3);
        //    randomx2 = Random.Range(objx[randomx1], objx[randomx1 + 1]);
        //    objpos = new Vector2(randomx2, objy[randomy]);

        //    Instantiate(objselect[number], objpos, transform.rotation, parent);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("testplayer").transform.position.y;
        if (player > 36f)
        {
            box1.SetActive(false);
        }
    }


}


//GameObject prefab; //エディタのインスペクターで設定しておく
//...
//Instantiate(prefab, transform.position, transform.rotation); //自身と同じ位置同じ回転度でprefabのクローンを作成





//Enumerable.Range(0, 49).OrderBy(n => Guid.NewGuid()).Take(5);
//３つのうち１つ選ぶ

//高さとっていくらより高くなったら1をけすとか
//他スクリプトから高さゲット

//num = Random.Range(1, 4); //回す数
//        List<int> tempNums = new List<int>() { 0, 1, 2 }; //ガラガラの中身

//        for (int i = 0; i<num; i++) {
//            int randomIndex = Random.Range(0, tempNums.Count);
//Debug.Log(tempNums[randomIndex]);
//    tempNums.Remove(randomIndex);
//}

