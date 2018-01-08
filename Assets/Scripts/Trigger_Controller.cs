using UnityEngine;
using System.Collections;

public class Trigger_Controller : MonoBehaviour {

	public int level_number;
	public GameObject camera;



	[Header("level 3")]
	public Light controlled_light_1;
	public Light controlled_light_2;
	public GameObject controlled_trigger;  
	public bool trigger_1;

	private bool trigger_active;

	// Use this for initialization
	void Start () {
		trigger_active = true;

		if (level_number == 3) {			
			controlled_light_1.intensity = 0;
			controlled_light_2.intensity = 4;
		}
	}

	public void Triggered(){
	}		
}
