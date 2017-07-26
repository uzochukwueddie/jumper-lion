using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipedestroyer : MonoBehaviour {

	public static Pipedestroyer instance;

	[HideInInspector]
	public bool canRespawn;

	void Start(){
		MakeInstance ();
	}

	void Update(){
		
	}

	void MakeInstance(){
		if(instance == null){
			instance = this;
		}
	}

	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == Tags.STAR_TAG) {
			Destroy (target.gameObject);
		}
			
		if (target.gameObject.tag == Tags.ENEMY_BULLET_TAG) {
			Destroy (target.gameObject);
		}
	}
}
