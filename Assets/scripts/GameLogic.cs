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
		visible = true;
		switchVisible ();
		button.onClick.AddListener (switchVisible);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void switchVisible() {
		GameObject[] wallList = GameObject.FindGameObjectsWithTag ("Wall");

		foreach (GameObject wall in wallList) {
			SpriteRenderer thisWall = wall.GetComponent<SpriteRenderer> ();
			if (!visible) {
				thisWall.color = testColor;
			} else {
				thisWall.color = gameColor;
			}
		}

		visible = !visible;
	}



}
