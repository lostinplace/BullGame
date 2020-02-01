using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullScript : isoMetricMovement
{
    void OnCollisionEnter(Collision collision) {
        switch (collision.gameObject.name.ToLower()) {
            case "backwall":
                Debug.Log("Bull hit BackWall");
                break;
            case "rightwall":
                Debug.Log("Bull hit RightWall");
                break;
            case "wallshelf1":
                Debug.Log("Bull hit WallShelf1");
                break;
            case "tallesttable":
                Debug.Log("Bull hit TallestTable");
                break;
        }
    }

}
