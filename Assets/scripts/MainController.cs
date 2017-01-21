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
    void ControllerIntf.setTarget()
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
}
