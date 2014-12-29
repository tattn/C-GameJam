using UnityEngine;
using System.Collections;

public class GiantEye : MonoBehaviour {

	Vector3 pos;
	float y = -11;

	// Use this for initialization
	void Start () {
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(y < 0) {
			y += Time.deltaTime;
		}
		transform.position = new Vector3 (pos.x, y, pos.z);
	}
}
