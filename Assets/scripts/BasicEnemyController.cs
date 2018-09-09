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
    private float pulseTimer = 0;
    private float pulseTimerMax = 2.0f;
    private bool pulseFound = false;

    private float lightTimer = 0;
    private float lightTimerMax = 0.1f;
    private bool lightFound = false;

    public float moveSpeed;
    public float rotationSpeed;
    public float boostedMoveSpeed;
    public float boostedRotationSpeed;

    public Color highlightColor;
    public Color regularColor;
    private float hunterTimer = 0;
    private float hunterTimerMax = 1.2f;

    // USAGE: initialize enemy controller
    void Start()
    {
        obj = GetComponent<MovementObj>();
        obj.setMovementSpeed(moveSpeed);
        obj.setRotationSpeed(rotationSpeed);
        obj.setTrigger(true);

        gameObject.AddComponent<TargetFinder>();
        
        target = null;
        regularColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = GameObject.Find("GameLogic").
            GetComponent<GameLogic>()
            .getGameColor();
    }

    // Update is called once per frame
    void Update()
    {   
        if (!pulseFound)        // detected player pulse
        {
            GameObject potentialTarget = GetComponent<TargetFinder>().getTarget();
            //print(potentialTarget.name + " " + Vector3.Distance(transform.position, potentialTarget.transform.position));
            if (potentialTarget != null)
            {
                float modifier = 1;
                if(gameObject.tag == "HunterEnemy")
                {
                    modifier = 3f;
                }
                if (Vector3.Distance(transform.position, potentialTarget.transform.position) <=
                    potentialTarget.GetComponent<LightEmitter>().getDistance() * modifier )
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
                obj.brake();
            }
        }
        else        // did not detect player pulse
        {
            pulseTimer += Time.deltaTime;
            if (pulseTimer >= pulseTimerMax)
            {
                pulseFound = false;
                pulseTimer = 0;
                target = null;
            }
            GameObject potentialTarget = GetComponent<TargetFinder>().getTarget();
            if (potentialTarget != null)
            {
                float modifier = 1;
                if (gameObject.tag == "HunterEnemy")
                {
                    modifier = 3f;
                }
                if (Vector3.Distance(transform.position, potentialTarget.transform.position) <= 
                    potentialTarget.GetComponent<LightEmitter>().getPulseDistance() * modifier)
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
                obj.brake();
            }
        }

        if(lightFound)
        {
            lightTimer += Time.deltaTime;
            drawPlus();
            if (lightTimer >= lightTimerMax)
            {
                obj.setMovementSpeed(moveSpeed);
                obj.setRotationSpeed(rotationSpeed);
                lightTimer = 0;
                lightFound = false;
                GetComponent<SpriteRenderer>().color = regularColor;
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
        if (gameObject.tag == "HunterEnemy")
        {
            if (target == null)
                return;
            if (Vector3.Distance(target.transform.position, transform.position) >= 
                target.GetComponent<LightEmitter>().getDistance() * 3f)
            {
                return;
            }
            hunterTimer += Time.deltaTime;
            if (hunterTimer >= hunterTimerMax)
            {
                if (UnityEngine.Random.Range(0, 10) <= 0.01)
                    boost();
                hunterTimer = 0;
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

    // TODO: pause enemy
    void ControllerIntf.pause()
    {
        throw new NotImplementedException();
    }

    // TODO: unpause enemy
    void ControllerIntf.unpause()
    {
        throw new NotImplementedException();
    }

    // TODO: should this function still be kept? (currently has 0 references)
    public void OnTriggerEnter2D(Collider2D col)
    {
        boost();
        /*
        Light light = GetComponent<Light>();
        light.enabled = true;
        light.color = new Color(10, 0, 0, 10);
        lightFound = true;
        */
    }


    // USAGE: changes enemy to "pursuit" state properties (i.e. faster movement, color change)
    public void boost()
    {
        lightFound = true;
        GetComponent<SpriteRenderer>().color = highlightColor;
        obj.setMovementSpeed(boostedMoveSpeed);
        obj.setRotationSpeed(boostedRotationSpeed);
    }

    /*
    // USAGE: emit "plus" / "cross" lighting pattern to indicate enemy location
    private void drawPlus()
    {
        Color flash = Color.red;
        //flash.r -= 0.5f;            // mutes red color ...
        Vector3 start = transform.position;
        Vector3 end = transform.position;

        Vector3 randVariation = UnityEngine.Random.insideUnitCircle * 0.02f; 
        float width = 10f;  // "length" of the line

        Debug.Log(string.Concat("start", start.ToString("F4")));
        Debug.Log(string.Concat("end", end.ToString("F4")));
        Debug.Log(string.Concat("variation", randVariation.ToString("F4")));

        // draw horizontal cross axis
        start += randVariation;
        end += randVariation;
        start.x -= width;
        end.x += width;
        NocturneDefinitions.DrawLine(start, end, flash);

        Debug.Log(string.Concat("start modified", start.ToString("F4")));
        Debug.Log(string.Concat("end modified", end.ToString("F4")));
        
        // draw vertical cross axis
        start = transform.position;
        end = transform.position;
        start += randVariation;
        end += randVariation;
        start.y -= width;
        end.y += width;
        NocturneDefinitions.DrawLine(start, end, flash);
    }
    */

    // USAGE: emit "plus" / "cross" lighting pattern to indicate enemy location
    // WARNING: modified version of drawPlus() above; needs to be checked
    private void drawPlus(){
        Color flash = Color.red;
        float axisWidth = 10f;

        Vector3 axisStart = transform.position;
        Vector3 axisEnd = transform.position;

        // draw horizontal cross axis
        axisStart.x -= axisWidth;
        axisEnd.x += axisWidth;
        NocturneDefinitions.DrawLine(axisStart, axisEnd, flash);

        // draw vertical cross axis
        axisStart = axisEnd = transform.position;
        axisStart.y -= axisWidth;
        axisEnd.y += axisWidth;
        NocturneDefinitions.DrawLine(axisStart, axisEnd, flash);
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

    // NOTE: you cannot change the affiliation of the Basic Enemy
    void ControllerIntf.setFaction(NocturneDefinitions.Faction faction)
    {
        return;
    }

    // assign this enemy a specific target
    void ControllerIntf.setTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    public void setPulse()
    {
        pulseFound = true;
    }

}
