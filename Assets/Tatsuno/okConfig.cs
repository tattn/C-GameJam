using UnityEngine;
using System.Collections;

public class okConfig : MonoBehaviour {

	bool isMouseOver;
	TextMesh t;

	public Color on = Color.red, off = Color.white;

	// Use this for initialization
	void Start () {
		isMouseOver = false;
		t = transform.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isMouseOver)
		{
			t.color = on;
		}

		else
		{
			t.color = off;
		}

		isMouseOver = false;
	}

	public void mouseOverAction(){
		isMouseOver = true;
	}
}
