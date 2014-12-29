using UnityEngine;
using System.Collections;

public class TitleScene : MonoBehaviour {

	public GUIText title;
	public GUIText description;

	public string NextScene;

	//private TimeManager timeManager;

	private bool isFire = false;

	// Use this for initialization
	void Start () {
		//timeManager = GetComponent<TimeManager>();
		title.pixelOffset = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f + 100);
		description.pixelOffset = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f - 100);
		//title.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (TimeManager.CurrentFaze() >= 2 && !isFire) {
			makeStage.launchLazer();
			isFire = true;
		}

		if(Input.GetMouseButtonDown(0)){
			Application.LoadLevel(NextScene);
		}
	}
}
