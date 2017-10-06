using UnityEngine;
using System.Collections;
using SnipnTweet;

public class SnipNTweetBehaviour : MonoBehaviour {

	public string twitter_UserName = "";
	public string twitter_Password = "";

	protected SNT_NetworkAdapter adapter;
	public Camera camera;

	// Use this for initialization
	void Start () {
		adapter = this.gameObject.AddComponent<SNT_NetworkAdapter>();
		adapter.Version();
		adapter.Connect(twitter_UserName, twitter_Password);

		this.camera = Camera.main;
	}

}
