using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour {
    private GameObject textbox;
    private GameObject nameBox;
    private GameObject yesButton;
    private GameObject noButton;
    private GameObject nextButton;

    handler hdr;

	// Use this for initialization
	void Start () {
        GameObject canvas = GameObject.Find("DialogueCanvas");
        foreach (Transform childT in
            canvas.GetComponentInChildren<Transform>())
        {
            switch(childT.gameObject.name)
            {
                case "textbox":
                    textbox = childT.gameObject;
                    break;
                case "namebox":
                    nameBox = childT.gameObject;
                    break;
                case "yesButton":
                    yesButton = childT.gameObject;
                    break;
                case "noButton":
                    noButton = childT.gameObject;
                    break;
                case "next":
                    nextButton = childT.gameObject;
                    break;
            }
        }
        hdr = textbox.GetComponent<handler>();
        toggleAllComps(false);
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonUp("Fire2"))
        {
        }
	}

    public void setCharacter(Character chr)
    {
        toggleAllComps(true);
        chr.initialize();
        nameBox.GetComponentInChildren<UnityEngine.UI.Text>().text
            = chr.getName();
        hdr.setCharacter(chr);
        //toggleYesNo(!chr.getYesDialogue().Equals(""));
    }

    public void toggleActivation()
    {
        toggleAllComps(!textbox.activeSelf);
    }

    public void toggleYesNo(bool toggle)
    {
        //yesButton.SetActive(toggle);
        //noButton.SetActive(toggle);
    }

    public void toggleAllComps(bool toggle)
    {
        textbox.SetActive(toggle);
        nameBox.SetActive(toggle);
        //noButton.SetActive(toggle);
        //yesButton.SetActive(toggle);
        //nextButton.SetActive(toggle);
    }
}
