using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Rotator : MonoBehaviour {

	public GameObject rotate_object;

	public bool use_trigger;

	public float speedx;
	public float speedy;
	public float speedz;
	public bool if_counter;

	public AudioSource tower_sfx;


	// Update is called once per frame
	void FixedUpdate () {
		if (!use_trigger) {
			transform.Rotate (new Vector3 (speedx, speedy, speedz) * Time.deltaTime);
		}
	}

	public void rotator_triggered () {
		if (tower_sfx.isPlaying != true) {
			tower_sfx.Play ();
		}

		if (if_counter == true){
			rotate_object.transform.Rotate (new Vector3 (speedx, -speedy, speedz) * Time.deltaTime);
		} else {
			rotate_object.transform.Rotate (new Vector3 (speedx, speedy, speedz) * Time.deltaTime);
		}

	}
		





}
