using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

	private static float delta;
	private static bool isStop = false;

	static int faze = 0; //0:appear eyes, 1: turn eyes 2: Instattiate OK!

	// Use this for initialization
	void Start () {
		faze = 0;
		isStop = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isStop) {
			delta = Time.deltaTime;
		}
		if(faze == 2) {
			GameObject o = Instantiate(Resources.Load("Prefab/OK!"))as GameObject;
			o.transform.name = "OK!";
			cameraRay.canClick = true;
			NextFaze();
		}
	}

	public static float getDelta(){
		return delta;
	}

	public static void stop(){
		isStop = true;
	}

	public static void restart(){
		isStop = false;
	}

	public static int CurrentFaze(){
		return faze;
	}

	public static void NextFaze(){
		faze++;
	}

}
