using UnityEngine;
using System.Collections;

public class score : MonoBehaviour {
	public player player;
	public Sprite zero, one, two, three, four, five, six, seven, eight, nine;
	public Sprite[] sprites;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		sprites = new Sprite[]{zero, one, two, three, four, five, six, seven, eight, nine};
	}
	
	// Update is called once per frame
	void Update () {
		scorecal (player.score);
	}

	void scorecal(int score){
		this.gameObject.transform.GetChild (2).GetComponent<SpriteRenderer> ().sprite = sprites[score/100];
		this.gameObject.transform.GetChild (1).GetComponent<SpriteRenderer> ().sprite = sprites[score/10%10];
		this.gameObject.transform.GetChild (0).GetComponent<SpriteRenderer> ().sprite = sprites[score%10];
	}
}
