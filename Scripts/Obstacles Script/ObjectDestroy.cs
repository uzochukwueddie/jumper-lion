using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroy : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == Tags.PLAYER_BULLET_TAG){
			Destroy (gameObject);
			Destroy (col.gameObject);
		}

		if(col.tag == Tags.ENEMY_BULLET_TAG){
			//Destroy (col.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D target){
		if(target.gameObject.tag == Tags.PLAYER_TAG){
			Destroy (target.gameObject);
			Player.instance.DiedThroughCollision ();
		}
	}
}
