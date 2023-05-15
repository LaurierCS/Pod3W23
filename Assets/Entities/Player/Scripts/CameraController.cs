using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camHolder;
    public Camera cam;
    public GameObject player;

    public static float defaultMouseCamInf = 0.28f;
    public static float adsCamInfluence = 0.3f;
    public static float zoom = -4f;

    public float fov;
    public float mouseCamInfluence = defaultMouseCamInf;
    Vector2 moveDirection;
    Vector2 mousePosition;

    private playerController pc;

    // Start is called before the first frame update
    void Start()
    {
        fov = cam.orthographicSize;
        pc = player.GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.ads)
        {
            cam.orthographicSize = fov - zoom;
            mouseCamInfluence = adsCamInfluence;
        }
        else
        {
            cam.orthographicSize = fov;
            mouseCamInfluence = defaultMouseCamInf;
        }
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        float posx = gameObject.transform.position.x;
        float posy = gameObject.transform.position.y;
        float posz = camHolder.transform.position.z;
        Vector3 pos = new Vector3(posx, posy, posz);
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        Vector2 aimDirection = mousePosition - rb.position;
        camHolder.transform.position = pos + new Vector3(aimDirection.x, aimDirection.y, 0) * mouseCamInfluence;
    }
}
