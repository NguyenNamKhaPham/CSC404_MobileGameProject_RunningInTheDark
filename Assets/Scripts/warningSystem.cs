using UnityEngine;
using System.Collections;

public class warningSystem : MonoBehaviour {

	//for warning system
	private ShadowDetector[] sdWarning;
	private ShadowDetector[] sdWarning1;
	private bool isSmoke;
	ParticleSystem p;

	// Use this for initialization
	void Start () {
		p = GameObject.FindGameObjectWithTag ("smoke").GetComponent<ParticleSystem> ();
		p.Stop ();
		//for near-light warning
		sdWarning = gatherShadowObjs("warning");
		sdWarning1 = gatherShadowObjs("warning1");


	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//nearer light
		if (shaded (sdWarning1)) {
			p.startSize = 5;
			if (p.isStopped) {
				p.Play ();
			}
		}
		//near light
		else if (shaded (sdWarning)) {
			p.startSize = 10;
			if (p.isStopped) {
				
				p.Play ();
			}
		} else {
			if (p.isPlaying) {
				
				p.Stop ();
			}
		}
	}

	static ShadowDetector[] gatherShadowObjs(string tag){
		GameObject[] o = GameObject.FindGameObjectsWithTag(tag);
		int l = o.Length;
		ShadowDetector[] s = new ShadowDetector[l];
		for (int i = 0; i < l; i++) {
			s [i] = o [i].GetComponent<ShadowDetector> ();
		}
		return s;
	}

	static bool shaded(ShadowDetector[] sd){
		foreach (ShadowDetector s in sd)
			if (!s.isShaded)
				return true;
		return false;
	}
}
