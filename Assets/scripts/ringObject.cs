using UnityEngine;
using System.Collections;

public class ringObject : MonoBehaviour {
    float i = 0;
    public float lifetime;
    public float rate;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        i++;
        transform.localScale *= rate;
        if (i >= lifetime)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, Color.clear, i/200);
        }
        if(i >= lifetime+20)
            DestroyObject(gameObject);
        if(i >= lifetime/2)
        {
            TargetFinder finder = null;
            finder = GetComponent<TargetFinder>();
            if(finder != null)
            {
                GameObject[] list = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject obj in list)
                {
                    obj.GetComponent<BasicEnemyController>().setPulse();
                }
            }
        }
    }
}
