using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// When activated, changes color of object this component is attached to.
// Optionally, will change color back when deactivated.
public class ColorChangeHandler : TriggerHandler {
    [SerializeField]
    Material nextMat;
    [SerializeField]
    bool resetColor;
    Renderer objRenderer;
	// Use this for initialization
	protected override void Start () {
        objRenderer = GetComponent<Renderer>();
        if(objRenderer == null)
        {
            throw new MissingComponentException("ColorChangeHandler must be attached to an object with a renderer.");
        }
	}

    public override void activate()
    {
        Material temp = objRenderer.material;
        objRenderer.material = nextMat;
        if(resetColor)
            nextMat = temp;
    }

    public override void deactivate()
    {
        if (resetColor)
        {
            Material temp = objRenderer.material;
            objRenderer.material = nextMat;
            nextMat = temp;
        }
    }

}
