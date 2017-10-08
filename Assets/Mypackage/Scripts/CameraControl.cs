using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{

    private GameObject player = null;
    private Vector3 offset = Vector2.zero;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = player.transform.position.y + offset.y;
        transform.position = Vector3.Lerp(transform.position, newPosition, 10.0f * Time.deltaTime);
    }
}

//てst