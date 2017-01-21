using UnityEngine;
using System.Collections;
using System;

// Script to accept user input and move a given object appropriately
// Eg, when you press up or right, this script is what captures that
// and delivers the relevant commands to the MovementObj possessed by
// this object
public class MainController : MonoBehaviour, ControllerIntf {
    public bool inactive;
    private string objectName = "Player";
    private MovementObj obj;
    public GameObject ring;
    public GameObject laser;
    public bool canMove = true;
    private float cantMoveTimer = 0;
    private float cantMoveTimerfull = 0.2f;
    public float projectileSpeed = 0.2f;

    // Use this for initialization
    void Start()
    {
        obj = GetComponent<MovementObj>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (canMove)
        {
            if (vertical != 0)
            {
                obj.move(vertical);
            }
            else
            {
                obj.brake();
            }

            if (horizontal != 0)
            {
                obj.rotate(horizontal);
            }
        }
        else
        {
            cantMoveTimer += Time.deltaTime;
            if(cantMoveTimer >= cantMoveTimerfull)
            {
                canMove = true;
                cantMoveTimer = 0;
            }
        }
        if(Input.GetButtonDown("Fire1"))
        {
            GameObject obj = (GameObject)Instantiate(ring, transform.position, Quaternion.Euler(0, 0, 0));
            obj.transform.localScale *= 0.5f;
        }
        if(Input.GetButtonDown("Fire2"))
        {
            Vector3 vec;
            Rigidbody2D proj;
            Vector3 temp;
            vec = new Vector3(0, (float)0.2, 0);
            vec = transform.rotation * vec;
            proj = (Rigidbody2D)Instantiate(laser.GetComponent<Rigidbody2D>(),
                transform.position + vec, transform.rotation);
            proj.transform.parent = transform.parent;
            temp = new Vector3(projectileSpeed *
                (vec.x), projectileSpeed *
                (vec.y), 0);
            proj.velocity = temp;

            proj.transform.position = gameObject.transform.position;
        }
    }

    public void setCantMove()
    {
        canMove = false;
    }

    // The player is of the Player faction
    NocturneDefinitions.Faction ControllerIntf.getFaction()
    {
        return NocturneDefinitions.Faction.Player;
    }

    // the player's name is just "Player"
    string ControllerIntf.getName()
    {
        return objectName;
    }


    // the player never has a set target
    GameObject ControllerIntf.getTarget()
    {
        return null;
    }

    // you cannot change the affiliation of the player
    void ControllerIntf.setFaction(NocturneDefinitions.Faction faction)
    {
        return;
    }

    // the player cannot have a set target
    void ControllerIntf.setTarget(GameObject newTarget)
    {
        return;
    }

    void ControllerIntf.pause()
    {
        throw new NotImplementedException();
    }


    void ControllerIntf.unpause()
    {
        throw new NotImplementedException();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        setCantMove();
        GetComponent<Rigidbody2D>().velocity *= -0.2f;
    }
}
