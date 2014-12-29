using UnityEngine;
using System.Collections;

public class makeGrid : MonoBehaviour {

    public GameObject gridPointPrefab;

    public string stageFileName;
    public static GameObject gridEmptyObject;
    public static GameObject[,] gridPoints;
   
    public const float pointDistance = 2f;
    private int gridCols = -1;
    private int gridRows = -1;


	// Use this for initialization
	void Start () {
        readFile();
        makeGridPoint();
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void makeGridPoint()
    {
        gridEmptyObject = new GameObject("gridPoints");
        gridEmptyObject.transform.position = new Vector3(0, 0, 0);
        gridEmptyObject.transform.localScale = new Vector3(1, 1, 1);

        gridPoints = new GameObject[gridRows, gridCols];

        for (int y = 0; y < gridRows; y++)
        {
            for (int x = 0; x < gridCols; x++)
            {
                GameObject p = GameObject.Instantiate(gridPointPrefab) as GameObject;
                p.transform.parent = gridEmptyObject.transform;
                p.transform.name = "gridPoint";

                dotConfig gp = p.GetComponent<dotConfig>();
                gp.setPosition(x, y);

                gridPoints[y, x] = p;
            }
        }
    }

    void readFile()
    {
        System.IO.StreamReader file = new System.IO.StreamReader("./StageDatas/" + stageFileName);

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
        gridCols = mapConfig[0];
        gridRows = mapConfig[1];
        cameraRay.maxWallNum = mapConfig[2];
        /*
        mapData = new GameObject[x, y];
        for (int i = 0; i < y; i++)
        {
            line = file.ReadLine();
            for (int j = 0; j < x; j++)
            {
                mapData[j, i] = (MAPDATA)line[j];
                // 生成するべきオブジェクトの数を数える
                if (mapData[j, i] != MAPDATA.SPACE)
                {
                    num++;
                }
            }
        }
         * */
    }

    void makeMirror(int fromx, int fromy, int tox, int toy)
    {
    }
    
}

