using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class clipping_controller : MonoBehaviour {

	public Vector3 clipping_postion;
	public Vector3 clipping_scale;
	public GameObject clipping_block;
	public AudioSource clip_sfx;

	private bool used;
	private Renderer rend;

	void Start () {
		used = false;
		rend = clipping_block.GetComponent<Renderer> ();
	}



	void OnTriggerStay(Collider other){
		if (used == false) {
			if (other.gameObject.CompareTag ("movableObject")) {
				Debug.Log ("1");
				//other.gameObject.transform.position = clipping_postion;
				//other.gameObject.transform.localScale = clipping_scale;
				rend.enabled = true;
				clip_sfx.Play ();
				used = true;
			}
			Debug.Log ("2");

		}

	}
}
