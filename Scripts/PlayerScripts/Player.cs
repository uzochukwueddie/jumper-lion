using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {

	public static Player instance;

	public float jumpPower = 10f;
	public float secondJumpPower = 10f;
	public Transform groundCheckPosition;
	public float radius = 0.3f;
	public LayerMask layerGround;
	public GameObject playerDiedEffect;
	public GameObject gameOver;
	public AudioClip deathSound, buttonSound, bulletSound;

	private Animator myAnimator;
	[HideInInspector]
	public bool gameStarted;
	[HideInInspector]
	public bool isGrounded;
	private bool playerJumped;
	private bool canDoubleJump;
	[HideInInspector]
	public bool playerDied = false;

	[HideInInspector]
	public Rigidbody2D myRigidbody;

	private BGScroller bgScoller;
	private GroundScroller groundScroller;

	[SerializeField]
	private GameObject playerBullet;

	[HideInInspector]
	public bool canShoot;

	public float bulletForce = 100f;

	[SerializeField]
	private BoxCollider2D boxCollider;

	[SerializeField]
	private PolygonCollider2D polygonCollider;

	[SerializeField]
	private float minY = -2.73f, maxY = 3f;


	void Awake(){
		myAnimator = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
		bgScoller = GameObject.Find (Tags.BG_GAMEOBJECT).GetComponent<BGScroller> ();
		groundScroller = GameObject.Find (Tags.GROUND_GAMEOBJECT).GetComponent<GroundScroller> ();

		Vector3 startPos = new Vector3(-6f, -2.73f, 10f);
		transform.position = startPos;

		myAnimator.SetFloat ("speed", 0f);


		boxCollider = GameObject.Find (Tags.PLAYER_TAG).GetComponent<BoxCollider2D> ();
		polygonCollider = GameObject.Find (Tags.PLAYER_TAG).GetComponent<PolygonCollider2D> ();

		Physics2D.IgnoreCollision (boxCollider, polygonCollider, true);
	}

	// Use this for initialization
	void Start () {
		MakeInstance ();

		StartCoroutine (StartGame ());
		gameOver.SetActive (false);

		float aspect = (float)Screen.width / (float)Screen.height;

		if (aspect == 1.777777) {
			Vector3 startPos1 = new Vector3 (-6f, -2.73f, 10f);
			transform.position = startPos1;
		} else {
			Vector3 startPos1 = new Vector3(-4.2f, -2.73f, 10f);
			transform.position = startPos1;
		}
	}

	void MakeInstance(){
		if (instance == null) {
			instance = this;
		}
	}

	void PlayerGrounded(){
		isGrounded = Physics2D.OverlapCircle (groundCheckPosition.position, radius, layerGround);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void ButtonDown(BaseEventData e){
		playerJumped = true;
	}

	public void ButtonUp(BaseEventData e){
		playerJumped = false;
	}

	public void PlayerShoot(BaseEventData e){
		if (gameStarted) {
			myAnimator.SetBool ("shoot", true);
			StartCoroutine (PlayerShootBullet (0.1f));
		}
	}

	public void PlayerNotShooting(BaseEventData e){
		myAnimator.SetBool ("shoot", false);
	}

	void FixedUpdate(){
		if (gameStarted && playerDied == false) {
			myAnimator.SetFloat ("speed", 1f);
			PlayerGrounded ();
		}

		if (playerJumped == true) {
			if (gameStarted && isGrounded) {
				myAnimator.SetBool ("jump", true);
				//transform.Translate (Vector3.up * Time.deltaTime * jumpPower, Space.World);
				myRigidbody.velocity = new Vector3(0f, Time.deltaTime * jumpPower, 0f);
				Vector3 clampedPos = transform.position;
				clampedPos.y = Mathf.Clamp (clampedPos.y, minY, maxY);
				transform.position = clampedPos;
				AudioSource.PlayClipAtPoint (buttonSound, transform.position, 1f);
			}
		} else{
			myAnimator.SetBool ("jump", false);
		}
	}

	IEnumerator PlayerShootBullet(float shootTime){
		yield return new WaitForSeconds (shootTime);

		Vector3 offset = new Vector3(transform.position.x, transform.position.y+(-0.5f), 10f);
		GameObject newBullet = Instantiate (playerBullet, offset, Quaternion.identity) as GameObject;
		newBullet.GetComponent<Rigidbody2D> ().velocity = new Vector2(bulletForce, 0f);

		AudioSource.PlayClipAtPoint (bulletSound, transform.position, 3f);

		myAnimator.SetBool ("shoot", false);
	}

	IEnumerator StartGame(){
		yield return new WaitForSeconds (3f);
		gameStarted = true;
		bgScoller.canScroll = true;
		groundScroller.groundScroll = true;
		Pipedestroyer.instance.canRespawn = true;
	}

	public void DiedThroughCollision(){
		GameplayController.instance.TakeDamage ();
		Vector3 effectPos = transform.position;
		effectPos.y += 0f;
		Instantiate(playerDiedEffect, effectPos, Quaternion.identity);
		Destroy (gameObject);
		playerDied = true;
		AudioSource.PlayClipAtPoint(deathSound, transform.position, 3f);

		myAnimator.SetFloat ("speed", 0f);
		myAnimator.SetBool ("jump", false);

		bgScoller.offsetSpeed = 0f;
		groundScroller.groundSpeed = 0f;

		bgScoller.myRenderer.material.mainTextureOffset = new Vector2 (bgScoller.offsetSpeed, 0);
		groundScroller.offset = new Vector2 (groundScroller.groundSpeed, 0);
	}

	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == Tags.PIPE_TAG) {

			DiedThroughCollision ();
		}

		if(target.gameObject.tag == Tags.ENEMY_BULLET_TAG){
			GameplayController.instance.TakeDamage ();

			Vector3 effectPos = transform.position;
			effectPos.y += 0f;
			Instantiate(playerDiedEffect, effectPos, Quaternion.identity);
			Destroy (gameObject);
			Destroy (target.gameObject);
			playerDied = true;
			Enemy.instance.canFlap = false;

			bgScoller.offsetSpeed = 0f;
			groundScroller.groundSpeed = 0f;

			bgScoller.myRenderer.material.mainTextureOffset = new Vector2 (bgScoller.offsetSpeed, 0);
			groundScroller.offset = new Vector2 (groundScroller.groundSpeed, 0);


		}
	}

	void OnTriggerEnter2D(Collider2D target){
		if(target.tag == Tags.ENEMY_TAG){

			bgScoller.offsetSpeed = 0f;
			groundScroller.groundSpeed = 0f;

			bgScoller.myRenderer.material.mainTextureOffset = new Vector2 (bgScoller.offsetSpeed, 0);
			groundScroller.offset = new Vector2 (groundScroller.groundSpeed, 0);

		}
	}
}
