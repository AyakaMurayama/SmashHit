using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement02 : MonoBehaviour
{
    Text timetext;
    float time = 30.0f;

    // Use this for initialization
    void Start()
    {
        timetext = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timetext.text = time.ToString("f2");

        if (time < 0.03f)
        {
            SceneManager.LoadScene("End");
        }

    }
}
