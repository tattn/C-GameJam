using UnityEngine;
using System.Collections;

public class LazerBehaviour : MonoBehaviour {

	public LineRenderer line;

	public LazerSpawner owner;

	private Vector3 currentPosition = new Vector3(0.5f, 0, 0.5f);

	// Use this for initialization
	public void Start () {
		line = GetComponent<LineRenderer>();
		line.SetWidth(0.1f, 0.1f);
		line.SetPosition(0, transform.position);
		line.SetPosition(1, transform.position);
	}
	
	// Update is called once per frame
	public void Update () {
	
	}

	public void SetDestination(Vector3 dest) {
		line.SetPosition(1, dest);
		currentPosition = dest;
	}

	public bool InRangeOfStage() {
		const float offset = 0.5f;
		float stageW = (makeStage.stageCols - 1) * makeStage.objectDistance + offset;
		float stageH = (makeStage.stageRows - 1) * makeStage.objectDistance + offset;

		return !(-offset > -currentPosition.x || -offset > currentPosition.z ||
		        stageW < -currentPosition.x || stageH < currentPosition.z);

		
	}
}

