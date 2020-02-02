using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullScript : isoMetricMovement
{
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.name.ToLower() != "terrain") 
        { 
            BullStop();
        
            // We should get the Bull animation controller and trigger the impact and daze animations.

            switch (collision.gameObject.name.ToLower()) {
                case "cube.001":
                case "wallshelf2_right":
                    ChangeRotation("left_90");
                    Debug.Log("Bull hit RightWall");
                    break;
                case "cube.002":
                    ChangeRotation("right_135");
                    Debug.Log("Bull hit BackWall");
                    break;
                case "cube.003":
                    ChangeRotation("right_135");
                    Debug.Log("Bull hit Register Table"); 
                    break;
                case "wallshelf2_left":
                    ChangeRotation("right_45");
                    break;
            }
        }
    }
}
