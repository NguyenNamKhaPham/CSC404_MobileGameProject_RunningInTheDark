using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu_Controller : MonoBehaviour {
	//public GameObject player;
	private GameObject wagon1;
	private GameObject wagon2;
	public Canvas pause_canvas;
	public Canvas leave_canvas;
	public Button pause_button;

	void Start () {
		if (pause_canvas.gameObject.activeInHierarchy == true) {
			pause_canvas.gameObject.SetActive (false);
		}
	}

	//public void restart_level() {
		//PlayerController PlayerController_script = player.GetComponent<PlayerController>();
		//player.transform.position = PlayerController_script.original_pos;
		//wagon1 = GameObject.Find ("Environement/cart/cart1");
		//wagon2 = GameObject.Find ("Environement/cart/cart2");
		//wagon1.transform.position = new Vector3 (181f, 0f, 138f);
		//wagon2.transform.position = new Vector3 (158f, 0f, 255f);
		//if (pause_canvas.gameObject.activeInHierarchy == true) {
			//pause_canvas.gameObject.SetActive (false);
			//pause_button.gameObject.SetActive (true);
			//Time.timeScale = 1;
		//}
	//}


	public void pause_pressed(){
		if (pause_canvas.gameObject.activeInHierarchy == false) {
			pause_canvas.gameObject.SetActive (true);
			pause_button.gameObject.SetActive (false);
			Time.timeScale = 0;
		}
	}

	public void resume_pressed(){
		if (pause_canvas.gameObject.activeInHierarchy == true) {
			pause_canvas.gameObject.SetActive (false);
			pause_button.gameObject.SetActive (true);
			Time.timeScale = 1;
		}
	}

	public void leave_pressed(){
		if (pause_canvas.gameObject.activeInHierarchy == true) {
			pause_canvas.gameObject.SetActive (false);
			leave_canvas.gameObject.SetActive (true);
		}
	}

	public void back_pressed(){
		if (leave_canvas.gameObject.activeInHierarchy == true) {
			leave_canvas.gameObject.SetActive (false);
			pause_canvas.gameObject.SetActive (true);
		}
	}

}