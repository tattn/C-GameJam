using UnityEngine;
using System.Collections;

public class dotConfig : MonoBehaviour {

    bool isMouseOver = false;
    const float size = 0.3f;
    const float mouseOverSize = 0.5f;
    const float flowHeight = 0.6f;
    private Vector3 scale;
    private Vector3 mouseOverScale;
    public int x = -1;
    public int y = -1;

	// Use this for initialization
	void Start () {
        scale = new Vector3(size, size, size);
        mouseOverScale = new Vector3(mouseOverSize, mouseOverSize, mouseOverSize);
	}
	
	// Update is called once per frame
	void Update () {

        if (isMouseOver)
            transform.localScale = mouseOverScale;
        else
            transform.localScale = scale;

        isMouseOver = false;
	}

    public void setPosition(int inx, int iny)
    {
        x = inx;
        y = iny;

        transform.position = new Vector3(makeStage.objectDistance * -x, flowHeight, makeStage.objectDistance * y);
    }

    public void mouseOverAction()
    {
        isMouseOver = true;
    }

    public void mouseClickAction()
    {

    }
}
