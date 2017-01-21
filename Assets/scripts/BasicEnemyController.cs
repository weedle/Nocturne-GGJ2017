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
    private float chaseDistance = 1f;
    private float pulseTimer = 0;
    private float pulseTimerMax = 1.2f;
    private bool pulseFound = false;

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
        if (!pulseFound)
        {
            GameObject potentialTarget = GetComponent<TargetFinder>().getTarget();
            if (Vector3.Distance(transform.position, potentialTarget.transform.position) <= chaseDistance)
            {
                target = potentialTarget;
                turnTowardsEnemy();
                obj.move(1);
            }
            else
            {
                target = null;
                obj.brake();
            }
        }
        else
        {
            pulseTimer += Time.deltaTime;
            if (pulseTimer >= pulseTimerMax)
            {
                pulseFound = false;
                pulseTimer = 0;
                target = null;
            }
            GameObject potentialTarget = GetComponent<TargetFinder>().getTarget();
            if (Vector3.Distance(transform.position, potentialTarget.transform.position) <= 2*chaseDistance)
            {
                target = potentialTarget;
                turnTowardsEnemy();
                obj.move(1);
            }
            else
            {
                target = null;
                obj.brake();
            }
        }
    }

    private void turnTowardsEnemy()
    {
        Vector3 diff = target.transform.position - transform.position;
        //print(diff);
        float targetAngle = Mathf.Atan(diff.y / diff.x) * 180 / Mathf.PI + 90;
        if (diff.x > 0)
            targetAngle = 180 + targetAngle;
        targetAngle = (int)targetAngle;
        float objAngle = transform.rotation.eulerAngles.z;

        if (NocturneDefinitions.quickestRotation(objAngle, targetAngle))
        {
            obj.rotate(1f);
        }
        else
        {
            obj.rotate(-1f);
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

    public void setPulse()
    {
        pulseFound = true;
    }
}
