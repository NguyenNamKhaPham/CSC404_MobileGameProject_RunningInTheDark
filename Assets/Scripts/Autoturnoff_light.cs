using UnityEngine;
using System.Collections;


public class Autoturnoff_light : MonoBehaviour {


	public float time;
	public float repeatRate;
	private Light theLight;

	// Use this for initialization
	void Start () {
		theLight = GetComponent<Light> ();
		InvokeRepeating("ChangeLight", time, repeatRate);
	}

	// Update is called once per frame
	void Update () {


	}

	void ChangeLight () {
		if (theLight.intensity > 0) {
			theLight.intensity = 0;
		}else{
			theLight.intensity = 8;
		}
	}
}
