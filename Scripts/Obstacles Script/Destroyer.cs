using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D target){

		if (target.gameObject.tag == Tags.PIPE_TAG) {
			Destroy (target.gameObject);
			Destroy (target.transform.parent.gameObject);
		}
	}
}
