using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : BullNode
{
    public bool isBroken = false;
    void Start() {
        AddNode();
    }
    private void Break() {
        isBroken = true;
        RemoveNode();
    }
    public override void OnTriggerEnter( Collider other )
    {
        Bull bull = other.gameObject.GetComponent< Bull >();
        if( bull ) {
            Break();
            bull.ChooseNextNode();
        }
    }
}
