using UnityEngine;
using System.Collections;

public class level1Switch : MonoBehaviour {

	public Light controlled_light;
	// Use this for initialization
	void Start () {
	}

	public void Triggered(){
        //this.gameObject.SetActive (false);
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Standard");
        rend.material.SetColor("_Color", Color.red);
		controlled_light.gameObject.SetActive (false);
	}
}