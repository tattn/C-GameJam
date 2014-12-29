using UnityEngine;
using System.Collections;

public class makeStage : MonoBehaviour {

	//public const string  

    //prefab
    public GameObject eyePrefab;
    public GameObject playerPrefab;
    public GameObject dotPrefab;
    public GameObject mirrorPrefab;
    public GameObject wallPrefab;

	//material
	public Material diffuseMaterial;

	//texture
	public Texture floorTexture;

    //static prefab
    private static GameObject dotPrefabStatic;
    private static GameObject mirrorPrefabStatic;
    private static GameObject wallPrefabStatic;

    //filename
    public string stageFileName;
    
    //stageObjects
    public static GameObject stageObjectsEmptyObject;
    public static GameObject[,] stageObjects;

    //stage config
    public const float objectDistance = 1f;
    public static int stageCols = -1;
    public static int stageRows = -1;

	private bool clearText = false;

	// Use this for initialization
	void Start () {

		clearText = false;

        dotPrefabStatic = dotPrefab;
        mirrorPrefabStatic = mirrorPrefab;
        wallPrefabStatic = wallPrefab;

        int[,] stagedata;
        readFile(stageFileName, out stagedata);

        makeObjects(stagedata);
        makeFloor();
	}
	
	// Update is called once per frame
	void Update () {
		if(isClear () && !clearText) {
			StageClear.Cleared ();
			clearText = true;
		}
	}

	public static void dotsEnabled( bool flag){
		for (int y = 0; y < stageRows; y++ )
		{
			for (int x = 0; x < stageCols; x++)
			{
				if (stageObjects[y, x])
					if (stageObjects[y, x].name == "dot")
						stageObjects[y, x].transform.renderer.enabled = flag;
			}
		}
	}

	public static bool isClear()
	{
		for (int y = 0; y < stageRows; y++ )
			for (int x = 0; x < stageCols; x++)
				if (stageObjects[y, x])
					if (stageObjects[y, x].name == "eye")
						if(!stageObjects[y, x].transform.GetComponent<LazerSpawner>().IsFinished())
							return false;

		return true;
	}

    public static void launchLazer()
    {
		for (int y = 0; y < stageRows; y++ )
		{
			for (int x = 0; x < stageCols; x++)
			{
				if (stageObjects[y, x])
					if (stageObjects[y, x].name == "eye")
						stageObjects[y, x].transform.GetComponent<LazerSpawner>().enabled = true;
			}
		}
    }

    void makeObjects(int[,] data)
    {
        stageObjectsEmptyObject = new GameObject("stageObjects");
        stageObjectsEmptyObject.transform.position = new Vector3(0, 0, 0);
        stageObjectsEmptyObject.transform.localScale = new Vector3(1, 1, 1);

        stageObjects = new GameObject[stageRows, stageCols];

        for (int y = 0; y < stageRows; y++ )
        {
            for (int x = 0; x < stageCols; x++)
            {
                switch (data[y, x])
                {
                    case '*':
                        makeDot(x, y);
                        break;
                    case '|':
                        makeMirror(x, y, 90);
                        break;
                    case '-':
                        makeMirror(x, y, 0);
                        break;
					case '/':
						makeMirror(x, y, -45);
						break;
					case '\\':
						makeMirror(x, y, 45);
						break;
                    case 'p':
                        makePlayer(x, y);
                        break;
                    case '1':
                        makeEye(x, y, 45);
                        break;
                    case '2':
                        makeEye(x, y, 135);
                        break;
                    case '3':
                        makeEye(x, y, -135);
                        break;
                    case '4':
                        makeEye(x, y, -45);
                        break;
					case 'u':
						makeEye(x, y, 180);
						break;
					case 'd':
						makeEye(x, y, 0);
						break;
					case 'l':
						makeEye(x, y, 90);
						break;
					case 'r':
						makeEye(x, y, -90);
						break;
					case 'h':
						makeWall(x, y, 0);
						break;
					case 'v':
						makeWall(x, y, 90);
						break;
                }
            }
        }
    }

