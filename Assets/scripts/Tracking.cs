using UnityEngine;
using System.Collections;

public class Tracking : MonoBehaviour {
    public GameObject target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = new Vector3(target.transform.position.x,
            target.transform.position.y, gameObject.transform.position.z);
	}
}
