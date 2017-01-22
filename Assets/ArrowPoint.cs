using UnityEngine;
using System.Collections;

public class ArrowPoint : MonoBehaviour {
    public GameObject target;
    public Color color;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //NocturneDefinitions.DrawLine(transform.position, target.transform.position, Color.green, 0.02f, 0.01f);
        if(target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        Vector3 diff = target.transform.position - transform.position;
        this.transform.rotation = Quaternion.Euler(0,0,-90 + Mathf.Atan2(diff.y, diff.x) * 180 / Mathf.PI);
        Color newColor = color;
        float dist = Vector3.Distance(target.transform.position, transform.position);
        if (dist > 8f)
        {
            newColor.a = 0.4f;
        }
        else
        {
            newColor.a = 0.4f + (1 - dist / 8f) * 0.6f;
        }
        setColor(newColor);
	}

    public void setColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}
