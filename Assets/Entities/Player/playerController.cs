using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    
    public Rigidbody2D rb;
    public Weapon weapon;

    public float moveSpeed = 12f;
    public float moveSpeedADSFactor = 2f;

    Vector2 moveDirection;
    Vector2 mousePosition;
    public bool ads = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButton(0))
        {
            if (weapon.CanFire())
            {
                weapon.Fire(ads);
            }
        }

        if (Input.GetMouseButton(1))
        {
            ads = true;
        } else
        {
            ads = false;
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    }

    private void FixedUpdate()
    {
        float calcMoveSpeed = moveSpeed;
        if (ads) //reduce movement when ads
        {
            calcMoveSpeed = moveSpeed / moveSpeedADSFactor;
        }
        rb.velocity = new Vector2(moveDirection.x * calcMoveSpeed, moveDirection.y * calcMoveSpeed); //prob should use delta time here?
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
}
