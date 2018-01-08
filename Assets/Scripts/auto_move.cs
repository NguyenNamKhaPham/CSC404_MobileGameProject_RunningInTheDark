using UnityEngine;
using System.Collections;

public class auto_move : MonoBehaviour {
	private Vector3 moveVector;
	private float refreshCooldown;
	private const float maxCooldown=1F;

	// Use this for initialization
	void Start () {
		moveVector=Vector3.zero;
	}

	// Update is called once per frame
	void Update () {
		refreshCooldown-=Time.deltaTime;
		if (refreshCooldown < 0F) {
			moveVector.x = Random.Range (-5F, 5F);
			//moveVector.y = Random.Range (-1F, 1F);
			moveVector.z = Random.Range (-5F, 5F);
			refreshCooldown = maxCooldown;
		}
		transform.Translate(moveVector*Time.deltaTime);
	}
}
