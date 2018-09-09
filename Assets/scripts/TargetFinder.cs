using UnityEngine;
using System.Collections;
using System;

// USAGE: Component used to finds nearby light emitter (target)
public class TargetFinder : MonoBehaviour {

    public GameObject GetClosestLight()
    {
        GameObject closest = null;
            GameObject[] list = GameObject.FindGameObjectsWithTag("LightEmitter");
            foreach (GameObject obj in list)
            {
                if (closest == null || !gameObject.Equals(obj))
                    closest = obj;

                if(closest)
                    if (Vector3.Distance(transform.position, obj.transform.position) <=
                        Vector3.Distance(transform.position, closest.transform.position))
                    {
                        closest = obj;
                    }
            }
        if (gameObject.Equals(closest)) return null;
        return closest;
    }

    public GameObject GetClosestEnemy()
    {
        GameObject closest = null;
        GameObject[] list = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in list)
        {
            if (closest == null || !gameObject.Equals(obj))
                closest = obj;

            if (closest)
                if (Vector3.Distance(transform.position, obj.transform.position) <=
                    Vector3.Distance(transform.position, closest.transform.position))
                {
                    closest = obj;
                }
        }
        if (gameObject.Equals(closest)) return null;
        return closest;
    }

    public GameObject getTarget()
    {
        GameObject obj = GetClosestLight();
        if (obj)
            return obj;
        return null;
    }
}
