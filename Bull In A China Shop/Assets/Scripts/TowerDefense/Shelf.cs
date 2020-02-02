using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : BullNode
{
    bool isBroken = false;
    void Start() {
        AddNode();
    }
    private void Break() {
        isBroken = true;
    }
    public override void OnTriggerEnter( Collider other )
    {
        Bull bull = other.gameObject.GetComponent< Bull >();
        if( bull )
        {
            Break();
            bull.ChooseNextNode();
            RemoveNode();
        }
    }
}
