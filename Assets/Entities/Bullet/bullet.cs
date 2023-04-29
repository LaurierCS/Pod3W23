using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float damage;
    public float bulletlife = 3f;
    public GameObject hitEffectPrefab;
    public Quaternion rotation;
    public Rigidbody2D rb;
    public Collider2D cd;

    public List<string> hitList;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldCollide(collision.gameObject))
        {
            RemoveBullet();
        }
        else
        {
            Physics2D.IgnoreCollision(collision.collider, cd);
        }
    }

    private bool ShouldCollide(GameObject go)
    {
        string tag = go.tag;
        if (hitList.Contains(tag))
        {
            return true;
        }
        return false;
    }

    private void Start()
    {
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        Invoke("RemoveBullet", bulletlife);
    }

    private void RemoveBullet()
    {
        GameObject ps = Instantiate(hitEffectPrefab, gameObject.GetComponent<Rigidbody2D>().position, Quaternion.identity);
        ps.transform.rotation = rotation;
        Destroy(gameObject);
    }

}
