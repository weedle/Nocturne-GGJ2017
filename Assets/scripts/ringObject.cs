using UnityEngine;
using System.Collections;

public class ringObject : MonoBehaviour {
    private float lifetimeInitial;
    public float lifetime;
    public float lifetimeTimer;
    public float rate;
	// Use this for initialization
	void Start () {
        lifetimeInitial = lifetime;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale *= rate;
        lifetimeTimer += Time.deltaTime;
        if (lifetimeTimer >= 0.02)
        {
            lifetimeTimer = 0;
            lifetime--;
        }
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            lifetime--;
        }

        if(lifetime <= lifetimeInitial/2)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, Color.clear, lifetime/lifetimeInitial);
        }
        if(lifetime <= lifetimeInitial/4)
        {
            GameObject[] list = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject obj in list)
            {
                obj.GetComponent<BasicEnemyController>().setPulse();
                if(Vector3.Distance(transform.position, obj.transform.position) <= 1.4f)
                {
                    obj.GetComponent<BasicEnemyController>().boost();
                }
            }
        }
    }
}
