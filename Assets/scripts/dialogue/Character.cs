using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("DialogueCollection")]
public abstract class Character
{
    public int index = 0;

    public struct Dialogue
    {
        // what level to display this one
        [XmlAttribute("level")]
        public int level;

        // what to say to the player
        [XmlAttribute("DialogueRegular")]
        public string dialogueRegular;

        // what to say if the player responds "yes"
        // leave blank to not display yes/no
        [XmlAttribute("DialogueYes")]
        public string dialogueYes;

        // what to say if the player responds "no"
        [XmlAttribute("DialogueNo")]
        public string dialogueNo;

        // 0 for no callback
        // 1 for after dialogueRegular
        // 2 after dialogueYes
        // 3 after dialogueNo
        [XmlAttribute("CallbackType")]
        public int callbackType;

        // what function to call after dialogue
        // this is handled in GameLogic->GameEventHandler
        [XmlAttribute("Callback")]
        public string callback;
    };
		

    [XmlAttribute("name")]
    public string name = "";

    [XmlArray("Dialogues")]
    [XmlArrayItem("Dialogue")]
    public List<Dialogue> dialogues = new List<Dialogue>();

    //public string Name;
    //private string factionRaw;
    //public ShipDefinitions.Faction Affiliation;

    //public int level;
    //public int dialogueLevel;

    public abstract void initialize();

    public string getRegularDialogue()
    {
        return dialogues[index].dialogueRegular;
    }

    public string getYesDialogue()
    {
        return dialogues[index].dialogueYes;
    }
    public string getNoDialogue()
    {
        return dialogues[index].dialogueNo;
    }

    public string getName()
    {
        return name;
    }

    public int getCallbackType()
    {
        return dialogues[index].callbackType;
    }

    public string getCallback()
    {
        return dialogues[index].callback;
    }

    // Each character needs to handle the index value in their own special way
    public abstract void handleIndex();
}