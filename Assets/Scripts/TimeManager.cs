using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : SingletonMonoBehaviour<ScoreManager>
{

    // Use this for initialization

    //Text timetext;
    public float time = 30.0f;
    bool scenem = false;


    protected override void Awake()//protected publicとかprivateとかかえるなよってこと override 同時？に動く？継承元と動く
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }


    void Start()
    {
        //timetext = GameObject.Find("timetx").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()

    {

        time -= Time.deltaTime;
        //timetext.text = time.ToString("f2");

        if (time < 0.03f)
        {
            //scenem = true;
            SceneManager.LoadScene("End");

        }
        if (scenem == true)
        {
            time = 30.0f;
        }
    }
}
