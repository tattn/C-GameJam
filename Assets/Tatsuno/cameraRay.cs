using UnityEngine;
using System.Collections;

public class cameraRay : MonoBehaviour
{
    private int numWall;
    public static int maxWallNum;
	public static bool canClick;

	dotConfig clickedDot;

	GameObject[] showNumWall;

    // Use this for initialization
    void Start()
    {
        numWall = 0;
		canClick = false;

		showNumWall = new GameObject [10];
		Object prefab = Resources.Load("Prefab/Wall");
		for (int i = 0; i < showNumWall.Length; i++) 
		{
			GameObject s = GameObject.Instantiate (prefab) as GameObject;
			s.transform.parent = transform;
			s.transform.position = new Vector3(- makeStage.objectDistance * (3 + i / 5f), 0, makeStage.objectDistance * -1.3f);
			s.transform.localEulerAngles = new Vector3(0,90,0);
			showNumWall[i] = s;
		}
    }

    // Update is called once per frame
    void Update()
    {
        mouseAction();
		for (int i = 0; i < showNumWall.Length; i++) 
		{
			if(i < maxWallNum - numWall)
				showNumWall[i].renderer.enabled = true;
			else
				showNumWall[i].renderer.enabled = false;
		}			
    }

    private void mouseAction()
    {
        rayCast();
    }

    private void rayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        
        if (Physics.Raycast(ray, out hit))
        {
            GameObject rayHitObject = hit.collider.gameObject;
            Debug.Log("Touch:" + rayHitObject.name);

			if (rayHitObject.name == "dot" && canClick)
                mouseOverDot(rayHitObject);

			if (rayHitObject.name == "wall" && canClick)
				mouseOverWall(rayHitObject);

			if (rayHitObject.name == "OK!" && canClick)
				mouseOverOK(rayHitObject);

			if (rayHitObject.tag == "Retry")
				mouseOverOK(rayHitObject);

			if (rayHitObject.tag == "Title")
				mouseOverOK(rayHitObject);

			if(rayHitObject.tag == "NextStage")
				mouseOverOK(rayHitObject);

            if (Input.GetMouseButtonDown(0) && rayHitObject)
            {
				if (rayHitObject.name == "dot" && canClick)
                    mouseClickDot(rayHitObject);

				if (rayHitObject.name == "wall" && canClick)
                    mouseClickWall(rayHitObject);

				if(rayHitObject.name == "OK!" && canClick)
					mouseClickOK();

				if(rayHitObject.tag == "Retry")
					Application.LoadLevel(Application.loadedLevelName);
				
				if(rayHitObject.tag == "Title")
					Application.LoadLevel("Title");

				if(rayHitObject.tag == "NextStage");

			}
        }
    }

	void mouseOverWall(GameObject obj){
		wallConfig w = obj.GetComponent<wallConfig> ();
		w.mouseOverAction ();
	}

	void mouseClickOK()
	{
		makeStage.dotsEnabled (false);
		makeStage.launchLazer ();
		canClick = false;
	}

   	void mouseOverOK(GameObject obj)
	{
		okConfig ok = obj.GetComponent<okConfig> ();
		ok.mouseOverAction ();
	}

    void makeWall(dotConfig d1, dotConfig d2)
    {
        if (numWall >= maxWallNum)
            return;

        int px = d1.x - d2.x;
        int py = d1.y - d2.y;

        if (px*px + py*py != 4)
            return;

        if (px == 0)
            if (makeStage.makeWall(d2.x, d2.y + py / 2, 90))
                numWall++;

        if (py == 0)
            if (makeStage.makeWall(d2.x + px / 2, d2.y, 0))
                numWall++;
    }

    void mouseClickWall(GameObject rayHitObject)
    {
        Destroy(rayHitObject);
        numWall--;
    }

	void dotColor(int x, int y, Color c)
	{
		if (!(0 <= x && x < makeStage.stageCols))
						return;
		if (!(0 <= y && y < makeStage.stageRows))
						return;
		if (!makeStage.stageObjects [y, x])
						return;

		makeStage.stageObjects[y,x].transform.renderer.material.color = c;
	}

    void mouseClickDot(GameObject rayHitObject)
    {
        if (numWall >= maxWallNum)
            return;

        if (!clickedDot)
        {
            clickedDot = rayHitObject.GetComponent<dotConfig>();
            Debug.Log("dot(" + clickedDot.x + ", " + clickedDot.y + ")");
            clickedDot.transform.renderer.material.color = Color.red;
			if(clickedDot.x + 1 < makeStage.stageCols)
				if(!makeStage.stageObjects[clickedDot.y , clickedDot.x + 1])
					dotColor(clickedDot.x + 2, clickedDot.y, Color.yellow);

			if(clickedDot.x - 1 >= 0)
				if(!makeStage.stageObjects[clickedDot.y, clickedDot.x - 1])
					dotColor(clickedDot.x - 2, clickedDot.y, Color.yellow);

			if(clickedDot.y + 1 < makeStage.stageRows)
				if(!makeStage.stageObjects[clickedDot.y + 1, clickedDot.x])
					dotColor(clickedDot.x, clickedDot.y + 2, Color.yellow);

			if(clickedDot.y - 1 >= 0)
				if(!makeStage.stageObjects[clickedDot.y - 1, clickedDot.x])
					dotColor(clickedDot.x, clickedDot.y - 2, Color.yellow);	

        }

        else if (clickedDot != rayHitObject)
        {
            makeWall(clickedDot, rayHitObject.transform.GetComponent<dotConfig>());
            clickedDot.transform.renderer.material.color = Color.white;
			dotColor(clickedDot.x + 2, clickedDot.y, Color.white);
			dotColor(clickedDot.x - 2, clickedDot.y, Color.white);
			dotColor(clickedDot.x, clickedDot.y + 2, Color.white);
			dotColor(clickedDot.x, clickedDot.y - 2, Color.white);
            clickedDot = null;
        }
    }

    void mouseOverDot(GameObject rayHitObject)
    {
        if (numWall >= maxWallNum)
            return;

        dotConfig g = rayHitObject.GetComponent<dotConfig>();
        g.mouseOverAction();
    }
}