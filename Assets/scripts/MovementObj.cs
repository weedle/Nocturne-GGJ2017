using UnityEngine;
using System.Collections;

// This is the component that interprets commands to move the entity
// Eg, if asked to "move" by the Controller of this entity, this 
// script will take that command and move the entity accordingly
public class MovementObj : MonoBehaviour {
    private float moveSpeed = 1;
    private float rotationSpeed = 5;

    // Use this for initialization
    void Start ()
    {
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void brake()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = rigidbody.velocity * (float)0.40;
    }

    public void move(float vertical)
    {
        Vector2 temp = new Vector2(Mathf.Cos(getAngle()),
   Mathf.Sin(getAngle()));
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        if (vertical != 0)
            rigidbody.velocity = temp * moveSpeed * (vertical + Mathf.Sign(vertical));
        temp = Vector3.zero;
    }

    public void rotate(float horizontal)
    {
        Vector3 temp = new Vector3(0, 0, -1 * rotationSpeed * horizontal);
        if (horizontal != 0)
            gameObject.transform.Rotate(temp);
        temp = Vector3.zero;
    }

    public float getAngle()
    {
        return (transform.rotation.eulerAngles.z + 90) * Mathf.PI / 180;
    }

    public void setTrigger(bool isTrigger)
    {
        if(gameObject.GetComponent<BoxCollider2D>() == null)
            gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<BoxCollider2D>().isTrigger = isTrigger;
    }

    public void setMovementSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void setRotationSpeed(float newSpeed)
    {
        rotationSpeed = newSpeed;
    }
}
