using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPScript : MonoBehaviour
{

    float time;

    // Use this for initialization
    void Start()
    {
        time = 5;

    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            gameObject.SetActive(false);
        }

    }
}
