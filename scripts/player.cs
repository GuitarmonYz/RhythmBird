using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	// The force which is added when the player jumps
	// This can be changed in the Inspector window
	//public Vector2 jumpForce = new Vector2(0, 300);
	public Rigidbody2D bird ;
	private Vector2 start;
	public generate generate;

	void Start(){
		bird = GetComponent<Rigidbody2D> ();
		start = new Vector2 (transform.position.x, transform.position.y);
	}

	// Update is called once per frame
	void Update ()
	{
		// Jump
		if (Input.GetKeyUp("space"))
		{
			bird.velocity = new Vector2 (0, -30f);
		}
		Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		if (screenPosition.y > Screen.height || screenPosition.y < 0)
		{
			Die();
		}
	}

	void Die()
	{
		//Application.LoadLevel(Application.loadedLevel);
		generate.die();
		bird.velocity = new Vector2 (0, 0);
		bird.MovePosition (start);

	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.CompareTag ("board")) {

			StartCoroutine (SmoothMovement (start));

			bird.velocity = new Vector2 (0, 0);
			other.gameObject.transform.parent.GetChild(1).gameObject.SetActive (false);

		} 
		if (other.gameObject.CompareTag ("collider")) {
			Die();
		}
	}

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
}
