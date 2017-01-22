using UnityEngine;
using System.Collections;
using System;

public class RingFiringModule : MonoBehaviour, FiringModuleIntf
{
    private GameObject ring;
    public float projectileSpeed = 0.2f;

    void FiringModuleIntf.fire()
    {
        ring = GameObject.Find("GameLogic").GetComponent<PrefabHost>()
            .getRingObj();
        ring.transform.position = transform.position;
    }

    bool FiringModuleIntf.canFire()
    {
        return true;
    }
}
