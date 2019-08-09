using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class TriggerHandler : MonoBehaviour {
    protected virtual void Awake() { }
    protected virtual void Start() { }

    abstract public void activate();

    public virtual void deactivate() { }
}
