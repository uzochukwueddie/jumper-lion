using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		//BulletVisible ();
	}

	void BulletVisible(){
		if(transform.position.x > 3.1f){
			Destroy (gameObject);
		}
	}

	void OnBecameInvisible(){
		Destroy (gameObject);
	}


}
