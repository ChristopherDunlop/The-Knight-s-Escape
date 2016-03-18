using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mopController : MonoBehaviour {

	public GameObject mopBackground, postMop, smooth, moppedSquares;	
	public List<Vector3> posList = new List<Vector3>();

	private float speed = 10;
	private float movement = 0;	
	public bool moving = false;
	public int mopped = 0;

	// Use this for initialization
	void Start () {
		smooth = new GameObject();
		smooth.name=("Fluid Motion");
		moppedSquares = new GameObject ();
		moppedSquares.name = ("Mopped Squares");
		Rigidbody2D player = GetComponent<Rigidbody2D> ();		
		Vector3 playerPos = player.transform.position;
		float x2 = Mathf.Round(playerPos.x*100);
		float y2 = Mathf.Round(playerPos.y*100);
		
		playerPos.Set((x2)/100,(y2)/100,0);
		posList.Add (playerPos);
		Instantiate (mopBackground, (playerPos), Quaternion.identity);

		GameObject newBackground = GameObject.Find("Background(Clone)");
		newBackground.name=("Background " + playerPos);
		newBackground.transform.parent = smooth.transform;
	}
	
	// Update is called once per frame
	void Update () {

		var vertical = Input.GetAxis("Vertical");
		var horizontal = Input.GetAxis("Horizontal");

		if (moving == false) {
			if (vertical > 0 ) {
				GetComponent<Rigidbody2D> ().transform.position += Vector3.up * speed * Time.deltaTime;
				movement = 1;
				moving = true;
			} else if (vertical < 0 ) {
				GetComponent<Rigidbody2D> ().transform.position += Vector3.down * speed * Time.deltaTime;
				movement = 2;
				moving = true;
			} else if (horizontal > 0 ) {
				GetComponent<Rigidbody2D> ().transform.position += Vector3.right * speed * Time.deltaTime;
				movement = 3;
				moving = true;
			} else if (horizontal < 0 ) {
				GetComponent<Rigidbody2D> ().transform.position += Vector3.left * speed * Time.deltaTime;
				movement = 4;
				moving = true;
			}
		} else {		
			if (movement == 1) {
				GetComponent<Rigidbody2D> ().transform.position += Vector3.up * speed * Time.deltaTime;
			} else if (movement == 2) {
				GetComponent<Rigidbody2D> ().transform.position += Vector3.down * speed * Time.deltaTime;
			} else if (movement == 3) {
				GetComponent<Rigidbody2D> ().transform.position += Vector3.right * speed * Time.deltaTime;
			} else if (movement == 4) {
				GetComponent<Rigidbody2D> ().transform.position += Vector3.left * speed * Time.deltaTime;
			}

		}
	}

	void OnCollisionEnter2D(Collision2D other) {

		resetPos ();
	}

	void OnCollisionStay2D(Collision2D other) {

		resetPos ();
	}

	void OnTriggerExit2D(Collider2D other){

		Rigidbody2D player = GetComponent<Rigidbody2D> ();
		
		Vector3 playerPos = player.transform.position;
		
		float x = playerPos.x * 100;
		float y = playerPos.y * 100;
		
		x = x - (x % 128);
		y = y - (y % 128);
		
		playerPos.Set (x / 100, y / 100, 0);

		Instantiate (mopBackground, (playerPos), Quaternion.identity);
		GameObject newBackground = GameObject.Find("Background(Clone)");
		newBackground.name=("Background " + playerPos);
		newBackground.transform.parent = smooth.transform;

		if (!posList.Contains(playerPos)) {
			posList.Add (playerPos);
		}
	}

	void resetPos(){
		movement = 0;
		Rigidbody2D player = GetComponent<Rigidbody2D> ();
		Vector3 playerPos = player.transform.position;

		float x = Mathf.Round(playerPos.x*100);
		float y = Mathf.Round(playerPos.y*100);
		
		x = x - (x % 128);
		y = y - (y % 128);
		playerPos.Set(x/100,y/100,0);
		
		GetComponent<Rigidbody2D> ().transform.position = playerPos;
		addMopped (playerPos);
		moving = false;
	}

	void addMopped(Vector3 newPos){
		while (posList.Count > 1) {
			Vector3 statonaryPos = posList [0];
			Instantiate (postMop, (statonaryPos), Quaternion.identity);
			posList.Remove (statonaryPos);
			mopped++;

			GameObject newMopped = GameObject.Find("lavaSquare(Clone)");
			newMopped.name=("Lava " + statonaryPos);
			newMopped.transform.parent = moppedSquares.transform;

			GameObject gc = GameObject.Find("GameController");		
			gameController levelScript = gc.GetComponent<gameController>();
			levelScript.mopped++;
		}
	}
}
