using UnityEngine;
using System.Collections;

public class GameEventHandler : MonoBehaviour {
    public AudioClip clipLaser;
    public AudioClip clipSonar;
    public AudioClip clipMusic;
    bool soundLoaded = false;
    AudioSource src;

    void Start()
    {
        src = GetComponent<AudioSource>();
    }

    public void fireLaserSound()
    {
        src.PlayOneShot(clipLaser);
    }

    public void fireRing()
    {
        src.PlayOneShot(clipSonar);
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
}
