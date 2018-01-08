using UnityEngine;
using System.Collections;

public class enemy2 : MonoBehaviour {
	public Vector3 start;
	public Vector3 stop1;
	public Vector3 stop2;
	public Vector3 end;

	public float moveSpeed;
	public float rotateSpeed;


	private float moveStep;
	private float rotateStep;
	private bool rotating;
	private bool rot;
	private Quaternion prev;
	private int stage = 0;

	void Start(){
		//set location
		Vector3 temp = transform.position;
		start = temp;
		temp.x += 70f;
		end = temp;
		//set step
		moveStep = moveSpeed * Time.deltaTime;
		rotateStep = rotateSpeed * Time.deltaTime;
		moveSpeed = 5f;
		rotateSpeed = 40f;
	}

	void Update() {

		//moveToDest, rotate, back
		if (stage == 0)
			moveTo (end);
		else if (stage == 1) {
			rotation (180f);
		}
		else if (stage == 2)
			moveTo (start);
		else if (stage == 3)
			rotation (0f);


		if (rotating) {
			rotating = false;
			stage += 1;
			if (stage == 4)
				stage = 0;
			//Debug.Log ("000000000000000000000");
		}

	}

	void moveTo(Vector3 location) {
		rotating = false;
		transform.position = Vector3.MoveTowards (transform.position, location, moveStep);
		//Debug.Log (transform.position);
		//Debug.Log (location);

		if ((Mathf.Abs(transform.position.x - location.x) < 0.1f) && (Mathf.Abs(transform.position.z - location.z) < 0.1f)) {
			rotating = true;
			//Debug.Log ("11111111");
		}
		//Debug.Log (rotating);
	}

	void rotation(float a) {
		rotating = false;
		prev = transform.rotation;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f,a,0f), rotateStep);
		//Debug.Log (transform.rotation);
		//Debug.Log (Quaternion.Euler(0f,a,0f));

		if (Quaternion.Euler(0f,a,0f) == transform.rotation) {
			rotating = true;
			//Debug.Log (transform.rotation);
			//Debug.Log (Quaternion.Euler(0f,a,0f));
			//Debug.Log ("222222222222");
		}
		//Debug.Log (rotating);
	}
}



