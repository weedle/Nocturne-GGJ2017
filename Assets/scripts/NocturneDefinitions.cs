using UnityEngine;
using System.Collections;

// USAGE: common definitions used throughout the project
public class NocturneDefinitions {

    // The affiliation of a given entity
	public enum Faction
    {
        Player, NPC, Enemy
    }

    
    // find quickest path for thing at angle1 to reach angle2
    // if true, turn clockwise, otherwise turn counterclockwise
    public static bool quickestRotation(float angle1, float angle2)
    {
        if (angle1 > 180)
        {
            if (angle2 > angle1 ||
                (angle2 < angle1 - 180))
                return false;
            else
                return true;
        }
        else
        {
            if (angle2 > angle1 &&
                (angle2 < angle1 + 180))
                return false;
            else
                return true;
        }
    }

    // USAGE: Draw a line! Parameters are self-explanatory
    // IT'S SELF-DOCUMENTING CODE :DDD
    public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.1f, float width = 0.04f)
    {
        GameObject myLine = new GameObject();

        myLine.layer = SortingLayer.GetLayerValueFromName("Default2");  // WARNING: do we actually have a layer called "Default2" ???
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));

        lr.startColor = lr.endColor = color;
        lr.startWidth = lr.endWidth = width;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        if (duration != 0)
            GameObject.Destroy(myLine, duration);
    }

	// Use the DrawLine method to draw a square         // WARNING: currently not used anywhere ... 
	public static void DrawSquare(Vector3 bottomLeft, Vector3 topRight, Color color, float duration = 0.2f, float width = 0.075f)
	{
		Vector3 bottomRight = new Vector3(topRight.x, bottomLeft.y);
		Vector3 topLeft = new Vector3(bottomLeft.x, topRight.y);
		NocturneDefinitions.DrawLine(bottomLeft, bottomRight, color, duration, width);
		NocturneDefinitions.DrawLine(bottomLeft, topLeft, color, duration, width);
		NocturneDefinitions.DrawLine(topRight, bottomRight, color, duration, width);
		NocturneDefinitions.DrawLine(topRight, topLeft, color, duration, width);
	}

}
