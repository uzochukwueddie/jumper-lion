using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour {

	public static Enemy instance;

	[HideInInspector]
	public Transform enemyTransform;

	public float verticalTime = 1f, verticalSpeed = 1f, fallSpeed = 1f;

	private Animator myAnimator;

	[HideInInspector]
	public bool canFlap;

	public GameObject enemyBullet;

	public float enemyBulletForce = 100f;
	public float timer = 3f;

	public GameObject enemyDiedEffect;

	// Use this for initialization
	void Start () {
		MakeInstance ();

		enemyTransform = this.transform;
		myAnimator = GetComponent<Animator> ();
	}

	void MakeInstance(){
		if (instance == null) {
			instance = this;
		}
	}

	void FixedUpdate () {
		if(Player.instance.playerDied == false){
			StartCoroutine (Rise());
		}

		if (canFlap && Player.instance.playerDied == false) {
			myAnimator.SetTrigger ("flying");
		} else {
			myAnimator.ResetTrigger ("flying");
		}
	}

	IEnumerator Rise(){
		enemyTransform.Translate (Vector2.up * verticalSpeed * Time.deltaTime);
		yield return new WaitForEndOfFrame ();

		if(enemyTransform.position.y >= 3f){
			StartCoroutine (Fall ());
		}

	}

	IEnumerator Fall(){
		yield return new WaitForSeconds (1f);

		enemyTransform.Translate (Vector2.down * fallSpeed);

		if(enemyTransform.position.y == -2.5f && enemyTransform.position.y < 0f){
			StartCoroutine (Rise());
		}
	}

	public void EnemyShoot(){
		if (Player.instance.playerDied == false) {
			Vector3 offset = new Vector3 (transform.position.x-2f, transform.position.y, 10f);
			GameObject newBullet = Instantiate (enemyBullet, offset, Quaternion.Euler (new Vector3 (0f, 0f, 180f))) as GameObject;
			newBullet.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-enemyBulletForce * Time.deltaTime, 0f);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == Tags.PLAYER_BULLET_TAG){
			Vector3 effectPos = transform.position;
			effectPos.y += 0f;
			Instantiate(enemyDiedEffect, effectPos, Quaternion.identity);
			Destroy (gameObject);
			Destroy (col.gameObject);
		}

		if(col.tag == Tags.PLAYER_TAG){
			GameplayController.instance.TakeDamage ();

			Vector3 effectPos = transform.position;
			effectPos.y += 0f;
			Instantiate(enemyDiedEffect, effectPos, Quaternion.identity);
			Destroy (gameObject);
			Destroy (col.gameObject);
			Player.instance.playerDied = true;
		}
	}
}
