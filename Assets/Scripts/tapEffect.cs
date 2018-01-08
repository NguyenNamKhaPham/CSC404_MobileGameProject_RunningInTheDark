using UnityEngine;
using System.Collections;

public class tapEffect : MonoBehaviour {
	public float speed;
	public bool active = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (active) {
			transform.localScale = new Vector3 (3f, 3f, 3f);
			active = false;
		}
		transform.localScale = Vector3.MoveTowards (transform.localScale, new Vector3 (15f, 15f, 15f), speed*Time.deltaTime);
		if (transform.localScale == new Vector3 (15f, 15f, 15f)) {
			transform.localScale = new Vector3 (3f, 3f, 3f);
			this.gameObject.SetActive (false);
		}
	}
}
