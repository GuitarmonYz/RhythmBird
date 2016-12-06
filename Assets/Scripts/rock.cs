using UnityEngine;
using System.Collections;

public class rock : MonoBehaviour {

	// Use this for initialization

	//public float range = 2;
	public int bpm = 90;

	// Use this for initialization
	void Start()
	{
		float vx = (15.2f*bpm)/240.0f;
		//Debug.Log ("block speed"+vx);
		Vector2 velocity = new Vector2(-vx, 0);
		GetComponent<Rigidbody2D>().velocity = velocity;
		//transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - range * Random.value, transform.localScale.z);
	}
}
