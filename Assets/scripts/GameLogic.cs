using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {
	public Button button;
	public Color testColor = Color.white;
	public Color gameColor = Color.black;
	private bool visible = true;

    public bool gotCollectible1 = false;
    public bool gotCollectible2 = false;
    public bool gotCollectible3 = false;

    // Use this for initialization
    void Start () {
		switchVisible ();
		button.onClick.AddListener (switchVisible);
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
