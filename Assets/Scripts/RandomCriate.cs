using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCriate : MonoBehaviour
{
    int[] select = new int[2];
    float[] objy = { 8f, 12f, 16f, 20f, 24f, 28f, 32f, 36f };
    int randomy;
    public GameObject[] objselect;
    int number;
    float objx;
    Vector2 objpos;

    // Use this for initialization
    void Start()
    {
        for (int i = 1; i <= 15; i++)
        {
            var parent = GameObject.Find("1").transform;
            number = Random.Range(0, objselect.Length);
            //number = Enumerable.Range(0, objselect.Length).OrderBy(n => Guid.NewGuid()).Take(5);
            objx = Random.Range(-2.83f, 2.83f);
            randomy = Random.Range(0, 4);
            objpos = new Vector2(objx, objy[randomy]);

            Instantiate(objselect[number], objpos, transform.rotation, parent);
        }
        for (int i = 1; i <= 15; i++)
        {
            var parent = GameObject.Find("2").transform;
            number = Random.Range(0, objselect.Length);
            //number = Enumerable.Range(0, objselect.Length).OrderBy(n => Guid.NewGuid()).Take(5);
            objx = Random.Range(-2.83f, 2.83f);
            randomy = Random.Range(4, 7);
            objpos = new Vector2(objx, objy[randomy]);

            Instantiate(objselect[number], objpos, transform.rotation, parent);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}


//GameObject prefab; //エディタのインスペクターで設定しておく
//...
//Instantiate(prefab, transform.position, transform.rotation); //自身と同じ位置同じ回転度でprefabのクローンを作成


//Enumerable.Range(0, 49).OrderBy(n => Guid.NewGuid()).Take(5);