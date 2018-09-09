using UnityEngine;
using System.Collections;

// Interface for a Controller
// Available controllers are MainController for the player and

// USAGE: controller interface for enemies or NPCs
public interface ControllerIntf {

    // return the affiliation of this entity
    NocturneDefinitions.Faction getFaction();

    // set the affiliation of this entity
    void setFaction(NocturneDefinitions.Faction faction);

    // return the name of this entity;
    string getName();

    // instruct the entity to pause
    void pause();

    // unpause entity
    void unpause();

    // return the current target of the entity, if it has one
    // the player will always return null
    GameObject getTarget();

    // set the target of the entity 
    // NOTE: will do nothing for a player entity
    void setTarget(GameObject newTarget);
}
