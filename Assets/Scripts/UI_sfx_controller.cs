using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UI_sfx_controller : MonoBehaviour {


	public AudioClip sound;
	 
	private Button button {get{ return GetComponent<Button> ();}}
	private AudioSource source {get{ return GetComponent<AudioSource> ();}}
	// Use this for initialization
	void Start () {
		gameObject.AddComponent<AudioSource> ();
		source.clip = sound;
		source.playOnAwake = false;

		button.onClick.AddListener (() => playsound ());
	}
	
	void playsound (){
		source.PlayOneShot (sound);
	}
}
