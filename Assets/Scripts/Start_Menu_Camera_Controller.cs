using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Start_Menu_Camera_Controller : MonoBehaviour {
	public GameObject currentmount;
    public GameObject[] gs;
	public float speed;

    public GameObject a;

	void Start () {
		
        GameObject[] gas = GameObject.FindGameObjectsWithTag("save");
        if (gas.Length == 0)
        {
            UnityEngine.Object o = Resources.Load("SaveVar");
            a = Instantiate((GameObject)o);
            a.GetComponent<saveVar>().g = currentmount.tag;
            DontDestroyOnLoad(a);
            transform.position = new Vector3(-150, 50, -5);
        }
        else
        {
            a = GameObject.FindGameObjectWithTag("save");
            if (a.GetComponent<saveVar>().g != currentmount.tag)
                currentmount = gs[1];
            transform.position = currentmount.transform.position;
        }
    }

    void Update () {
		transform.position = Vector3.Lerp (transform.position, currentmount.transform.position, speed);
		transform.rotation = Quaternion.Slerp(transform.rotation, currentmount.transform.rotation, speed);
        if (gs[0] == currentmount)
            a.GetComponent<saveVar>().g = currentmount.tag;
        else
            a.GetComponent<saveVar>().g = gs[1].tag;
    }

	public void SetMount(GameObject newmount){
		currentmount = newmount;
	}

}
