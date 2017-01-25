using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.ImageEffects;

// Script to accept user input and move a given object appropriately
// Eg, when you press up or right, this script is what captures that
// and delivers the relevant commands to the MovementObj possessed by
// this object
public class MainController : MonoBehaviour, ControllerIntf {
    public bool inactive;
    private string objectName = "Player";
    private MovementObj obj;
    public GameObject sparkle;
    public bool canMove = true;
    private float cantMoveTimer = 0;
    private float cantMoveTimerfull = 0.2f;
    public float moveSpeed;
    public float rotationSpeed;
    private float sparkleTimer = 0;
    private float sparkleTimerMax = 0.1f;
    private float deathTimer = 0;
    private float deathTimerFull = 1f;
    private bool isDying = false;
    private Color regularColor;

    // Use this for initialization
    void Start()
    {
        regularColor = GetComponent<SpriteRenderer>().color;
        obj = GetComponent<MovementObj>();
        obj.setMovementSpeed(moveSpeed);
        obj.setRotationSpeed(rotationSpeed);
        GameObject.Find("GameLogic").GetComponent<GameEventHandler>()
            .playMusic();
    }

    private void turnTowardsCursor(Vector3 targetPosition)
    {
        Vector3 diff = targetPosition - transform.position;
        //print(diff);
        float targetAngle = Mathf.Atan(diff.y / diff.x) * 180 / Mathf.PI + 90;
        if (diff.x > 0)
            targetAngle = 180 + targetAngle;
        targetAngle = (int)targetAngle;
        float objAngle = transform.rotation.eulerAngles.z;
        if (Math.Abs(objAngle - targetAngle) >= 5)
        {
            if (NocturneDefinitions.quickestRotation(objAngle, targetAngle))
            {
                obj.rotate(1f);
            }
            else
            {
                obj.rotate(-1f);
            }
        }
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
                sparkleTimer += Time.deltaTime;
                if (sparkleTimer >= sparkleTimerMax)
                {
                    genSparkles();
                }
            }
            else
            {
                obj.brake();
            }

            if (horizontal != 0)
            {
                obj.rotate(horizontal);
            }
            
            int androidMode = UnityEngine.PlayerPrefs.GetInt("androidMode");
            if (androidMode == 1)
            {
                if (Input.touchCount > 0)
                {
                    // Get movement of the finger since last frame
                    Vector3 touchPos = Input.GetTouch(0).position;
                    Vector3 targetPosition = new Vector3(touchPos.x, touchPos.y,
            touchPos.z);
                    targetPosition.z = 10.0f;
                    targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);
                    targetPosition.z = transform.position.z;
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        turnTowardsCursor(targetPosition);
                        if (Vector3.Distance(transform.position, targetPosition) >= 2f)
                        {
                            obj.move(1);
                        }
                    }
                }
                if (Input.touchCount > 1)
                {
                    // Get movement of the finger since last frame
                    Vector3 touchPos = Input.GetTouch(1).position;
                    Vector3 targetPosition = new Vector3(touchPos.x, touchPos.y,
            touchPos.z);
                    targetPosition.z = 10.0f;
                    targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);
                    targetPosition.z = transform.position.z;
                    if (Vector3.Distance(transform.position, targetPosition) >= 2f)
                    {
                        GameObject.Find("GameLogic").GetComponent<GameEventHandler>()
                            .fireLaserSound();
                        FiringModuleIntf mod = GetComponent<LaserFiringModule>();
                        mod.fire();
                    }
                    else
                    {
                        FiringModuleIntf mod = GetComponent<RingFiringModule>();
                        GameObject.Find("GameLogic").GetComponent<GameEventHandler>()
                            .fireRing();
                        mod.fire();
                    }
                }
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
            FiringModuleIntf mod = GetComponent<RingFiringModule>();
            GameObject.Find("GameLogic").GetComponent<GameEventHandler>()
                .fireRing();
            mod.fire();
        }
        if(Input.GetButtonDown("Fire2"))
        {
            //DialogueManager dMan = GameObject.Find("GameLogic").
            //    GetComponent<DialogueManager>();
            //dMan.setCharacter(new CutterTheMerchant());
            GameObject.Find("GameLogic").GetComponent<GameEventHandler>()
                .fireLaserSound();
            FiringModuleIntf mod = GetComponent<LaserFiringModule>();
            mod.fire();
            /*
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

            GameObject[] list = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject obj in list)
            {
                obj.GetComponent<BasicEnemyController>().setPulse();
            }

            //GameObject.Find("Main Camera").GetComponent<Camera>()
            //    .GetComponent<Bloom>().bloomIntensity *= 0.9f;
            */
        }

        if(isDying)
        {
            deathTimer += Time.deltaTime;
            GetComponent<SpriteRenderer>().color = regularColor * (1f - deathTimer / deathTimerFull);
            GameObject.Find("Main Camera").GetComponent<UnityStandardAssets.ImageEffects.Bloom>().bloomIntensity =  (1f - deathTimer / deathTimerFull);
        }
        if (deathTimer >= deathTimerFull)
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void genSparkles()
    {
        for (int i = 0; i <= Mathf.FloorToInt(UnityEngine.Random.Range(0.5f, 1.5f)); i++)
        {
            sparkleTimer = 0;
            Vector3 pos = transform.position;
            float randRange = 0.15f;
            Vector3 randVar = new Vector3(UnityEngine.Random.Range(-randRange, randRange),
                                        UnityEngine.Random.Range(-randRange, randRange));
            GameObject spark = (GameObject)Instantiate(sparkle, pos + randVar, Quaternion.Euler(0, 0, 0));
            spark.GetComponent<Sparkle>().spinrate = UnityEngine.Random.Range(2, 8);
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
        /*
        setCantMove();

        Vector3 vel = GetComponent<Rigidbody2D>().velocity;

        if(col.name == "wallLeftSide" || 
            col.name == "wallRightSide")
        {
            //vel.x *= -0.2f;
            //GetComponent<Rigidbody2D>().velocity = vel;
            print(this.transform.rotation.eulerAngles.z);
            Vector3 thisAngle = this.transform.rotation.eulerAngles;
            float newZ = 360 - thisAngle.z;
            this.transform.rotation = Quaternion.Euler(0,0,newZ);
        }

        if (col.name == "wallUpperSide" ||
            col.name == "wallLowerSide")
        {
            //vel.y *= -0.2f;
            //GetComponent<Rigidbody2D>().velocity = vel;
            print(this.transform.rotation.eulerAngles.z);
            Vector3 thisAngle = this.transform.rotation.eulerAngles;
            float newZ = 180 - thisAngle.z;
            this.transform.rotation = Quaternion.Euler(0, 0, newZ);
        }
        */
        if (col.gameObject.tag == "BasicEnemy" ||
            col.gameObject.tag == "HunterEnemy")
        {
            isDying = true;
            canMove = false;
        }
    }
}
