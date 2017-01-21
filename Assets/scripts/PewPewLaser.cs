using UnityEngine;
using System.Collections;

/*
* This is the stereotype ship laser
* It simply moves in a straight line and
* damages whatever it hits
*/
public class PewPewLaser : Particle
{
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        if (!active)
            return;
        handleLifetime();
    }
}
