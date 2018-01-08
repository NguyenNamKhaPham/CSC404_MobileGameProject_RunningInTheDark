using UnityEngine;
using System.Collections;

public class animatorPumpkin : MonoBehaviour {

    Animator anim;
    public bool isDead = false;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (!isDead)
        {
            anim.SetBool("isDead", false);
        }
        else if (!isDead)
        {
            anim.SetBool("isDead", true);
        }
	}
}
