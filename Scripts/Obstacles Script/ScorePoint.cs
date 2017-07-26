using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePoint : MonoBehaviour {

	public float pipeSpeed = 5f;

	public AudioClip barSound;

	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate () {
		if (Player.instance.playerDied == false) {
			StartCoroutine (BarMovement ());
		} else {
			pipeSpeed = 0f;
		}

		BarBecameInVisible ();
	}

	IEnumerator BarMovement(){
		yield return new WaitForSeconds (3f);
		transform.Translate (Vector3.left * Time.fixedDeltaTime * pipeSpeed);
	}
		
	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == Tags.PLAYER_TAG && Player.instance.playerDied == false) {
			Score.IncrementScore ();
			AudioSource.PlayClipAtPoint(barSound, transform.position);
			gameObject.SetActive (false);
		} 
	}

	void BarBecameInVisible(){
		if (transform.position.x < -16f) {
			Destroy (gameObject);
		}
	}
}
