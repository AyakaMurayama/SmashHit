using UnityEngine;
using System.Collections;


/// <summary>
/// プレイヤーにカメラが追尾するためのスクリプト
/// </summary>


public class CameraControl : MonoBehaviour
{

    private GameObject player = null;//まず中身ない状態に
    private Vector3 offset = Vector2.zero;//カメラとプレイヤーの位置の差を登録する

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//Playerを登録
        offset = transform.position - player.transform.position;//カメラとプレイヤーの位置の差
    }

    void LateUpdate()
    //キャラクター移動の後にカメラ追尾させたいのでLateUpdate()を利用
    //Updateの後に呼ばれる
    {
        Vector3 newPosition = transform.position;//カメラの位置を入れる入れ物
        newPosition.y = player.transform.position.y + offset.y;//カメラの位置をプレイヤーの位置と、最初のカメラとプレイヤーと位置の差を足したものに
        transform.position = Vector3.Lerp(transform.position, newPosition, 10.0f * Time.deltaTime);//元の位置と移動後の位置を補完しながら移動していく
    }
}
