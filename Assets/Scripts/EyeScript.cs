using UnityEngine;
using System.Collections;

public class EyeScript : MonoBehaviour {

	Vector3 pos;
	Vector3 rot;

	float y = -3;
	float rad = -90;

	int faze;

	// Use this for initialization
	void Start () {
		pos = transform.position;
		rot = transform.eulerAngles;
		Vector3 tmp = new Vector3 (pos.x, y, pos.z);
		transform.position = tmp;
		transform.Rotate (rad, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		switch(TimeManager.CurrentFaze ()){
		case 0:
			appear();
			break;
		case 1:
			turn();
			break;
		default:
			break;
		}
	}

	void appear(){
		y += TimeManager.getDelta () * 1.8f;
		if(y >= 0) {
			y = 0;
			TimeManager.NextFaze();
		}
		transform.position = new Vector3 (pos.x, y, pos.z);
	}

	void turn(){
		float r = TimeManager.getDelta () * 45;
		rad += r;
		transform.Rotate (r, 0, 0);
		if(rad >= 0){
			transform.eulerAngles = new Vector3(0, rot.y, rot.z);
			TimeManager.NextFaze();
		}

	}
}
