using UnityEngine;
using System.Collections;

public class LazerSpawner : MonoBehaviour {

	public LaserColor _color = LaserColor.Red;

	private const float Speed = 0.15f;

	private GameObject lazerObject;
	private LazerBehaviour laser;

	private float distance = 0.01f;
	private Vector3 destVec = new Vector3(0f, 0f, 1.0f);

	private bool isFinished = false;

	private bool isStopped = false;

	private bool hitPlayer = false;

	private float timeCounter = 0;

	private LazerSpawner prev = null, next = null;

	// Use this for initialization
	void Start () {
		Object prefab = Resources.Load("Prefab/Lazer");
		lazerObject = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
		laser = lazerObject.GetComponent<LazerBehaviour>();
		laser.owner = this;

		destVec = transform.rotation * destVec;
		destVec.Normalize();
		SetDirection(destVec);

		Debug.Log("LazerSpawner");

		// レーザーの色
		//Color = _color;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isFinished && !isStopped) {
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(transform.position, destVec, out hit)) {
				if (hit.distance <= distance) {
					HittestLaser(hit.collider.gameObject);
				}
			}

			if (!laser.InRangeOfStage()) {
				setFinished(true);
				Color = LaserColor.Blue;
			}

			SetDirection(destVec);
			distance += Speed;
		}

		if (hitPlayer) {
			timeCounter += Time.deltaTime;
			if (timeCounter >= 2.0f) {
				GameObject.FindWithTag("Player").renderer.enabled = false;
				//isFinished = false;
			}
		}
	}

	void HittestLaser(GameObject obj) {
		if (obj.tag == "Mirror"){
			Debug.Log("Hit a Mirror");
			Object spawnerPrefab = Resources.Load("Prefab/LazerSpawner");
			GameObject spawnerObject = Instantiate(spawnerPrefab, obj.transform.position, Quaternion.identity) as GameObject;

			var spawner = spawnerObject.GetComponent<LazerSpawner>();
			Vector3 rot = obj.transform.rotation.eulerAngles;
			
			if (-10 <= rot.y && rot.y <= 10 || 170 <= rot.y && rot.y <= 190) {
				spawner.destVec = new Vector3(destVec.x, 0, -destVec.z);
			}
			else if (80 <= rot.y && rot.y <= 100 || 260 <= rot.y && rot.y <= 280) {
				spawner.destVec = new Vector3(-destVec.x, 0, destVec.z);
			}
			spawner._color = _color;
			spawner.prev = this;
			next = spawner;
			
			distance = Vector3.Distance(transform.position, obj.transform.position);
		}

		if (obj.tag == "Wall") {
			Debug.Log("Hit a Wall");
			distance = Vector3.Distance(transform.position, obj.transform.position);
			Color = LaserColor.Blue;
			setFinished(true);
		}

		if (obj.tag == "Player") {
			obj.GetComponentInChildren<ParticleSystem>().Stop();
			//obj.particleSystem.Play ();
			Instantiate(Resources.Load("Prefab/Dead Particle"), obj.transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));

			GameOver.Dead();
			GameObject.FindWithTag("Player").collider.enabled = false;

			hitPlayer = true;
		}

		isFinished = true;
	}

	//public void SetDirection(float degree) {
	public void SetDirection(Vector3 destVec) {
		LazerBehaviour lazer = lazerObject.GetComponent<LazerBehaviour>();
		if (lazer.line) {
			Vector3 dest = destVec * distance;
			lazer.SetDestination(dest + transform.position);
		}
	}

	public LaserColor Color {
		get {
			return _color;
		}

		set {
			_color = value;
			switch (_color) {
			case LaserColor.Red:
				lazerObject.renderer.material.mainTexture = LaserTextures.Red;
				break;
			case LaserColor.Blue:
				lazerObject.renderer.material.mainTexture = LaserTextures.Blue;
				break;
			case LaserColor.Yellow:
				lazerObject.renderer.material.mainTexture = LaserTextures.Yellow;
				break;
			case LaserColor.Green:
				lazerObject.renderer.material.mainTexture = LaserTextures.Green;
				break;
			}

			changePrevLaserColor(value);
		}
	}

	private void changePrevLaserColor(LaserColor c) {

		if (prev != null) {
			prev.Color = c;

			prev.changePrevLaserColor(c);
		}
	}

	private void changeNextLaserColor(LaserColor c) {
		if (next != null) {
			next.Color = c;
			next.changeNextLaserColor(c);
		}
	}

	private void setFinished(bool f) {
		isStopped = f;
		if (prev != null) {
			prev.setFinished(f);
		}
	}

	/*public void Remove() {
		Destroy(this);
		LazerBehaviour l = lazerObject.GetComponent<LazerBehaviour>();
		if (l.next) {
			l.next.Remove();
		}
		Destroy(lazerObject);
	}*/

	public bool IsFinished() {
		return isStopped;
	}

	public enum LaserColor {
		Red,
		Blue,
		Green,
		Yellow,
	}
}
 