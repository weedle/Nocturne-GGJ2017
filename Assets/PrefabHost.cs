using UnityEngine;
using System.Collections;

public class PrefabHost : MonoBehaviour {
    public GameObject ring;
    public GameObject laser;

    public GameObject getRingObj()
    {
        GameObject obj = (GameObject)Instantiate(ring,
            Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getLaserObj()
    {
        GameObject obj = (GameObject)Instantiate(laser,
            Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }
}
