using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement02 : MonoBehaviour
{

    public float time = 30.0f;
    public bool end = false;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        //timetext.text = time.ToString("f2");

        if (time < 0.03f)
        {
            end = false;
            SceneManager.LoadScene("End");
        }

    }
}
