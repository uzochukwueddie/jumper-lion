using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == Tags.ENEMY_TAG){
			Destroy (col.gameObject);
		}

		if (col.tag == Tags.BLOCKER_TAG) {
			Destroy (col.gameObject);
		}
	}
}
