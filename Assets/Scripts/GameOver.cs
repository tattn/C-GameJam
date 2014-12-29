using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public AudioSource music;

	static AudioSource musicStatic;

	// Use this for initialization
	void Start () {
		musicStatic = music;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void Dead(){
		GameObject g = GameObject.Find ("GameManager");
		gameBGM bgm = g.GetComponent<gameBGM> ();
		bgm.GameOver ();

		Instantiate(Resources.Load("Prefab/Retry"));
		Instantiate(Resources.Load("Prefab/Title"));
		Instantiate(Resources.Load("Prefab/GameOver"));
		Instantiate(Resources.Load("Prefab/GiantEye"));
		Destroy(GameObject.Find("OK!"));

	}
}
