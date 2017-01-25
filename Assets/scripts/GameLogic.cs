using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {
	public Button button;
	public Color testColor = Color.white;
	public Color gameColor = Color.black;
    public bool usePreferredColor = false;
	private bool visible = true;

    public bool gotCollectible1 = false;
    public bool gotCollectible2 = false;
    public bool gotCollectible3 = false;

    public Color getGameColor()
    {
        return gameColor;
    }

    // Use this for initialization
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
		button.onClick.AddListener (switchVisible);
        GameObject.Find("Main Camera").GetComponent<Camera>()
            .backgroundColor = camColor;
    }
	
	// Update is called once per frame
	void Update () {
		if(gotCollectible1 && gotCollectible2 && gotCollectible3)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
	}


	public void switchVisible() {
		GameObject[] wallList = GameObject.FindGameObjectsWithTag ("Wall");

		foreach (GameObject wall in wallList) {
			SpriteRenderer thisWall = wall.GetComponent<SpriteRenderer> ();
			if (!visible) {
				thisWall.color = testColor;
			} else {
				thisWall.color = gameColor;
			}
		}

        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("BasicEnemy");
        setColor(enemyList);


        enemyList = GameObject.FindGameObjectsWithTag("HunterEnemy");
        setColor(enemyList);

        visible = !visible;
	}

    void setColor(GameObject[] enemyList)
    {
        foreach (GameObject enemy in enemyList)
        {
            SpriteRenderer thisEnemy = enemy.GetComponent<SpriteRenderer>();
            if (!visible)
            {
                thisEnemy.color = testColor;
                enemy.GetComponent<BasicEnemyController>().regularColor = testColor;
            }
            else {
                thisEnemy.color = gameColor;
                enemy.GetComponent<BasicEnemyController>().regularColor = gameColor;
            }
        }
    }

}
