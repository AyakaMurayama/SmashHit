using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
	void Update()
	{
        this.transform.rotation = Input.gyro.attitude;
    }
}
