using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {
	
	public Text candycount;
	public Text ExitWarning;
	public Canvas inGame_screen;
	public Canvas pause_screen;
	public Canvas leave_screen;
	public Canvas ending_screen;
	public Canvas death_canvas;
	public Button pause_button;
	public int candytotal;
	public Vector3 original_pos;
	public int required_candynum;
	public AudioSource candy_sfx;
	public AudioSource trigger_sfx;
	public AudioSource Endgame_sfx;

	private int candynum;


	public bool keys;
	public bool test_mode;
	private int notActivate = 0;
	private Animator anim;

	//for shadow detection
	private ShadowDetector sd;

	//For movement
	public GameObject tap;
	private Vector3 tapLocation;
	private Ray ray; 
	private Rigidbody rb;
	public float speed;
	private Vector3 s;
	public float speed1;
	public float speed2;
	public float speed3;
	public float speed4;
	public float speed5;
	public float distance1;
	public float distance2;
	public float distance3;
	public float distance4;
	private int debug1;
	private int debug2;

	private float lastClickTime = 0;
	private float catchTime = 0.25f;

	//for movable objects
	GameObject[] movedObjects;
	Vector3[] movedOjectsPosition;

	// Use this for initialization
	void Start () {
		//store original position
		original_pos = transform.position;

		//shadow detector
		sd = this.gameObject.GetComponent<ShadowDetector>();

		//for movement
		tapLocation = transform.position;
		rb = GetComponent<Rigidbody> ();
		rb.freezeRotation = true;

		//store all movable object positions
		movedObjects = GameObject.FindGameObjectsWithTag("movableObject");
		movedOjectsPosition = new Vector3[movedObjects.Length];
		for (int j = 0; j < movedObjects.Length; j++) {
			movedOjectsPosition [j] = movedObjects [j].transform.position;
		}

		anim = GameObject.FindGameObjectWithTag("pumpkin").GetComponent<Animator>();

		//Set UI
		ExitWarning.material.color = Color.white;
		ExitWarning.text = "";
		ExitWarning.CrossFadeAlpha(0.0f, 1f, false);

		pause_button.image.color = Color.white;
		StartCoroutine (Flash_item (pause_button.image, 1.0f, 0.2f, 2, 0.3f));

		inGame_screen.gameObject.SetActive (true);

		if (pause_screen.gameObject.activeInHierarchy == true) {
			pause_screen.gameObject.SetActive (false);
		}
		if (leave_screen.gameObject.activeInHierarchy == true) {
			leave_screen.gameObject.SetActive (false);
		}
		if (ending_screen.gameObject.activeInHierarchy == true) {
			ending_screen.gameObject.SetActive (false);
		}
		if (death_canvas.gameObject.activeInHierarchy == true) {
			death_canvas.gameObject.SetActive (false);
		}

		//set candy
		SetCountText ();
		candynum = 0;


	}

	void Update(){
		//hit light
		if (!sd.isShaded) {
			//pop up warning, pumpkin stops and resqpawns, movable objects respawn
			if (test_mode) {
				StartCoroutine (PopMessage (ExitWarning, "You Shall Not Embrace the Light", 4));
				keys = false;
				rb.velocity = Vector3.zero;
				anim.SetBool ("isDead", true);
				StartCoroutine (respawn ());   
			} else if (death_canvas.gameObject.activeInHierarchy == false) {
				keys = false;
				anim.SetBool ("isDead", true);
				StartCoroutine (death ());  
			}
		}
	}


	void FixedUpdate () {

		if (keys) {
			bool click = false;

			// pc click
			click  = Input.GetMouseButtonDown (0);
					
			if (Input.touchCount == 1 &&  Input.GetTouch(0).phase == TouchPhase.Began || click) {
				Vector2 v;
				bool noPause = false;

				// touch
				//v = new Vector2( Input.GetTouch (0).position.x,Input.GetTouch (0).position.y) ;

				//pc click
				v = new Vector2( Input.mousePosition.x,Input.mousePosition.x) ;


					//print ("tap" + v);
				if (Screen.width - v.x > 200 || Screen.height - v.y > 200) {
					ray = Camera.main.ScreenPointToRay (v);
					noPause = true;
					//print ("pause");
				}

				// pc click
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);


				//identify hit, find the correct hit
				//print (noPause + " " + tapLocation);
				if (noPause) {
					RaycastHit[] hits;
					hits = Physics.RaycastAll (ray);
					for (int i = 0; i < hits.Length; i++) {
						RaycastHit hit = hits [i];
						if (hit.collider.name == "floor") {
							indicateTap (hit);
							break;
						}
					}
				}


				//tapLocation = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);;

				//print (tapLocation);
				Vector3 d = (tapLocation - transform.position);
				if (d.magnitude > distance1) {
					speed = speed1;
					debug1 = 1;
				} else if (d.magnitude > distance2) {
					speed = speed2;
					debug1 = 2;
				} else if (d.magnitude > distance3) {
					speed = speed3;
					debug1 = 3;
				} else if (d.magnitude > distance4) {
					speed = speed4;
					debug1 = 4;
				} else {
					speed = speed5;
					debug1 = 5;
				}
				if (debug1 != debug2) {
					////print (debug1);
					debug2 = debug1;
				}
			}
			Vector3 q = (tapLocation - transform.position);
			if (q.magnitude < 0.1) {
				speed = 0;
			} else if (q.magnitude < 1) {
				speed = speed / 2;
			}
			rb.velocity = q.normalized * speed;


		} else {
			rb.velocity = Vector3.zero;
			speed = 0;
			////print (Input.acceleration.magnitude);
			if (Input.acceleration.magnitude > 2) {
				Camera.main.gameObject.GetComponent<CameraController> ().skip = true;
				////print ("shake");
			}	
			if(Input.GetButtonDown("Fire1")){
				if (Time.time - lastClickTime < catchTime) {
					//double click
					//print ("double click");
					Camera.main.gameObject.GetComponent<CameraController> ().skip = true;
				}
				lastClickTime=Time.time;
			}
		}

	}


	void OnTriggerEnter(Collider other){
		//Candy collector
		print ("hit");
		if (other.gameObject.CompareTag ("Candy")) {
			ExitWarning.text = "";
			ExitWarning.enabled = true;
			ExitWarning.CrossFadeAlpha(1.0f, 0.5f, false);
			candy_sfx.Play ();
			other.gameObject.SetActive (false);
			candynum += 1;
			SetCountText ();
			keys = false;
			tapLocation = transform.position;
			rb.velocity = Vector3.zero;
			Camera.main.gameObject.GetComponent<CameraController> ().unlockALock = true;
		}

		//Exit Level
		else if (other.gameObject.CompareTag ("Exittrigger")) {

			if (candynum >= required_candynum) {
				Time.timeScale = 0;
				if (ending_screen.gameObject.activeInHierarchy == false) {
					ending_screen.gameObject.SetActive (true);
					Endgame_sfx.Play ();
					pause_button.gameObject.SetActive (false);
				}

			} else {
				StartCoroutine (PopMessage (ExitWarning, "CANDY PLEASE", 3));
			}
		}

		else if (other.gameObject.CompareTag ("Button_trigger_level1")) {
			trigger_sfx.Play ();
			other.GetComponent<level1Switch>().Triggered();
		}
		else if (other.gameObject.CompareTag ("Button_trigger1_level3") && notActivate == 0) {
			trigger_sfx.Play ();
			keys = false;
			tapLocation = transform.position;
			rb.velocity = Vector3.zero;
			Camera.main.gameObject.GetComponent<CameraController>().switchS1 = true;
			other.gameObject.GetComponent<Renderer> ().material.color = Color.red;
			notActivate++;
		}
		else if (other.gameObject.CompareTag ("Button_trigger2_level3") && notActivate == 1){
			trigger_sfx.Play ();
			keys = false;
			tapLocation = transform.position;
			rb.velocity = Vector3.zero;
			Camera.main.gameObject.GetComponent<CameraController>().switchS2 = true;
			other.gameObject.GetComponent<Renderer> ().material.color = Color.red;
			notActivate++;
		}
		else if (other.gameObject.CompareTag ("Button_trigger") && notActivate == 0) {
			trigger_sfx.Play ();
			keys = false;
			tapLocation = transform.position;
			rb.velocity = Vector3.zero;
			other.gameObject.GetComponent<Renderer> ().material.color = Color.red;
			Camera.main.gameObject.GetComponent<CameraController> ().level2 = true;
			notActivate++;
		}

	}


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "floor")
            return;
        if (collision.gameObject.GetComponent<Rigidbody>() == null)
        {
            speed = 0;
        }
    }

    void OnTriggerStay(Collider other){
		if (other.gameObject.CompareTag ("Rotator_trigger")) {
			Rotator Rotator_script = other.GetComponent<Rotator>();
			Rotator_script.rotator_triggered();
		}

	}


	void SetCountText(){
		candycount.text = "Candy " + candynum.ToString () + "/" + candytotal;
	}



	//indicate successful tap/click, store and face to that location
	void indicateTap(RaycastHit hit){
		tapLocation = hit.point;
		tapLocation.y = transform.position.y;
		tap.GetComponent <tapEffect> ().active = true;
		tap.SetActive (true);
		tap.transform.position = tapLocation;
		transform.LookAt (tapLocation);
	}

	IEnumerator PopMessage (Text guiText, string message, float delay) {
		guiText.text = message;
		guiText.enabled = true;
		guiText.CrossFadeAlpha(1.0f, 0.5f, false);
		yield return new WaitForSeconds(delay);
		guiText.text = "";
		//guiText.enabled = false;
	}

	IEnumerator Flash_item (Image ImgObj, float max_alpha, float min_alpha, int flash_times, float delay) {
		ImgObj.CrossFadeAlpha(max_alpha, delay, false);
		ImgObj.enabled = true;
		yield return new WaitForSeconds(delay);
		int count = flash_times;
		while (count != 0) {
			ImgObj.CrossFadeAlpha(min_alpha, delay, false);
			yield return new WaitForSeconds(delay);
			ImgObj.CrossFadeAlpha(max_alpha, delay, false);
			yield return new WaitForSeconds(delay);
			count--;
		}


	}

		
	IEnumerator death(){
		yield return new WaitForSeconds(2);
		death_canvas.gameObject.SetActive (true);
		pause_button.gameObject.SetActive (false);
		Time.timeScale = 0;
	}

	IEnumerator respawn()
	{
		//StartCoroutine(PopMessage(ExitWarning, "You Shall Not Embrace the Light", 4));
		////print(Time.time);
		yield return new WaitForSeconds(2);
		tapLocation = original_pos;
		rb.velocity = Vector3.zero;
		keys = true;
		transform.position = original_pos;
		for (int i = 0; i < movedObjects.Length; i++)
		{
			movedObjects[i].transform.position = movedOjectsPosition[i];
		}
		anim.SetBool("isDead", false);
	}
}
