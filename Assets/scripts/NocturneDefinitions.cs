using UnityEngine;
using System.Collections;

// Contains common definitions used throughout the project
public class NocturneDefinitions {

    // The affiliation of a given character
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

    // Draw a line! Parameters are self-explanatory
    // IT'S SELF-DOCUMENTING CODE :DDD
    public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.1f, float width = 0.04f)
    {
        GameObject myLine = new GameObject();
        myLine.layer = SortingLayer.GetLayerValueFromName("Default2");
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(width, width);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        if (duration != 0)
            GameObject.Destroy(myLine, duration);
    }
}
