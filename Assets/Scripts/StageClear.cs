using UnityEngine;
using System.Collections;

public class StageClear : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void Cleared(){
		Debug.Log ("clear");
		GameObject g = GameObject.Find ("GameManager");
		gameBGM bgm = g.GetComponent<gameBGM> ();
		bgm.GameClear ();
		Instantiate (Resources.Load ("Prefab/Clear!!"));
		Instantiate (Resources.Load ("Prefab/Next Stage"));
	}

}
