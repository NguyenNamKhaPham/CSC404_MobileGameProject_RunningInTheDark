using UnityEngine;
using System.Collections;

public class Trigger_Controller_lvl3 : MonoBehaviour {

	public Light controlled_light_1;
	public Light controlled_light_2;
	public bool trigger_1;
	//public GameObject camera;

	private bool is_triggered;
	private bool trigger_active;

	// Use this for initialization
	void Start () {
		is_triggered = false;
		trigger_active = true;
		controlled_light_1.gameObject.SetActive (false);
		controlled_light_2.gameObject.SetActive (true);
	}

	public void Triggered(){
		if ((trigger_active == true) && (trigger_1)) {
			gameObject.GetComponent<Renderer> ().material.color = Color.green;
			controlled_light_1.gameObject.SetActive (true);
			controlled_light_2.gameObject.SetActive (false);

		}else if ((trigger_active == true) && (!trigger_1)) {
			gameObject.GetComponent<Renderer> ().material.color = Color.green;

		}
		trigger_active = false;
	}

}
