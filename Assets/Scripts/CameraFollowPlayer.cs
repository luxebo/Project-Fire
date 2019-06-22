using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    GameObject player;
    bool followPlayer = true;
    // Use this for initialization
    //z and x
    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
        }
        else
        {
            if (Input.GetKeyDown("l"))
            {
                followPlayer = true;
            }
        }
    }

    public void setFollowPlayer(bool val)
    {
        followPlayer = val;
    }

    void camFollowPlayer()
    {
        Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        player.transform.position = newPos;
    }
}
