using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BullNode : MonoBehaviour
{
    public static List< BullNode > nodes = new List< BullNode >();
    public void AddNode() {
        nodes.Add( this );
    }
    public void RemoveNode() {
        nodes.Remove( this );
    }
    public abstract void OnTriggerEnter( Collider other );
}
