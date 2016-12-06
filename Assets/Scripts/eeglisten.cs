using UnityEngine;
using System.Collections;

public class eeglisten : MonoBehaviour {
	player player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("bird").GetComponent<player>();

	}
	
	// Update is called once per frame
	void Update () {
		Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		if (screenPosition.y > Screen.height || screenPosition.y < 0)
		{
			this.gameObject.SetActive (false);
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.CompareTag ("spike")) {
			Debug.Log ("hit spike");
			if (other.transform.position.x > -5.3) {
				player.score += 1;
				Debug.Log ("Early");
			} else if (other.transform.position.x < -5.6) {
				player.score += 1;
				Debug.Log ("Late");
			} else {
				player.score += 5;
				Debug.Log ("perfect");
			}
			this.gameObject.SetActive (false);
			other.gameObject.transform.parent.GetChild(1).gameObject.SetActive (false);
			other.gameObject.transform.parent.GetChild(2).gameObject.SetActive (false);

		}

	}
}
