using UnityEngine;
using System.Collections;


/*
 * used as an abstraction of various particles (e.g.- missiles, flames)
 * reference: see   scripts/TopDown/IMPL/PARTICLE
 * 			: see	scripts/TopDown/MECHANICS/ShipComponents/ShipDefinitions.cs 
 */ 
public abstract class Particle : MonoBehaviour
{
    public int lifetime;
    private float lifetimeCount;
    private Vector2 velKeep;
    public bool active = true;
    public int damage;
    public NocturneDefinitions.Faction faction;

    
	/*
	 * allows the particle to freeze; records its current velocity
	 * to resume upon unpausing
	 */
    public void pause()
    {
        active = false;

        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        velKeep = rbody.velocity;
        rbody.velocity = Vector2.zero;
        if(GetComponent<Animator>() != null)
            GetComponent<Animator>().enabled = false;
    }

	/*
	 * allows the particule to resume its previous activity
	 */ 
    public void unpause()
    {
        active = true;

        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = velKeep;
        if(GetComponent<Animator>() != null)
            GetComponent<Animator>().StartPlayback();
    }


	/*
	 * sets the ship faction
	 */ 
    public void setFaction(NocturneDefinitions.Faction faction)
    {
        this.faction = faction;
    }

    /*
    * when the particle collides with a ship, decreases the health of
    * the ship under appropriate circumstances (see below)
    * 
    * NOTE: a ship will be damaged only if the particle that hits it 
    * 		 is sent from a ship in the opposing faction!!!
    */
    public void OnTriggerEnter2D(Collider2D col)
    {
        /*
        if ((col.CompareTag("Enemy") &&
                (faction == ShipDefinitions.Faction.Player ||
                faction == ShipDefinitions.Faction.PlayerAffil)) ||
             (col.CompareTag("Player") ||
              col.CompareTag("PlayerAffil")) &&
                faction == ShipDefinitions.Faction.Enemy)
        {
            col.gameObject.GetComponent
                <ShipIntf>().isHit(damage);
            Destroy(gameObject);
        }
        */
    }

    /*
    * Destroy the particle if its duration has expired
    * Otherwise, decrement lifetime
    */
    protected void handleLifetime()
    {
        lifetimeCount += Time.deltaTime;
        if(lifetimeCount >= 0.02)
        {
            lifetimeCount = 0;
            lifetime--;
        }
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            lifetime--;
        }
    }
}
