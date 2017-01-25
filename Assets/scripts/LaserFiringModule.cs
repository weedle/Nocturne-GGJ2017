using UnityEngine;
using System.Collections;

public class LaserFiringModule : MonoBehaviour, FiringModuleIntf
{

    private GameObject laser;
    public float projectileSpeed = 20f;

    void FiringModuleIntf.fire()
    {
        laser = GameObject.Find("GameLogic").GetComponent<PrefabHost>()
            .getLaserObj();
        laser.transform.position = transform.position;

        Rigidbody2D proj;
        Vector3 temp;
        Vector3 vec = new Vector3(0, (float)0.2, 0);
        vec = transform.rotation * vec;
        proj = laser.GetComponent<Rigidbody2D>();
        proj.transform.parent = transform.parent;
        temp = new Vector3(projectileSpeed *
            (vec.x), projectileSpeed *
            (vec.y), 0);
        proj.velocity = temp;

        proj.transform.position = gameObject.transform.position;
        proj.transform.rotation = gameObject.transform.rotation;

        GameObject[] list = GameObject.FindGameObjectsWithTag("BasicEnemy");
        foreach (GameObject obj in list)
        {
            obj.GetComponent<BasicEnemyController>().setPulse();
        }

        //GameObject.Find("Main Camera").GetComponent<Camera>()
        //    .GetComponent<Bloom>().bloomIntensity *= 0.9f;
    }

    bool FiringModuleIntf.canFire()
    {
        return true;
    }
}
