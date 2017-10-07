using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{

    // Use this for initialization

    public int scorem;

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }


    void Start()
    {
        //scorem = GameObject.Find("testplayer").GetComponent<PlayerScript>().count;

    }

    // Update is called once per frame
    void Update()

    {
        scorem = GameObject.Find("testplayer").GetComponent<PlayerScript>().count;

    }
}
