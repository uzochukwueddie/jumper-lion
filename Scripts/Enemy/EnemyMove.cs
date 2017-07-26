using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	public static EnemyMove instance;

	//[SerializeField]
	public float enemmySpeed = 5f;

	// Use this for initialization
	void Start () {
		MakeInstance ();
	}

	void MakeInstance(){
		if (instance == null) {
			instance = this;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (Player.instance.playerDied == false) {
			StartCoroutine (MoveEnemy ());
		} else {
			enemmySpeed = 0f;
		}
	}

	IEnumerator MoveEnemy(){
		yield return new WaitForSeconds (3f);
		transform.Translate (Vector3.right * Time.fixedDeltaTime * enemmySpeed);
	}
}
