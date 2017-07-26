using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

	//[SerializeField]
	public float pipeSpeed = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Player.instance.playerDied == false) {
			StartCoroutine (ObstacleMove ());
		} else {
			pipeSpeed = 0f;
		}
	}

	IEnumerator ObstacleMove(){
		yield return new WaitForSeconds (3f);
		transform.Translate (Vector3.left * Time.fixedDeltaTime * pipeSpeed);
	}
}
