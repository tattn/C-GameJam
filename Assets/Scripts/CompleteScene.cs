using UnityEngine;
using System.Collections;

public class CompleteScene : MonoBehaviour {

	GameObject[] eye = new GameObject[2];

	private float frame;

	// Use this for initialization
	void Start () {
		eye[0] = Instantiate (Resources.Load ("Prefab/Eye"), new Vector3(0,0,6), Quaternion.Euler(new Vector3(0,-90,0)))as GameObject;
		eye[1] = Instantiate (Resources.Load ("Prefab/Eye"), new Vector3(-52,0,14), Quaternion.Euler(new Vector3(0,90,0)))as GameObject;
		//GameObject o = Instantiate (Resources.Load ("Prefab/Title"), new Vector3 (-23.5f, 0, 22), Quaternion.Euler(new Vector3(90, -180, 0)))as GameObject;
		//o.transform.localScale = new Vector3 (2, 2, 2);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (TimeManager.CurrentFaze());
		if(TimeManager.CurrentFaze () == 3) {
			eye[0].GetComponent<LazerSpawner>().enabled = true;
			eye[1].GetComponent<LazerSpawner>().enabled = true;
			TimeManager.NextFaze();
		}
	}
}
