using UnityEngine;
using System.Collections;

public class GameEventHandler : MonoBehaviour {
    public AudioClip clipLaser;
    public AudioClip clipSonar;
    public AudioClip clipMusic;
    bool soundLoaded = false;
    void Start()
    {

    }

    public void fireLaserSound()
    {
        GetComponent<AudioSource>().PlayOneShot(clipLaser);
    }

    public void fireRing()
    {
        GetComponent<AudioSource>().PlayOneShot(clipSonar);
    }

    public void playMusic()
    {
        GetComponent<AudioSource>().PlayOneShot(clipMusic);
    }

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

    public void playSound1()
    {
    }
}
