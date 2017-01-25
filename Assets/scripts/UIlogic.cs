using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIlogic : MonoBehaviour {
    string prefix = "COL PRESET:";

    void Start()
    {

        int i = UnityEngine.PlayerPrefs.GetInt("colorPreset");
        Color gameColor = getColor(i);
        GameObject obj = GameObject.Find("colorButton");
        if(obj != null)
        {
            obj.GetComponentInChildren<UnityEngine.UI.Text>().text
                = prefix + i;
            UnityEngine.UI.ColorBlock blk;
            blk = obj.GetComponentInChildren<UnityEngine.UI.Button>().colors;
            blk.normalColor = gameColor;
            obj.GetComponentInChildren<UnityEngine.UI.Button>().colors = blk;
        }
    }

    public void loadScene(int index) {
		UnityEngine.SceneManagement.SceneManager.LoadScene (index);
	}

    public void setColor()
    {
        int i = UnityEngine.PlayerPrefs.GetInt("colorPreset");
        switch (i)
        {
            case 0:
                i = 1;
                GameObject.Find("colorButton").
                    GetComponentInChildren<UnityEngine.UI.Text>().text
                    = prefix + i;
                break;
            case 1:
                i = 2;
                GameObject.Find("colorButton").
                    GetComponentInChildren<UnityEngine.UI.Text>().text
                    = prefix + i;
                break;
            case 2:
                i = 3;
                GameObject.Find("colorButton").
                    GetComponentInChildren<UnityEngine.UI.Text>().text
                    = prefix + i;
                break;
            case 3:
                i = 0;
                GameObject.Find("colorButton").
                    GetComponentInChildren<UnityEngine.UI.Text>().text
                    = prefix + i;
                break;
            default:
                i = 0;
                break;
        }

        Color gameColor = getColor(i);
        UnityEngine.PlayerPrefs.SetInt("colorPreset", i);
        GameObject obj = GameObject.Find("colorButton");
        UnityEngine.UI.ColorBlock blk;
        blk = obj.GetComponentInChildren<UnityEngine.UI.Button>().colors;
        blk.normalColor = gameColor;
        blk.highlightedColor = gameColor;
        obj.GetComponentInChildren<UnityEngine.UI.Button>().colors = blk;
    }

    private Color getColor(int i)
    {
        Color gameColor = Color.black;
        switch (i)
        {
            case 0:
                gameColor = Color.black;
                break;
            case 1:
                gameColor = new Color(42f / 255f, 0, 96f / 255f);
                break;
            case 2:
                gameColor = new Color(0, 20f / 255f, 85f / 255f);
                break;
            case 3:
                gameColor = new Color(2f / 255f, 69f / 255f, 0);
                break;
        }
        return gameColor;
    }
}
