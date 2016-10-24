using UnityEngine;
using System.Collections;

public class rock : MonoBehaviour {

	// Use this for initialization
	public Vector2 velocity = new Vector2(-30, 0);
	//public float range = 2;

	// Use this for initialization
	void Start()
	{
		GetComponent<Rigidbody2D>().velocity = velocity;
		//transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - range * Random.value, transform.localScale.z);
	}
}
