using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public int openingDirection;
    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (spawned == false)
        {
            switch (openingDirection)
            {
                case 1: // Spawn Bottom Room
                    CreateRoom(templates.bottomRooms);
                    break;

                case 2: // Spawn Top Room
                    CreateRoom(templates.topRooms);
                    break;

                case 3: // Spawn Left Room
                    CreateRoom(templates.leftRooms);
                    break;
                case 4: // Spawn Right Room
                    CreateRoom(templates.rightRooms);
                    break;

            }
            spawned = true;
        }
    }

    void CreateRoom(GameObject[] rooms)
    {
        rand = Random.Range(0, rooms.Length);
        Instantiate(rooms[rand], transform.position, Quaternion.identity);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawn>().spawned == false && spawned == false)
            {
                // spawn walls blocking off any openings
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
    
}
