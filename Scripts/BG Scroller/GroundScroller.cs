using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroller : MonoBehaviour {

	//[SerializeField]
	public float groundSpeed = 2f;

	[HideInInspector]
	[SerializeField] new Renderer renderer;

	private Player player;

	[HideInInspector]
	public Vector2 offset;

	[HideInInspector]
	public bool groundScroll;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer> ();
		player = GameObject.Find (Tags.PLAYER_TAG).GetComponent<Player> ();
	}

	void FixedUpdate () {
		if (groundScroll) {
			offset = new Vector2 (Time.time * groundSpeed, 0); 
			renderer.material.mainTextureOffset = offset;
		}
	}

	/*IEnumerator GroundScroll(){
		yield return new WaitForSeconds (2f);
		offset = new Vector2 (Time.time * groundSpeed, 0); 
		renderer.material.mainTextureOffset = offset;
	}*/

	IEnumerator WaitBeforeReplay(){
		yield return new WaitForSecondsRealtime (2f);
		player.gameOver.SetActive (true);

	}
}
