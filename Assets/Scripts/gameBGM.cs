using UnityEngine;
using System.Collections;

public class gameBGM : MonoBehaviour {

	public AudioClip gameover;
	public AudioClip clear;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GameOver()
	{
	    gameObject.audio.clip = gameover;
		gameObject.audio.Play ();
	}

	public void GameClear()
	{
		gameObject.audio.clip = clear;
		gameObject.audio.Play ();
	}

}
