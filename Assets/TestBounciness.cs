using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBounciness : MonoBehaviour {

    SphereCollider ccd;
    Rigidbody rrd;


	// Use this for initialization
	void Start () {
        ccd = gameObject.GetComponent<SphereCollider>();
        rrd = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
    void Update () {
       // rrd.drag *= -0.1f;
    }
}
