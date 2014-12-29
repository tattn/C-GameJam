using UnityEngine;
using System.Collections;

public class wallConfig : MonoBehaviour {

    bool isMouseOver = false;
    const float thickness = 0.1f;
    const float mouseOverThickness = 0.2f;
    private Vector3 scale;
    private Vector3 mouseOverScale;
    public int x = -1;
    public int y = -1;

    // Use this for initialization
    void Start()
    {
        scale = new Vector3(makeStage.objectDistance * 2, 1, thickness);
        mouseOverScale = new Vector3(makeStage.objectDistance * 2, 1, mouseOverThickness);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isMouseOver) 
		{
			transform.localScale = mouseOverScale;
		}

		else
		{
			transform.localScale = scale;
		}

        isMouseOver = false;
        
    }

    public void setPosition(int inx, int iny)
    {
        x = inx;
        y = iny;

        transform.position = new Vector3(makeStage.objectDistance * -x, 0, makeStage.objectDistance * y);
    }

    public void setRotation(int degree)
    {
        transform.localEulerAngles = new Vector3(0, degree, 0);
    }
    
    public void mouseOverAction()
    {
        isMouseOver = true;
    }

    public void mouseClickAction()
    {

    }
}
