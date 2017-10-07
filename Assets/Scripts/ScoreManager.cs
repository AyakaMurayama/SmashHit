using System.Collections;
using System.Collections.Generic;
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


    protected override void Awake()//protected publicとかprivateとかかえるなよってこと override 同時？に動く？継承元と動く
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }


    void Start()
    {
        scorem = GameObject.Find("testplayer").GetComponent<PlayerScript>().count;
        timetext = GameObject.Find("timetx").GetComponent<Text>();

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
                SceneManager.LoadScene("End");

            }
        }
        if (scenem == true)
        {
            time = 30.0f;
            scenem = false;
        }
    }
}
