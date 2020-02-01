using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullScript : isoMetricMovement
{
    void OnCollisionEnter(Collision collision) {
        Debug.Log("A collision happened.");
        if (collision.gameObject.name.ToLower().Contains("wall"))
        {
            Debug.Log("A collided with a wall.");
            Vector3 dir = collision.contacts[0].point - transform.position;

            dir = -dir.normalized;
            GetComponent<Rigidbody>().AddForce(dir * 20f);
            
        }
        else if (collision.gameObject.name == (""))
        {
           
        }
    }

}
