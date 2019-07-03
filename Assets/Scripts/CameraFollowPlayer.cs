using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float upLimit;
    public float downLimit;
    public float rightLimit;
    public float leftLimit;
    Vector3 cxy;
    bool followPlayer = true;
    float fov = 60.0f;
    // Use this for initialization
    //z and x
    void Start()
    {
        cxy = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer == true)
        {
            camFollowPlayer();
            if (Input.GetKeyDown("l"))
            {
                followPlayer = false;
            }
            camZoom();
        }
        else
        {
            camUnlocked();
            if (Input.GetKeyDown("l"))
            {
                followPlayer = true;
            }
            camZoom();
        }
    }

    void camFollowPlayer()
    {
        Vector3 newPos = player.position + cxy;
        transform.position = Vector3.Slerp(transform.position, newPos, 1.0f);
        transform.LookAt(player);
    }

    void camUnlocked()
    {
        float panSpeed = 100.0f;
        float border = 30.0f;
        Vector3 pos = transform.position;
        if (Input.mousePosition.x >= Screen.width - border)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= border)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y >= Screen.height - border)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= border)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        pos.x = Mathf.Clamp(pos.x, downLimit, upLimit);
        pos.z = Mathf.Clamp(pos.z, leftLimit, rightLimit);
        transform.position = pos;
    }

    void camZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        fov -= (scroll * 5);
        fov = Mathf.Clamp(fov, 40.0f, 100.0f);
        Camera.main.fieldOfView = fov;
    }
}