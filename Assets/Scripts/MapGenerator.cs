using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public GameObject room;
    private Bounds blah;
	// Use this for initialization
	void Start () {
        Bounds room_bbox = room.GetComponent<Renderer>().bounds;
        GameObject newRoom = Instantiate(room);
        newRoom.transform.position = room.transform.position + new Vector3(0,0, room_bbox.size.z);
        print(room_bbox.center);
        print(room_bbox.size);
        print(newRoom.transform.position);
        blah = room_bbox;
	}
	
	// Update is called once per frame
	void Update () {
    }
}
