using UnityEngine;
using System.Collections;

public class LightEmitter : MonoBehaviour {
    public float chaseDistance = 1.4f;
    public float pulseChaseDistance = 3f;

	public float getDistance()
    {
        return chaseDistance;
    }

    public float getPulseDistance()
    {
        return pulseChaseDistance;
    }
}
