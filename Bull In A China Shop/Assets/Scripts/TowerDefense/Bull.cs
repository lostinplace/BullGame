using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bull : MonoBehaviour
{
    public float initialSpeed = .05f;
    public float currentSpeed;
    public bool chooseStartingNode = true;
    public BullNode currentTarget;
    public enum ClockDirection
    {
        CLOCKWISE = 0, 
        COUNTER_CLOCKWISE = 1, 
        //Cheeky way of saying "unassigned."//
        SUPER_POSITION = 2
    }
    protected delegate bool CheckAngleType( float queryAngle );
    protected delegate bool DesiredAngleQuality( float leftAngle, float rightAngle );

    private void Awake() {
        currentSpeed = initialSpeed;
    }

    void Update()
    {
        if( chooseStartingNode == true ) {
            ChooseNextNode();
            chooseStartingNode = false;
        }
        if( BullNode.nodes.Contains( currentTarget ) == true )
        {
            transform.position = Utility.XZLerpTo( 
                    transform.position, 
                    currentTarget.transform.position, currentSpeed );
        }
    }

    
    /// <summary>
    /// Note: Please substitute "cross - product" for "angle."
    /// Checks if an angle is "better" than another angle, basically
    /// 0: The angle is more "clockwise"/"counter clockwise"
    /// 1: The angle is "closer" on the XY plane to the angle the bull 
    ///         is currently facing/or angle we currently have selected.
    /// Priority is in that order.
    /// Example: 
    /// 11 o'clock is at a very close angle to 12 o'clock, however, it is in 
    /// the counter clockwise direction, if we wanted an angle in the clockwise 
    /// direction that was closer to 12 o'clock 2 o'clock may be better, but 
    /// 1 o'clock would be even better, if we didnt find an angle in the clockwise 
    /// direction 7 o'clock would be better than 11 o'clock.
    /// </summary>
    /// <param name="currentAngle">The angle the bull is currently 
    /// facing or the angle that is currently selected we would like to 
    /// see if the query angle is "better" than.</param>
    /// <param name="queryAngle">The angle we want to see if it is closer to 
    /// the current angle or direction the bull is currently facing.</param>
    /// <param name="angleQuery">A method to disern the "type" of an angle</param>
    /// <returns></returns>
    protected float RetrieveBetterAngle( float currentAngle, float queryAngle, 
            CheckAngleType angleQuery )
    {
        return ( angleQuery( currentAngle ) &&
                angleQuery( queryAngle ) && 
                Mathf.Abs( queryAngle ) < Mathf.Abs( currentAngle ) ) ? queryAngle : 
                        !angleQuery( currentAngle ) && angleQuery( queryAngle ) ? 
                                queryAngle : Mathf.Abs( queryAngle ) < 
                                Mathf.Abs( currentAngle ) ? queryAngle : currentAngle;
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
        float angleWithRespectTooBull = float.MaxValue / 20.0f;
        float currentAngle = float.MaxValue / 20.0f;
        Debug.Log( currentAngle );
        //Save on memory.//
        Vector3 currentNodePosition = new Vector3( 0.0f, 0.0f, 0.0f );
        Vector3 bullPositionNormal = Utility.XZToXYPlane3D( transform.position ).normalized;
        if( direction == ClockDirection.SUPER_POSITION )
            direction = ( ClockDirection ) Random.Range( 0, 1 );
        if( direction == ClockDirection.COUNTER_CLOCKWISE )
            angleWithRespectTooBull = float.MinValue;
        Debug.Log( direction );
        int i = 0;
        foreach( BullNode currentNode in BullNode.nodes )
        {
            Utility.XZToXYPlane( currentNode.transform.position, out currentNodePosition );
            angleWithRespectTooBull = Vector3.Cross( bullPositionNormal, currentNodePosition ).z;
            Debug.Log( "Unmodified Current Angle@ " + i + ": " + currentAngle );
            Debug.Log( "Angle " + i + ": " + angleWithRespectTooBull );
            //Find the smaller angle in the clockwise or counter clockwise direction.//
            if( direction == ClockDirection.CLOCKWISE ) {
                currentAngle = RetrieveBetterAngle( currentAngle, angleWithRespectTooBull, 
                        Utility.IsClockwiseAngle );
            }
            else {
                currentAngle = RetrieveBetterAngle( currentAngle, angleWithRespectTooBull,
                        Utility.IsCounterClockwiseAngle );
            }
            Debug.Log( "currentAngle@ " + i + ": " + currentAngle );
            if( currentAngle == angleWithRespectTooBull )
                currentTarget = currentNode;
        }
        return currentTarget;
    }
}
