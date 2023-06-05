using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private int maxHealth = 5; //The player's maximum health
    [SerializeField] private float damageDelay = 1f; //how many seconds between taking damage
    [SerializeField] private float moveSpeed = 12f; //default move speed
    [SerializeField] private float moveSpeedADSFactor = 2f; //speed reduction for ADSing -> moveSpeed = moveSpeed / moveSpeedADSFactor

    private int health;
    private float lastHit = 0; //When the player last took damage


    void Start()
    {
    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }

    public void setMoveSpeed(float amt)
    {
        moveSpeed = amt;
    }

    public float getADSFactor()
    {
        return moveSpeedADSFactor;
    }

    public void setADSFactor(float amt)
    {
        moveSpeedADSFactor = amt;
    }

    public void DoDamage(int damage, GameObject attacker = null)
    {
        if (CanTakeDamage())
        {
            SetHealth(GetHealth() - damage); //do the damage
            lastHit = Time.time;
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
    public int GetHealth()
    {
        return health;
    }

    bool CanHeal()
    {
        return GetHealth() < maxHealth;
    }

    //Returns whether or not we were able to heal
    public bool Heal(int amt)
    {
        if (CanHeal())
        {
            SetHealth(GetHealth() + amt);
            if(GetHealth() > maxHealth) //oopsies too much health
            {
                SetHealth(maxHealth); //in case we go over
            }
            return true;
        }
        return false;
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
