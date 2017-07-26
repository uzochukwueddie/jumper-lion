using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate () {
		BulletBecameInvisible ();
	}

	void BulletBecameInvisible(){
		if (transform.position.x < -16f) {
			Destroy (gameObject);
		}
	}
}
