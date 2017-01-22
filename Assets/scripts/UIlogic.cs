using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class UIlogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadScene(int index) {
		UnityEditor.SceneManagement.EditorSceneManager.LoadScene (index);
	}
}
