using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    public static Vector2 ToXYPlane( Vector3 toConvert ) {
        return new Vector2( toConvert.x, toConvert.y );
    }
    public static void ToXYPlane( Vector3 toConvert, out Vector2 converted ) {
        converted.x = toConvert.x;
        converted.y = toConvert.y;
    }
    public static bool IsClockwiseAngle( float queryAngle ) {
        return ( queryAngle >= 0.0f && queryAngle <= ( Mathf.PI / 2.0f ) );
    }
    //Literally calls !IsClockwiseAngle.//
    public static bool IsCounterClockwiseAngle( float queryAngle ) {
        return !IsClockwiseAngle( queryAngle );
    }
    //////////////////////////////////////
    //So they can be used in delegates. //
    //////////////////////////////////////
    public static bool GreaterThan( float left, float right ) {
        return left > right;
    }
    public static bool LessThan( float left, float right ) {
        return left < right;
    }
}
