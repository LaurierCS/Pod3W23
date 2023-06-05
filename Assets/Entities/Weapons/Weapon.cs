using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;
    public List<string> hitList = new List<string>();

    protected string bulletPrefabPath = "bullet";
    protected string soundPath = "/Sounds/WeaponSounds/m1-garand-rifle-80192";
    protected string muzzleFlashPath = "muzzleflash";
    protected float fireForce = 30f;
    protected float fireDelay = 0.3f; //delay in seconds between shots
    protected float damage = 1f;
    protected float shakeMagnitude = 2f;
    protected float shakeRoughness = 2f;
    public float spread = 0.1f;
    public int magsize = 30; //shots before reloading
    public float reloadtime = 2f; //how long in seconds it takes to reload
    public int numShots = 1; //how many shots per trigger pull, > 1 for shotguns
    public float bulletLife = 3f; //how long the bullet should last before despawning
    public float spreadADSFactor = 10f; //by how much should we reduce spread when ADS?
    public int ammoPool = 500; //how much ammo the player can relaod from, -1 is infinite



    protected AudioSource fireSound;
    private AudioClip fireSoundClip;

    private float lastShot = 0f;
    private bool reloading = false;
    public int magcount; //shots currently in mag

    public void Start()
    {
        fireSound = gameObject.AddComponent<AudioSource>();
        magcount = magsize;
        Debug.Log(fireSound);
        //fireSound.clip = Resources.Load<AudioClip>(soundPath);
        fireSoundClip = Resources.Load<AudioClip>(soundPath);
    }
    public virtual void Fire(bool ads)
    {
        for (int i = 0; i < numShots; i++)
        {
            float spreadCalc = spread;
            if (ads)
            {
                spreadCalc = spread / spreadADSFactor;
            }
            GameObject bullet = Instantiate(Resources.Load<GameObject>(bulletPrefabPath), firePoint.position, firePoint.rotation);
            Vector3 spreadVec = new Vector3(Random.Range(-spreadCalc, spreadCalc), Random.Range(-spreadCalc, spreadCalc), 0);
            bullet.GetComponent<Rigidbody2D>().AddForce((firePoint.up + spreadVec) * fireForce, ForceMode2D.Impulse);
            bullet fireBullet = bullet.GetComponent<bullet>();
            fireBullet.damage = damage;
            fireBullet.bulletlife = bulletLife;
            fireBullet.rotation = firePoint.rotation; //workaround
        }
        GameObject muzzleFlash = Instantiate(Resources.Load<GameObject>(muzzleFlashPath), firePoint.position, Quaternion.identity);
        //AudioClip fireSoundClip = fireSound.clip;
        fireSound.PlayOneShot(fireSoundClip);
        CameraShaker.Instance.ShakeOnce(shakeMagnitude, shakeRoughness, 0.1f, 1f);
        magcount--;

    }

    public bool CanFire()
    {
        if(Time.time > (lastShot + fireDelay))
        {
            if (magcount > 0)
            {
                lastShot = Time.time;
                return true;
            } else
            {
                if (CanReload())
                {
                    Reload();
                }
            }
        }

        return false;
    }

    public bool CanReload()
    {
        if (reloading)
        {
            return false;
        }
        if(magcount < magsize && (ammoPool > 0 || ammoPool == -1))
        {
            return true;
        }
        return false;
    }

    public void Reload()
    {
        reloading = true;
        Invoke("DoReload", reloadtime);
    }

    protected void DoReload()
    { 
        if(ammoPool == -1) //infinite ammo case
        {
            magcount = magsize;
        } else
        {
            int ammoNeeded = magsize - magcount; //how many rounds do we need to reload
            int ammoResupply = ammoNeeded;
            if (ammoPool >= ammoNeeded)
            {
                ammoPool -= ammoNeeded;
            } else
            {
                ammoResupply = ammoPool;
                ammoPool = 0;
            }
            magcount += ammoResupply;
        }

        reloading = false;
    }
}
