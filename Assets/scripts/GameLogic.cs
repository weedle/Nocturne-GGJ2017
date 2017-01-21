using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {
	public Button button;
	public Color testColor = Color.white;
	public Color gameColor = Color.black;
	private bool visible;

	// Use this for initialization
	void Start () {
		visible = false;
		button.onClick.AddListener (makeVisible);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void makeVisible() {
		GameObject[] wallList = GameObject.FindGameObjectsWithTag ("Wall");

		foreach (GameObject wall in wallList) {
			SpriteRenderer thisWall = wall.GetComponent<SpriteRenderer> ();
			if (visible) {
				thisWall.color = gameColor;
			} else {
				thisWall.color = testColor;
			}
		}

		visible = !visible;
	}
}
