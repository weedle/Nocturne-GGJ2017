using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class handler : MonoBehaviour {
	public GameObject dialogueText;
	private UnityEngine.UI.Text textThing;
	private int globalPos = 0;
    private int linePos = 0;
	private float currentTime = 0;
	private float speed1 = 0.8f; 		// use values between 0.2 - 1.0, with 1.0 being the max speed
    private float speed2 = 2.4f;
    private float currSpeed = 0.8f;
    private int lineLength = 60;
    private int whichLine = 1; 
    private int linesMax = 3;
    private int currCallbackType = 0;
    private int currentTrack = 0;
    private string assignment; // the text we are currently displaying in the box
    private bool newAssignment; // this is used to prevent next skipping us ahead when we aren't done with the text
    private Character chr;

    void Start () {
        //print("calling init from start");
        //init();
    }

    public void init()
    {
        textThing = dialogueText.GetComponent<UnityEngine.UI.Text>();
        textThing.text = "";
        assignment = "";
    }

    public void setCharacter(Character character)
    {
        resetBox();
        if (chr == null || !character.name.Equals(chr.name))
        {
            chr = character;
        }
        else
        {
            chr.handleIndex();
        }
        assignment = chr.getRegularDialogue();
        currCallbackType = chr.getCallbackType();
        currentTrack = 1;
        newAssignment = true;
    }

    // Update is called once per frame
    void Update() {
        if (newAssignment)
        {
            printer();
        }
    }


	private void printer(){
        // track time
        currentTime += currSpeed * Time.deltaTime;

        // enough time has passed to try and print another letter
        // use 'while' so if we're going really fast we can print multiple letters
        while (currentTime > 0.01 && globalPos < assignment.Length)
        {
            // reset timer in preparation for next letter
            currentTime -= 0.01f;
            // if we're at a space
            if (assignment.Substring(globalPos, 1).Equals(" "))
            {
                // get the remaining letters
                string rest = assignment.Substring(globalPos,
                        assignment.Length - globalPos);
                string[] restSplit = rest.Split(' ');

                // if we have another word coming up
                if (restSplit.Length > 0)
                {
                    // get the number of chars needed to print next word
                    int next = restSplit[1].Length + 1;

                    // if we don't have enough space, print a newline
                    if (linePos + next >= lineLength)
                    {
                        // we're one letter forward but at the beginning of a new line
                        textThing.text += " \n";
                        linePos = 0;
                        globalPos++;
                        whichLine++;
                    }
                }
            }

            // check if we're currently out of view of the dialogue box
            if (whichLine <= linesMax)
            {
                // if not, add a new line and keep going
                string text = assignment.Substring(globalPos, 1);
                if (text.Equals("/"))
                {
                    linePos = -1;
                    whichLine++;
                    textThing.text += "\n";
                }
                else
                {
                    textThing.text += text;
                }
                if (text == "\n")
                    whichLine++;
                globalPos++;
                linePos++;
            }
            else
            {
                // we're finished with this box's worth of text
                newAssignment = false;
            }
        }
        if (globalPos == assignment.Length)
        {
            // we're out of letters, so still done with this box's worth of text
            newAssignment = false;
        }
    }

    // if the box is full but we have more text, reset box and prepare
    // to display remaining text.
    // if we're done, do nothing.
    // speed up the text display rate
    public void speedUp()
    {
        if (assignment == "")
        {
            return;
        }
        if (!newAssignment && globalPos < assignment.Length)
        {
            assignment = assignment.Substring(globalPos,
                assignment.Length - globalPos);
            // reset this to say we have a new set of text for the dialogue box
            newAssignment = true;
            resetBox();
        }
        else
        {
            if(!newAssignment)
            {
                GameObject.Find("GameLogic")
                .GetComponent<DialogueManager>().
                toggleAllComps(false);
                if(currCallbackType == currentTrack)
                {
                    if(currentTrack != 0)
                        GameObject.Find("GameLogic").
                            GetComponent<GameEventHandler>().
                            callEvent(chr.getCallback());
                }
            }
            else
                currSpeed = speed2;
        }
    }

    // if yes, do this
    // this should be updated to retrieving the correct data
    public void yesButton()
    {
        if (assignment == "")
            return;
        assignment = chr.getYesDialogue();
        newAssignment = true;
        currentTrack = 2;
        resetBox();
    }

    public void noButton()
    {
        if (assignment == "")
            return;
        assignment = chr.getNoDialogue();
        newAssignment = true;
        currentTrack = 3;
        resetBox();
    }

    // reset progress within dialogue box
    // also reset typing speed and clear existing text
    public void resetBox()
    {
        currSpeed = speed1;
        if (textThing == null)
            init();
        textThing.text = "";
        whichLine = 1;
        globalPos = 0;
        linePos = 0;
    }
}
