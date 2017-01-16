using UnityEngine;
using System.Collections;
using System;

public struct Pickup
{
    public string type;
    public int value;
}

public class Item : MonoBehaviour {

    public Pickup action;
    public bool destroy;
    
	// Update is called once per frame
	void Update ()
    {
	    if (destroy)
	    {
            Destroy(gameObject);
	    }
	}

    public Pickup Collect()
    {
        destroy = true;
        return action;
    }
}
