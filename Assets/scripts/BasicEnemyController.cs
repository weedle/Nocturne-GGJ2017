using UnityEngine;
using System.Collections;
using System;

// 
public class BasicEnemyController : MonoBehaviour, ControllerIntf
{
    public bool inactive;
    private string objectName = "BasicEnemy";
    private MovementObj obj;
    private GameObject target;

    // Use this for initialization
    void Start()
    {
        obj = GetComponent<MovementObj>();
        target = null;
        gameObject.AddComponent<TargetFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            GameObject potentialTarget = GetComponent<TargetFinder>().getTarget();
            if (Vector3.Distance(transform.position, potentialTarget.transform.position) <= 1)
            {
                target = potentialTarget;
                print(Vector3.Distance(transform.position, potentialTarget.transform.position));
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 1)
            {
                target = null;
                print("Lost target...");
            }
        }
    }

    // The basic enemy is of the Enemy faction
    NocturneDefinitions.Faction ControllerIntf.getFaction()
    {
        return NocturneDefinitions.Faction.Enemy;
    }

    // the basic enemy's name is "BasicEnemy"
    string ControllerIntf.getName()
    {
        return objectName;
    }

    // return the current target of this enemy
    GameObject ControllerIntf.getTarget()
    {
        return target;
    }

    // you cannot change the affiliation of the Basic Enemy
    void ControllerIntf.setFaction(NocturneDefinitions.Faction faction)
    {
        return;
    }

    // assign this enemy a specific target
    void ControllerIntf.setTarget(GameObject newTarget)
    {
        target = newTarget;
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
