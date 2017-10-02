using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement01 : MonoBehaviour
{

    // Use this for initialization

    public void Buttonpush()
    {
        SceneManager.LoadScene("main");
    }
}
