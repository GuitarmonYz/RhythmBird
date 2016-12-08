using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO.Ports;



public class player : MonoBehaviour {

	// The force which is added when the player jumps
	// This can be changed in the Inspector window
	//public Vector2 jumpForce = new Vector2(0, 300);

	public Sprite bird0;
	public Sprite bird1;

	public Rigidbody2D bird ;
	private Vector2 start;
	public generate generate;
	public Rigidbody2D egg;

	public int score;
	//public Text counttext;
	//private Rigidbody2D tmpegg;

	public AudioClip jump;
	public AudioClip death;
	//public AudioSource background;
	public AudioClip spike;


	public AudioClip c;
	public AudioClip ee;
	public AudioClip g;
	public AudioClip b;
	AudioClip[] sourcearray;
	//private int count;
	//public text countText;
	private AudioSource source;
	private float vol = 2.5f;
	void Start(){
		if (LoadOnClick.bird == 0) {
			this.GetComponent<SpriteRenderer> ().sprite = bird0;
		} else {
			this.GetComponent<SpriteRenderer> ().sprite = bird1;
		}

		source = this.GetComponent<AudioSource> ();
		sourcearray = new AudioClip[]{c,ee,g,b};
		bird = GetComponent<Rigidbody2D> ();
		start = new Vector2 (transform.position.x, transform.position.y);
		//background.playOnAwake = true;
		//background.loop = true;
		score = 0;
		//counttext.text = "Score: " + score.ToString();

	}

	// Update is called once per frame
	void Update ()
	{


		if (Input.GetKeyDown("space"))
		{
			bird.velocity = new Vector2 (0, -50f);
			//bird.(new Vector2(0,-500));

		}
		if (Input.GetKeyDown ("s")) {
			Debug.Log ("pressed");
			Rigidbody2D newegg = Instantiate (egg);
			source.PlayOneShot (spike,1f);
			//tmpegg = newegg;
			newegg.velocity = new Vector2 (0, -30f);
		}
		Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		if (screenPosition.y > Screen.height || screenPosition.y < 0)
		{
			Die();
			source.PlayOneShot(death,1f);
		}
	}

    void BirdEggMotion()
    {
        Debug.Log("pressed");
        Rigidbody2D newegg = Instantiate(egg);
		source.PlayOneShot (spike,1f);
        //tmpegg = newegg;
        newegg.velocity = new Vector2(0, -50f);
    }

	void BirdShakeMotion(string svol)
    {
		vol = float.Parse (svol);
		if (vol > 2.5f)
			vol = 2.5f;
		if (vol < 1.5f)
			vol = 1.5f;
        bird.velocity = new Vector2(0, -50f);
    }

    void Die()
	{
		//Application.LoadLevel(Application.loadedLevel);

		score = 0;
		//counttext.text = "Score: " + score.ToString();
		source.PlayOneShot (death,1f);
		generate.die();
		bird.velocity = new Vector2 (0, 0);
		bird.MovePosition (start);


	}


	void OnTriggerEnter2D (Collider2D other){
		float sample = UnityEngine.Random.Range (0.0f,4.0f);
		if (other.gameObject.CompareTag ("board")) {
			//jump.Play();
			source.PlayOneShot(sourcearray[(int)sample],vol-1.5f);
			source.pitch = Random.Range (0.75f,1.5f);
			if (other.transform.position.x > -4.4) {
				
				//source.PlayOneShot(sourcearray[(int)sample],0.5f);
				score += 1;
				Debug.Log ("Early");
			} else if (other.transform.position.x < -5.3) {
				//source.PlayOneShot(sourcearray[(int)sample],0.5f);
				score += 1;
				Debug.Log ("Late");
			} else {
				//source.PlayOneShot(sourcearray[(int)sample],1f);
				score += 5;
				Debug.Log ("perfect");
			}
			//counttext.text = "score: " + score.ToString();
			StartCoroutine (SmoothMovement (start));

			bird.velocity = new Vector2 (0, 0);
			other.gameObject.transform.parent.GetChild(1).gameObject.SetActive (false);

			// count = count + 1;
			// countText.text = "Score: " + count.ToString();



		} 
		if (other.gameObject.CompareTag ("collider")) {
			Die();
			Debug.Log ("Die");
		}
		if (other.gameObject.CompareTag ("invi")) {
			other.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			StartCoroutine (SmoothMovement (start));
			bird.velocity = new Vector2 (0, 0);
		}


//		if (other.gameObject.CompareTag ("spike")) {
//			Debug.Log ("hit spike");
//			tmpegg.gameObject.SetActive (false);
//			other.gameObject.transform.parent.GetChild(2).gameObject.SetActive (false);
//
//		}

	}

	/*
	void OnCollisionEnter2D(Collision2D coll){
		Debug.Log ("Enter");
		if (coll.gameObject.CompareTag ("board")) {
			
			jump.Play();
			StartCoroutine (SmoothMovement (start));
			Debug.Log ("contact points; "+coll.contacts[0].point);
			Debug.Log ("block position: " + coll.transform.position);
			bird.velocity = new Vector2 (0, 0);
			coll.gameObject.transform.parent.GetChild(1).gameObject.SetActive (false);

			// count = count + 1;
			// countText.text = "Score: " + count.ToString();



		} 
		if (coll.gameObject.CompareTag ("collider")) {
			Die();
		}
	}
	*/

	private IEnumerator SmoothMovement (Vector3 end)
	{
		//Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
		//Square magnitude is used instead of magnitude because it's computationally cheaper.
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		//While that distance is greater than a very small amount (Epsilon, almost zero):
		while(sqrRemainingDistance > float.Epsilon)
		{
			//Find a new position proportionally closer to the end, based on the moveTime
			//Debug.Log(Time.deltaTime);
			Vector3 newPostion = Vector3.MoveTowards(bird.position, end, 50*Time.deltaTime);

			//Call MovePosition on attached Rigidbody2D and move it to the calculated position.
			bird.MovePosition (newPostion);

			//Recalculate the remaining distance after moving.
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;

			//Return and loop until sqrRemainingDistance is close enough to zero to end the function
			yield return null;
		}
	}

    void OnMessageArrived(string msg)
    {
        Debug.Log("Message arrived: " + msg);
		string[] msgs = msg.Split (' ');
		if (msgs[0]=="1")
        {
			BirdShakeMotion(msgs[1]);
        }
        else if (msg == "2")
        {
            BirdEggMotion();
        }
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }
}

