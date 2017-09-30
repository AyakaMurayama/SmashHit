using UnityEngine;
using System.Collections;

public class Player1 : MonoBehaviour
{

	float speed = 5.0f;

	void Update()
	{
		var dir = Vector3.zero;
		dir.x = Input.acceleration.x;
		//dir.y = Input.acceleration.y;

		if (dir.sqrMagnitude > 1)
		{
			dir.Normalize();
		}

		dir *= Time.deltaTime;

		transform.Translate(dir * speed);
	}
}

//加速度でとってる