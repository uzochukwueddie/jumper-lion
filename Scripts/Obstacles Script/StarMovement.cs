using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMovement : MonoBehaviour {

	public float starSpeed = 5f;
	private Player player;

	void Start () {
		player = GameObject.Find (Tags.PLAYER_TAG).GetComponent<Player> ();
	}
	
	void FixedUpdate () {
		if (player.playerDied == false) {
			StartCoroutine (StarMove ());
		} else {
			starSpeed = 0f;
		}
	}

	IEnumerator StarMove(){
		yield return new WaitForSeconds (3f);
		transform.Translate (Vector3.left * Time.fixedDeltaTime * starSpeed);
	}
}
