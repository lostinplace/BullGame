using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bull : MonoBehaviour
{
    protected BullNode currentTarget;

    public enum ClockDirection
    {
        CLOCKWISE = 0, 
        COUNTER_CLOCKWISE = 1, 
        //Cheeky way of saying "unassigned."//
        SUPER_POSITION = 2
    }
    protected delegate bool CheckAngleType( float queryAngle );
    protected delegate bool DesiredAngleQuality( float leftAngle, float rightAngle );
    /// <summary>
    /// Checks if an angle is "better" than another angle, basically
    /// 0: The angle is more "clockwise"/"counter clockwise"
    /// 1: The angle is "closer" on the XY plane to the angle the bull 
    ///         is currently facing/or angle we currently have selected.
    /// Priority is in that order.
    /// Example: 
    /// 11 o'clock is at a very close angle to 12 o'clock, however, it is in 
    /// the counter clockwise direction, if we wanted to see
    /// </summary>
    /// <param name="currentAngle">The angle the bull is currently 
    /// facing or the angle that is currently selected we would like to 
    /// see if the query angle is "better" than.</param>
    /// <param name="queryAngle">The angle we want to see if it is closer to 
    /// the current angle or direction the bull is currently facing.</param>
    /// <param name="angleQuery">A method to disern the "type" of an angle</param>
    /// <param name="angleQuality">An inequality</param>
    /// <returns></returns>
    protected float RetrieveBetterAngle( float currentAngle, float queryAngle, 
            CheckAngleType angleQuery, DesiredAngleQuality angleQuality)
    {
        return ( angleQuery( currentAngle ) &&
                angleQuery( queryAngle ) && 
                queryAngle < currentAngle ) ? 0.0f : 
                        !angleQuery( currentAngle ) && angleQuery( queryAngle ) ? 
                                queryAngle : queryAngle < currentAngle ? queryAngle : currentAngle;
    }
    /// <summary>
    /// Finds the next node the bull should run towards.
    /// </summary>
    /// <param name="direction">The angular direction on the XY plane that 
    /// the bull will try to find the closest node to in terms of angle.</param>
    /// <returns>null if there are no more nodes (time to exit the shop), 
    /// the node it is targeting otherwise.</returns>
    public BullNode ChooseNextNode( ClockDirection direction = ClockDirection.SUPER_POSITION )
    {
        if( BullNode.nodes.Count <= 0 )
            return null;
        float currentAngle = float.MaxValue;
        float angleWithRespectTooBull = float.MaxValue;
        //Save on memory.//
        Vector2 currentNodePosition = new Vector2( 0.0f, 0.0f );
        Vector2 bullPosition = Utility.ToXYPlane( transform.position ).normalized;
        if( direction == ClockDirection.SUPER_POSITION )
            direction = ( ClockDirection ) Random.Range( 0, 1 );
        foreach( BullNode currentNode in BullNode.nodes )
        {
            Utility.ToXYPlane( currentNode.transform.position, out currentNodePosition );
            /*It is not nessisary to divide the magnitude of the dot product of 
            two normals to achive the angle, because the magnitude of a normal is 1.*/
            angleWithRespectTooBull = Mathf.Acos( Vector2.Dot( bullPosition, currentNodePosition ) );
            //Find the smaller angle in the clockwise or counter clockwise direction.//
            if( direction == ClockDirection.CLOCKWISE ) {
                currentAngle = RetrieveBetterAngle( currentAngle, angleWithRespectTooBull, 
                        Utility.IsClockwiseAngle, Utility.LessThan );
            }
            else {
                currentAngle = RetrieveBetterAngle( currentAngle, angleWithRespectTooBull,
                        Utility.IsCounterClockwiseAngle, Utility.GreaterThan );
            }
            if( currentAngle == angleWithRespectTooBull )
                currentTarget = currentNode;
        }
        return currentTarget;
    }
}
