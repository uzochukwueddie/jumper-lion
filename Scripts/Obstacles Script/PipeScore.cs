using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeScore : MonoBehaviour {

	private Text scoreText;
	private float score;
	private bool canCollide;

	private Player player;


	void Awake () {
		scoreText = GameObject.Find (Tags.SCORE_TEXT).GetComponent<Text> ();
		player = GameObject.Find (Tags.PLAYER_TAG).GetComponent<Player> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void IncrementScore(float scoreValue){
		score += scoreValue;
		scoreText.text = score.ToString ();
	}

	void OnCollisionStay2D(Collision2D target){
		if (target.gameObject.tag == "Player") {
			IncrementScore (1);
			//player.myRigidbody.isKinematic = true;
			//canCollide = true;

			Physics2D.IgnoreCollision (player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		} 
	}

	//void OnCollisionExit2D(Collision2D target){
		//if (target.gameObject.tag == "Player") {
			//player.myRigidbody.isKinematic = false;
			//canCollide = false;

		//} 
	//}
}
