using UnityEngine;
using System.Collections;

public class raycastForward : MonoBehaviour {
    public GameObject pumpkin;
    private GameObject previousObj;
    private Color previousColor;


	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        pumpkin = GameObject.Find("Player");
        float pumpkinDistance = Vector3.Distance(pumpkin.transform.position, transform.position) - 5;
        float distance;
        //debug
        Vector3 forward = transform.TransformDirection(Vector3.forward) * pumpkinDistance ;
        Debug.DrawRay(transform.position, forward, Color.red);

        if (Physics.Raycast(transform.position, (forward), out hit))
        { 
            //only concerned with environment objects
            if (hit.transform.gameObject.tag == "environment")
            {
                // previous collided object is different from current collided object
                if (previousObj != hit.transform.gameObject)
                {
                    //revert previous color
                    if (previousObj != null)
                    {
                        //previousObj.GetComponent<Renderer>().material.SetColor("_Color", previousColor);
                        previousObj.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    }

                    //debug
                    distance = hit.distance;
                    print(distance + " " + hit.collider.gameObject.name);

                    //set current object as previous
                    previousObj = hit.transform.gameObject;
                    //previousColor = previousObj.GetComponent<Renderer>().material.color;
                    //previousColor.a = 0.5f;
                    //previousObj.GetComponent<Renderer>().material.SetColor("_Color", previousColor);
                    print(previousObj.GetComponent<Renderer>().material.color);
                    previousObj.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                    print("after " + previousObj.GetComponent<Renderer>().material.color);
                    //previousColor.a = 1f;
                }
                // if object is the same... pass
            }else
            {
                if (previousObj != null)
                {
                    //revert back the colors
                    //previousObj.GetComponent<Renderer>().material.SetColor("_Color", previousColor);
                    previousObj.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    previousObj = null;
                }
            }

            
        }
	}
}
