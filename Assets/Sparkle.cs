using UnityEngine;
using System.Collections;

public class Sparkle : Particle
{
    public float spinrate = 8;

    /*
	 * regulates particle movement (rotation) and handles the particle lifetime
	 */
    void Update()
    {
        if (!active)
            return;
        handleLifetime();
        transform.Rotate(new Vector3(0, 0, spinrate));
    }
}