    public bool makeEye(int x, int y, int degree)
    {
        Debug.Log("makeEye(" + x + ", " + y + ")");
        if (stageObjects[y, x])
            return false;

        GameObject p = GameObject.Instantiate(eyePrefab) as GameObject;
        p.transform.parent = stageObjectsEmptyObject.transform;
        p.transform.name = "eye";

        eyeConfig gp = p.GetComponent<eyeConfig>();
        gp.setPosition(x, y);
        gp.setRotation(degree);
        
        stageObjects[y, x] = p;

        return true;
    }

    public bool makePlayer(int x, int y)
    {
        Debug.Log("makePlayer(" + x + ", " + y + ")");
        if (stageObjects[y, x])
            return false;

        GameObject p = GameObject.Instantiate(playerPrefab) as GameObject;
        p.transform.parent = stageObjectsEmptyObject.transform;
        p.transform.name = "player";

        playerConfig gp = p.GetComponent<playerConfig>();
        gp.setPosition(x, y);
        stageObjects[y, x] = p;
        return true;
    }

    public static bool makeMirror(int x, int y, int degree)
    {
        Debug.Log("makeMirror(" + x + ", " + y + ")");

        if (stageObjects[y, x])
            return false;

        GameObject p = GameObject.Instantiate(mirrorPrefabStatic) as GameObject;
        p.transform.parent = stageObjectsEmptyObject.transform;
        p.transform.name = "mirror";
        wallConfig gp = p.GetComponent<wallConfig>();
        gp.setPosition(x, y);
        gp.setRotation(degree);
        stageObjects[y, x] = p;

        return true;
    }

    public static bool makeDot(int x, int y)
    {
        Debug.Log("makeDot(" + x + ", " + y + ")");

        if (stageObjects[y, x])
            return false;

        GameObject p = GameObject.Instantiate(dotPrefabStatic) as GameObject;
        p.transform.parent = stageObjectsEmptyObject.transform;
        p.transform.name = "dot";
        dotConfig gp = p.GetComponent<dotConfig>();
        gp.setPosition(x, y);
        stageObjects[y, x] = p;

        return true;
    }

    public static bool makeWall(int x, int y, int degree)
    {
        Debug.Log("makeWall(" + x + ", " + y +")");

        if (stageObjects[y, x])
            return false;

        GameObject p = GameObject.Instantiate(wallPrefabStatic) as GameObject;
        p.transform.parent = stageObjectsEmptyObject.transform;
        p.transform.name = "wall";
        wallConfig gp = p.GetComponent<wallConfig>();
        gp.setPosition(x, y);
        gp.setRotation(degree);
        stageObjects[y, x] = p;

        return true;
    }

    void makeFloor()
    {
        GameObject floors = new GameObject("floor");
        floors.transform.position = new Vector3(0, 0, 0);
        floors.transform.localScale = new Vector3(1, 1, 1);

        for (int y = 0; y < stageRows; y++ )
        {
            for (int x = 0; x < stageCols; x++)
            {
                GameObject tmp = GameObject.CreatePrimitive(PrimitiveType.Cube);
				tmp.transform.parent = floors.transform;
                tmp.transform.position = new Vector3(-objectDistance * x, -0.5f, objectDistance * y);
				tmp.transform.localScale = new Vector3(1f,0.0001f,1f);
				tmp.renderer.material = new Material(diffuseMaterial);
				tmp.renderer.material.mainTexture = floorTexture;
            }
        }
    }

    void readFile(string filename, out int[,] stagedata)
    {
        System.IO.StreamReader file = new System.IO.StreamReader("./StageDatas/" + filename);
        string line;
        int[] mapConfig = new int[3];
        for (int i = 0; i < mapConfig.Length; i++)
        {
            string[] bufs;
            line = file.ReadLine();
            bufs = line.Split(':');
            bufs = bufs[1].Split(';');
            mapConfig[i] = int.Parse(bufs[0]);
        }
        stageCols = mapConfig[0];
        stageRows = mapConfig[1];
        cameraRay.maxWallNum = mapConfig[2];

        stagedata = new int[stageRows,stageCols];
        for (int y = 0; y < stageRows; y++)
        {
            line = file.ReadLine();
            for (int x = 0; x < stageCols; x++)
            {
                stagedata[y, x] = (int)line[x];
            }
        }
    }
}
