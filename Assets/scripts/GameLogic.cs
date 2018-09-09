using UnityEngine;
using System.Collections;
using UnityEngine.UI;


// USAGE: game logic; handles setup, initial and end states
public class GameLogic : MonoBehaviour {
	public Button visibilityToggle;
	public Color testColor = Color.white;
	public Color gameColor = Color.black;
    public bool usePreferredColor = false;
	private bool visible = true;

    // objectives
    public bool gotCollectible1 = false;
    public bool gotCollectible2 = false;
    public bool gotCollectible3 = false;


    // USAGE: game initialization + user settings
    void Start ()
    {
        Color camColor = new Color(gameColor.r / 2.05f, gameColor.g / 2.05f, gameColor.b / 2.05f);

        if (usePreferredColor)
        {
            int i = UnityEngine.PlayerPrefs.GetInt("colorPreset");
            switch (i)
            {
                case 0:
                    gameColor = Color.black;
                    camColor = Color.black;
                    break;
                case 1:
                    gameColor = new Color(42f / 255f, 0, 96f / 255f);
                    camColor = new Color(19f / 255f, 0, 45f / 255f);
                    break;
                case 2:
                    gameColor = new Color(0, 20f / 255f, 85f / 255f);
                    camColor = new Color(0, 10f / 255f, 40f / 255f);
                    break;
                case 3:
                    gameColor = new Color(2f / 255f, 69f / 255f, 0);
                    camColor = new Color(3.5f / 255f, 34.5f / 255f, 0);
                    break;
            }
        }
        
        switchVisible ();
		visibilityToggle.onClick.AddListener (switchVisible);

        GameObject.Find("Main Camera").GetComponent<Camera>()
            .backgroundColor = camColor;
    }
	
	// USAGE: checks for end state
	void Update () {
		if(gotCollectible1 && gotCollectible2 && gotCollectible3)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
	}


    // USAGE: switch between "invisible" mode (unhide walls, enemies) and "regular" mode (hidden walls, enemies)
	public void switchVisible() {

        Color mainColor;
        if(visible){
            mainColor = gameColor;
        }else{
            mainColor = testColor;
        }

        GameObject[] spriteList = GameObject.FindGameObjectsWithTag ("Wall");
        switchSpriteColor(spriteList, mainColor, "Wall");

        spriteList = GameObject.FindGameObjectsWithTag("BasicEnemy");
        switchSpriteColor(spriteList, mainColor, "BasicEnemy");

        spriteList = GameObject.FindGameObjectsWithTag("HunterEnemy");
        switchSpriteColor(spriteList, mainColor, "HunterEnemy");

        visible = !visible; 
	}

    // USAGE: change list of sprites to specified color
    void switchSpriteColor(GameObject[] spriteList, Color c, string tag){

        foreach(GameObject obj in spriteList){
            SpriteRenderer thisobj = obj.GetComponent<SpriteRenderer>();
            thisobj.color = c;

            if(tag == "BasicEnemy" || tag == "HunterEnemy"){
                obj.GetComponent<BasicEnemyController>().regularColor = c;
            }
        }
    }

    public Color getGameColor()
    {
        return gameColor;
    }
}
