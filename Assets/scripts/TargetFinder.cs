using UnityEngine;
using System.Collections;
using System;

// TargetFinder for this project finds us a nearby Light Emitter
public class TargetFinder : MonoBehaviour {

    private GameObject GetClosestLight()
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

    public GameObject getTarget()
    {
        GameObject obj = GetClosestLight();
        if (obj)
            return obj;
        return null;
    }
}
