using UnityEngine;
using System.Collections;

public class GameEventHandler : MonoBehaviour {

	public void callEvent(string eventName)
    {
        switch(eventName)
        {
            case "print":
                printTest();
                break;
        }
    }

    private void printTest()
    {
        print("printTest called!");
    }
}
