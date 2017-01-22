using UnityEngine;
using System.Collections;

public class CollectibleScript : MonoBehaviour {
    public int collectibleNumber;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            switch(collectibleNumber)
            {
                case 1:
                    GameObject.Find("GameLogic").GetComponent<GameLogic>().gotCollectible1 = true;
                    Destroy(this.gameObject);
                    break;
                case 2:
                    GameObject.Find("GameLogic").GetComponent<GameLogic>().gotCollectible2 = true;
                    Destroy(this.gameObject);
                    break;
                case 3:
                    GameObject.Find("GameLogic").GetComponent<GameLogic>().gotCollectible3 = true;
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}
