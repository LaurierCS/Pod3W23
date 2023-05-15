using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private int maxHealth; //The player's maximum health
    [SerializeField] private float damageDelay; //how many seconds between taking damage

    private int health;
    private float lastHit = 0; //When the player last took damage


    void Start()
    {
        Spawn();
    }

    public void DoDamage(int damage, GameObject attacker = null)
    {
        if (CanTakeDamage())
        {
            SetHealth(GetHealth() - damage); //do the damage
            if(GetHealth() <= 0)
            {
                Die();
            }
        }
    }

    void SetHealth(int hp)
    {
        health = hp;
    }
    int GetHealth()
    {
        return health;
    }

    public void Heal(int amt)
    {
        SetHealth(GetHealth() + amt);
    }

    bool CanTakeDamage()
    {
        if (Time.time > (lastHit + damageDelay)){
            return true;
        }
        return false;
    }

    void Die()
    {
        //Remove player object?
        //Spawn "respawn card" entity for other players to pick up
        //???
    }

    void Spawn()
    {
        SetHealth(maxHealth);
        //Other things we want to do:
        //Set position as specified
        //Give a set of weapons

    }
}
