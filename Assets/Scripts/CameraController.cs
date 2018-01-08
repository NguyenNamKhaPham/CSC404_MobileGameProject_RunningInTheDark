using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class CameraController : MonoBehaviour {

	public GameObject Player;
	public Light l1;
	public GameObject l3;
	private GameObject l2;
	private Vector3 offset;
	private Vector3 pumkinPos;
	public bool level2;
	private Vector3 v;
	private Vector3 v1;
	public int i = 0;
	private Vector3 s;
	private bool q = true;
	private WitchAI w;
	public bool unlockALock;
	public GameObject lock1;
	public GameObject lock2;
	public GameObject lock3;
	public GameObject door;
	public Vector3 doorPos;
	public GameObject magic;
	public GameObject pointToDoor;
	public int j;
	public GameObject question;
	private int oldFlag;
	//transparent
	private ArrayList oldGS = new ArrayList();
	private GameObject[] newGS;
	public int level;
	public Light ll1;
	public Light ll2;
	public Light ll3;
	public bool switchS1;
	public bool switchS2;
	private Vector3 ll1p;
	private Vector3 ll2p;
	private Vector3 ll3p;

	public bool skip = false;
	// Audio Source
	public AudioSource unlockaudio;

	// Use this for initialization
	void Start () {
		offset = new Vector3 (0, 60, -45);
		pumkinPos = Player.transform.position;
		transform.position = pumkinPos + offset;
		transform.LookAt (pumkinPos);
		v = l1.gameObject.transform.position;
		v.y = transform.position.y;
		v.z -= 30f;
		l2 = GameObject.Find ("Witch_Model_Prefab(Clone)");
		w = l2.GetComponent<WitchAI> ();
	}

	// Update is called once per frame
	void LateUpdate () {
        //Debug.Log ("----MAKE TRANS----");
        makeTransparent();
        if (level2) {
			switchOfflevel2 ();
		} else if (unlockALock) {
			if (skip) {
				if (j < 4) {
					if (lock1.activeSelf) {
						lock1.GetComponent<Animator> ().SetBool ("isUnlocked", true);
						StartCoroutine (unlockAni (1.5f, 1));
					} else if (lock2.activeSelf) {
						lock2.GetComponent<Animator> ().SetBool ("isUnlocked", true);
						StartCoroutine (unlockAni (1, 2));
					} else if (lock3.activeSelf) {
						lock3.GetComponent<Animator> ().SetBool ("isUnlocked", true);
						StartCoroutine (unlockAni (1, 3));
					}
				}
				unlockALock = false;
				Player.GetComponent<PlayerController> ().ExitWarning.text = "";
				skip = false;

				if (l2 != null)
					w.pathFlag = oldFlag;
				StartCoroutine (wait2 (0.5f));
				j = 0;
			} else {
				unlock ();
			}
		} else if (switchS1) { 
			level3S1 ();
		} else if (switchS2) { 
			level3S2 ();
		} else {
			
			pumkinPos = Player.transform.position;
			transform.position = pumkinPos + offset;
			transform.LookAt (pumkinPos);
			pointToDoor.transform.LookAt (lock2.transform.position);
			j = 0;
			//Debug.Log (offset);
		}
		skip = false;
	}

	void level3S1(){
		if (j == 0) {
            l2 = GameObject.Find("Witch_Model_Prefab(Clone)");
            
            if (l2 != null) {
                w = l2.GetComponent<WitchAI>();
                oldFlag = w.pathFlag;
				w.pathFlag = 1;
			}
			s = transform.position;
			ll2p = ll2.transform.position;
			ll2p.y += 30;
			ll2p.z -= 35;
			j++;
			q = true;
			StartCoroutine (wait1 (0.5f));
		
		} else if (j == 2) {
			transform.position = Vector3.MoveTowards (transform.position, ll2p, 100f * Time.deltaTime);
			if ((Mathf.Abs (transform.position.x - ll2p.x) < 0.1f) && (Mathf.Abs (transform.position.z - ll2p.z) < 0.1f)) {
				j++;
				q = true;
				StartCoroutine (wait1 (0.25f));
			}
		} else if (j == 4) {
			ll2.intensity = 0;
			j++;
			q = true;
			StartCoroutine (wait1 (0.5f));
		} else if (j == 6) {
			transform.position = Vector3.MoveTowards (transform.position, s, 100f * Time.deltaTime);
			if ((Mathf.Abs (transform.position.x - s.x) < 0.1f) && (Mathf.Abs (transform.position.z - s.z) < 0.1f)) {
				j = 0;
				switchS1 = false;
				GameObject.Find ("Player").GetComponent<PlayerController> ().keys = true;
				if (l2 != null)
					w.pathFlag = oldFlag;
			}
		}
	}

	void level3S2(){
		if (j == 0) {
            l2 = GameObject.Find("Witch_Model_Prefab(Clone)");
            
            if (l2 != null) {
                w = l2.GetComponent<WitchAI>();
                oldFlag = w.pathFlag;
				w.pathFlag = 1;
			}
			s = transform.position;
			ll1p = ll1.transform.position;
			ll1p.y += 50;
			ll1p.z -= 35;
			ll3p = ll3.transform.position;
			ll3p.y += 50;
			ll3p.z -= 35;
			j++;
			q = true;
			StartCoroutine (wait1 (0.5f));
		} else if (j == 2) {
			transform.position = Vector3.MoveTowards (transform.position, ll1p, 100f * Time.deltaTime);
			if ((Mathf.Abs (transform.position.x - ll1p.x) < 0.1f) && (Mathf.Abs (transform.position.z - ll1p.z) < 0.1f)) {
				j++;
				q = true;
				StartCoroutine (wait1 (0.5f));
			}
		} else if (j == 4) {
			ll1.intensity = 0;
			j++;
			q = true;
			StartCoroutine (wait1 (0.5f));
		} else if (j == 6) {
			transform.position = Vector3.MoveTowards (transform.position, ll3p, 100f * Time.deltaTime);
			if ((Mathf.Abs (transform.position.x - ll3p.x) < 0.1f) && (Mathf.Abs (transform.position.z - ll3p.z) < 0.1f)) {
				j++;
				q = true;
				StartCoroutine (wait1 (0.5f));
			}
		} else if (j == 8) {
			ll3.intensity = 0;
			j++;
			q = true;
			StartCoroutine (wait1 (0.5f));
		} else if (j == 10) {
			transform.position = Vector3.MoveTowards (transform.position, s, 100f * Time.deltaTime);
			if ((Mathf.Abs (transform.position.x - s.x) < 0.1f) && (Mathf.Abs (transform.position.z - s.z) < 0.1f)) {
				j = 0;
				switchS2 = false;
				GameObject.Find ("Player").GetComponent<PlayerController> ().keys = true;
				if (l2 != null)
					w.pathFlag = oldFlag;
			}
		}
	}

	void unlock(){
		if (j == 0) {
			Player.GetComponent<PlayerController> ().ExitWarning.text = "Shake/Tap to skip";


            l2 = GameObject.Find("Witch_Model_Prefab(Clone)");
            
            if (l2 != null) {
                w = l2.GetComponent<WitchAI>();
                oldFlag = w.pathFlag;
				w.pathFlag = 1;
			}
			s = transform.position;
			doorPos = door.transform.position;
			doorPos.y += 35;
			doorPos.z -= 35;
			j++;
			q = true;
			StartCoroutine (wait1 (0.5f));
		} else if (j == 2) {
			transform.position = Vector3.MoveTowards (transform.position, doorPos, 100f * Time.deltaTime);
			if ((Mathf.Abs (transform.position.x - doorPos.x) < 0.1f) && (Mathf.Abs (transform.position.z - doorPos.z) < 0.1f)) {
				j++;
				q = true;
				StartCoroutine (wait1 (0.5f));
			}
		} else if (j == 4) {
			if (lock1.activeSelf) {
				lock1.GetComponent<Animator> ().SetBool ("isUnlocked", true);
				StartCoroutine (unlockAni (1.5f, 1));
				j++;
				q = true;
				StartCoroutine (wait1 (2));
			} else if (lock2.activeSelf) {
				lock2.GetComponent<Animator> ().SetBool ("isUnlocked", true);
				StartCoroutine (unlockAni (1, 2));
				j++;
				q = true;
				StartCoroutine (wait1 (2));
			} else if (lock3.activeSelf){
				lock3.GetComponent<Animator> ().SetBool ("isUnlocked", true);
				StartCoroutine (unlockAni (1, 3));
				j++;
				q = true;
				StartCoroutine (wait1 (3));
			}

		} else if (j == 6) {
			transform.position = Vector3.MoveTowards (transform.position, s, 100f * Time.deltaTime);
			if ((Mathf.Abs (transform.position.x - s.x) < 0.1f) && (Mathf.Abs (transform.position.z - s.z) < 0.1f)) {
				unlockALock = false;
				GameObject.Find ("Player").GetComponent<PlayerController> ().keys = true;
				j = 0;
				Player.GetComponent<PlayerController> ().ExitWarning.text = "";
				if (l2 != null)
					w.pathFlag = oldFlag;
			}
		}

	}

	IEnumerator unlockAni(float f, int l){
		unlockaudio.Play ();
		yield return new WaitForSeconds (f);
		if (l == 1)
			lock1.SetActive (false);
		else if (l == 2)
			lock2.SetActive (false);
		else {
			lock3.SetActive (false);
			door.GetComponent<Animator> ().SetBool ("isUnlocked", true);
			magic.SetActive(true);
		}
	}

	IEnumerator wait2(float f){
		yield return new WaitForSeconds (f);
		GameObject.Find ("Player").GetComponent<PlayerController> ().keys = true;
	}

	IEnumerator wait1(float f){
		yield return new WaitForSeconds (f);
		if (q) {
			j++; 
			q = false;
		}
	}

	IEnumerator wait(float f){
		yield return new WaitForSeconds (f);
		if (q) {
			i++; 
			q = false;
		}
	}

	void makeTransparent(){
		//collect all blocked objects
		RaycastHit[] hits;
		hits = Physics.RaycastAll (transform.position, -offset, 100f);
		newGS = new GameObject[hits.Length];
		for (int i = 0; i < hits.Length; i++) {
			newGS [i] = hits[i].collider.gameObject;
		}
		ArrayList t = new ArrayList();
		//old object no longer blocks
		//Debug.Log ("aaaaaaa");
		foreach (GameObject oldG in oldGS){
			if (!exists (oldG, newGS) && oldG.tag == "remove"){
				//Debug.Log (oldG);
				oldG.GetComponent<MeshRenderer> ().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
				t.Add (oldG);
			}
		}
		foreach (GameObject g in t) {
			oldGS.Remove (g);
		}
		//new objects block
		//Debug.Log ("bbbbbbb");
		foreach (GameObject newG in newGS) {
			if (!exists (newG, oldGS) && newG.tag == "remove") {
				//Debug.Log (newG);
				oldGS.Add (newG);
				newG.GetComponent<MeshRenderer> ().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
			}
		}
	}

	//check of GameObject g in array
	bool exists(GameObject g, ArrayList gs){
		int i = gs.Count;
		while (i > 0) {
			i -= 1;
			if (g == gs [i])
				return true;
		}
		return false;
	}

	bool exists(GameObject g, GameObject[] gs){
		int i = gs.Length;
		//Debug.Log (i);
		//Debug.Log (g);
		while (i > 0) {
			i -= 1;
			//Debug.Log (gs[i]);
			if (g == gs [i])
				return true;
		}
		return false;
	}

	void switchOfflevel2(){
		if (i == 0) {
			w.pathFlag = 1;
			s = transform.position;
			i++;
			q = true;
			StartCoroutine (wait (0.5f));
		} else if (i == 2) {
			transform.position = Vector3.MoveTowards (transform.position, v, 100f * Time.deltaTime);
			if ((Mathf.Abs (transform.position.x - v.x) < 0.1f) && (Mathf.Abs (transform.position.z - v.z) < 0.1f)) {
				i++;
				q = true;
				StartCoroutine (wait (0.5f));
			}
		} else if (i == 4) {
			l1.gameObject.SetActive (false);
			i++;
			q = true;
			StartCoroutine (wait (0.5f));
		} else if (i == 6) {
			i++;
		} else if (i == 7) {
			v1 = l2.transform.position;
			v1.y += 50f;
			v1.z -= 35f;
			i++;
		} else if (i == 8) {
			transform.position = Vector3.MoveTowards (transform.position, v1, 100f * Time.deltaTime);
			if ((Mathf.Abs (transform.position.x - v1.x) < 0.1f) && (Mathf.Abs (transform.position.z - v1.z) < 0.1f)) {
				i++;
			}
		} else if (i == 9) {

			Vector3 h = l2.transform.position;
			h.y += 20;
			h.z -= 10;
			question.transform.position = h;
			question.SetActive (true);
			i++;
			q = true;
			StartCoroutine (wait (1));
		} else if (i == 11) {
			question.SetActive (false);
			w.pathFlag = 2;
			i++;
		} else if (i == 12) {
			v1 = l2.transform.position;
			v1.y += 50f;
			v1.z -= 35f;
			transform.position = Vector3.MoveTowards (transform.position, v1, 30f * Time.deltaTime);
			if (w.finish) {
				i++;
			}
		} else if (i == 13) {
			transform.position = Vector3.MoveTowards (transform.position, s, 30f * Time.deltaTime);
			if ((Mathf.Abs (transform.position.x - s.x) < 0.1f) && (Mathf.Abs (transform.position.z - s.z) < 0.1f)) {
				level2 = false;
				GameObject.Find ("Player").GetComponent<PlayerController> ().keys = true;
			}
		}
	}


}
