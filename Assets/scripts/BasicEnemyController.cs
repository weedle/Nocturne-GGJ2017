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
    private float lightTimer = 0;
    private float lightTimerMax = 0.2f;
    private bool lightFound = false;

    // Use this for initialization
    void Start()
    {
        obj = GetComponent<MovementObj>();
        obj.setTrigger(true);
        target = null;
        gameObject.AddComponent<TargetFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pulseFound)
        {
            GameObject potentialTarget = GetComponent<TargetFinder>().getTarget();
            //print(potentialTarget.name + " " + Vector3.Distance(transform.position, potentialTarget.transform.position));
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
        if(lightFound)
        {
            lightTimer += Time.deltaTime;
            drawPlus();
            if (lightTimer >= lightTimerMax)
            {
                lightTimer = 0;
                lightFound = false;
            }
            /*
            Light light = GetComponent<Light>();
            light.color = new Color(light.color.r * (1 - lightTimer / lightTimerMax), light.color.g, light.color.b,
                                    light.color.g);
            if(lightTimer >= lightTimerMax)
            {
                lightTimer = 0;
                lightFound = false;
                light.enabled = false;
                print("disabled light");
            }
            */
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

    public void OnTriggerEnter2D(Collider2D col)
    {

        lightFound = true;
        /*
        Light light = GetComponent<Light>();
        light.enabled = true;
        light.color = new Color(10, 0, 0, 10);
        lightFound = true;
        */
    }

    private void drawPlus()
    {
        Color flash = Color.red;
        flash.r -= 0.5f;
        Vector3 start = transform.position;
        Vector3 end = transform.position;
        Vector3 randVariation = UnityEngine.Random.insideUnitCircle * 0.02f;
        float width = 10f;
        start += randVariation;
        end += randVariation;
        start.x -= width;
        end.x += width;
        NocturneDefinitions.DrawLine(start, end, flash);
        start = transform.position;
        end = transform.position;
        start += randVariation;
        end += randVariation;
        start.y -= width;
        end.y += width;
        NocturneDefinitions.DrawLine(start, end, flash);
    }
}
