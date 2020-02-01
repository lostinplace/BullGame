using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehavior : TrapManager
{
    protected Trap type;
    
    // Start is called before the first frame update
    void Start()
    {
        // Evaluates which type of trap this is based on name
        if (gameObject.name.Contains("gum"))
        {
            type = Trap.Gum;
        }
        if (gameObject.name.Contains("glue"))
        {
            type = Trap.Glue;
        }
        if (gameObject.name.Contains("banana") || (gameObject.name.Contains("peel")))
        {
            type = Trap.BananaPeel;
        }
        if (gameObject.name.Contains("cape"))
        {
            type = Trap.Cape;
        }
        if (gameObject.name.Contains("decoy"))
        {
            type = Trap.Decoy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision other)
    {
        // If the bull collides with this object
        if (other.gameObject.name.Contains("bull"))
        {
            TrapEffect(type);
            Destroy(this);
        }
    }
}
