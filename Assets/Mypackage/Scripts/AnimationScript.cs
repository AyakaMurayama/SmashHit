using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 円形のオブジェクトを回転させるスクリプト
/// </summary>


public class AnimationScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * 30f);//ゆっくり回転させる
    }
}
