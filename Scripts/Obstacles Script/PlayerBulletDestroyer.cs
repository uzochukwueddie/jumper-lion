using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletDestroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){

		if (col.tag == Tags.PLAYER_BULLET_TAG) {
			Destroy (col.gameObject);
		}
	}

}
