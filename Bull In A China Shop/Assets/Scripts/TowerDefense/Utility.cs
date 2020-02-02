using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    public static Vector2 XZToXYPlane( Vector3 toConvert ) {
        return new Vector2( toConvert.x, toConvert.z );
    }
    public static void XZToXYPlane( Vector3 toConvert, out Vector2 converted ) {
        converted.x = toConvert.x;
        converted.y = toConvert.z;
    }
    public static Vector3 XZToXYPlane3D( Vector3 toConvert ) {
        return new Vector3( toConvert.x, toConvert.z, 0.0f );
    }
    public static void XZToXYPlane( Vector3 toConvert, out Vector3 converted )
    {
        converted.z = 0.0f;
        converted.x = toConvert.x;
        converted.y = toConvert.z;
    }
    public static Vector3 XYToXZPlane( Vector2 toConvert ) {
        return new Vector3( toConvert.x, 0.0f, toConvert.y );
    }
    public static Vector3 XYZToXZPlane( Vector3 toConvert ) {
        return new Vector3( toConvert.x, 0.0f, toConvert.z );
    }
    public static void XYZToXZPlane( Vector3 toConvert, out Vector3 converted ) {
        converted.x = toConvert.x;
        converted.y = 0.0f;
        converted.z = toConvert.z;
    }
    public static Vector3 XZLerpTo( Vector3 toMove, Vector3 to, float speed = 1.0f ) {
        return XZLerp( toMove, to, speed );
    }
    public static Vector3 XZLerp( Vector3 toMove, Vector3 along, float speed = 1.0f )
    {
        float y = toMove.y;
        toMove = Vector3.Lerp( toMove, along, Time.deltaTime );
        toMove.y = y;
        return toMove;
    }
    public static bool IsClockwiseAngle( float queryAngle ) {
        return queryAngle >= 0.0f;
    }
    //Literally calls !IsClockwiseAngle.//
    public static bool IsCounterClockwiseAngle( float queryAngle ) {
        return !IsClockwiseAngle( queryAngle );
    }
}
