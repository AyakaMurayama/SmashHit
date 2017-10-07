using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement04 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickRank()
    {
        SceneManager.LoadScene("Rank");
        GameObject.Find("ScoreManager").GetComponent<Score>().getrank();
    }
}
