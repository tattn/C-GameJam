using UnityEngine;
using System.Collections;

public class playerConfig : MonoBehaviour {

    private Vector3 scale;
    public int x = -1;
    public int y = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setPosition(int inx, int iny)
    {
        x = inx;
        y = iny;

        transform.position = new Vector3(makeStage.objectDistance * -x, 0, makeStage.objectDistance * y);
    }
}
