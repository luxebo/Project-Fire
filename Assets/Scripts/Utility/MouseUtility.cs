using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseUtility {

    // Performs a raycast from the main camera to the specified plane.
    // If ray intersects plane, hit is set to true and function returns intersection point on the plane.
    // Otherwise, hit is set to false and function returns (0,0,0).
	public static Vector3 MouseWorldPoint(Plane plane, out bool hit)
    {
        Ray cameraMouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        float hitDist = 0f;
        bool intersect;
        intersect = plane.Raycast(cameraMouseRay, out hitDist);
        if (intersect)
        {
            hit = true;
            return cameraMouseRay.GetPoint(hitDist);
        }
        else
        {
            hit = false;
            return Vector3.zero;
        }
        
    }

}
