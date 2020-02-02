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
        DebugRay();
        if( chooseStartingNode == true ) {
            ChooseNextNode();
            chooseStartingNode = false;
        }
        if( BullNode.nodes.Contains( currentTarget ) == true )
        {
            if( Mathf.Abs( Vector3.Angle( transform.position, currentTarget.transform.position ) ) > float.Epsilon )
            {
                //Not enough time, it just looks!//
                transform.LookAt( Utility.XYToXZPlane( currentTarget.transform.position ) );
                /*Debug.Log( Utility.XZLerp( transform.rotation.eulerAngles,
                        currentTarget.transform.rotation.eulerAngles, .01f ) + "" );*/
                /*transform.rotation.SetLookRotation( 
                        Utility.XZLerp( transform.rotation.eulerAngles,
                        currentTarget.transform.rotation.eulerAngles, 1.0f ) );*/
            }
            else
            {
                transform.position = Utility.XZLerp(
                        transform.position,
                        currentTarget.transform.position, 5.0f );
            }
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
        Debug.Log( "angleQuery( currentAngle: " + currentAngle + " ): " + angleQuery( currentAngle ) + " && " +
                "angleQuery( queryAngle: " + queryAngle + " ): " + angleQuery( queryAngle ) + " && " +
                "Mathf.Abs( queryAngle: " + queryAngle + " ) < Mathf.Abs( currentAngle: " + currentAngle + " ): " + 
                Mathf.Abs( queryAngle ) + " < " + Mathf.Abs( currentAngle ) );
        Debug.Log( "\t!angleQuery( currentAngle ): " + !angleQuery( currentAngle ) + 
                " && " + " angleQuery( queryAngle: " + queryAngle + " ): " + angleQuery( queryAngle ) );
        Debug.Log( "\t\tMathf.Abs( queryAngle: " + queryAngle + " ) < Mathf.Abs( currentAngle: " + currentAngle + " ): " +
                Mathf.Abs( queryAngle ) + " < " + Mathf.Abs( currentAngle ) );
        return ( angleQuery( currentAngle ) &&
                angleQuery( queryAngle ) && 
                Mathf.Abs( queryAngle ) < Mathf.Abs( currentAngle ) ) ? queryAngle : 
                        !angleQuery( currentAngle ) && angleQuery( queryAngle ) ? 
                                queryAngle : Mathf.Abs( queryAngle ) < 
                                Mathf.Abs( currentAngle ) ? queryAngle : currentAngle;
    }

    List< Vector3 > dbgPositions = new List< Vector3 >();
    Vector3 lastBullPositionNormal;
    List<Vector3> nodePositionNormals = new List<Vector3>();
    List<Vector3> crossProduccts = new List<Vector3>();
    void DebugRay()
    {
        for( int i = 0; i < dbgPositions.Count; ++i )
        {
            Debug.DrawRay( dbgPositions[ i ], Utility.XYToXZPlane( lastBullPositionNormal ), Color.blue );
            Debug.DrawRay( dbgPositions[ i ], Utility.XYToXZPlane( nodePositionNormals[ i ] ), Color.green );
            Debug.DrawRay( dbgPositions[ i ], crossProduccts[ i ], Color.red );
        }
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
        float angleWithRespectTooBull = float.MaxValue;
        float currentAngle = float.MaxValue;
        Debug.Log( currentAngle );
        //Save on memory.//
        Vector3 currentNodePosition = new Vector3( 0.0f, 0.0f, 0.0f );
        Vector3 bullPositionNormal = Utility.XZToXYPlane3D( transform.position ).normalized;
        if( direction == ClockDirection.SUPER_POSITION )
            direction = ( ClockDirection ) Random.Range( 0, 2 );
        if( direction == ClockDirection.COUNTER_CLOCKWISE )
            angleWithRespectTooBull = float.MinValue;
        Debug.Log( direction );
        int i = 0;

        lastBullPositionNormal = bullPositionNormal;
        crossProduccts.Clear();
        nodePositionNormals.Clear();
        dbgPositions.Clear();


        foreach( BullNode currentNode in BullNode.nodes )
        {
            Utility.XZToXYPlane( currentNode.transform.position, out currentNodePosition );
            angleWithRespectTooBull = Vector3.Cross( bullPositionNormal, currentNodePosition.normalized ).z;
            dbgPositions.Add( new Vector3( currentNodePosition.x, 5.0f, currentNodePosition.y ) );
            nodePositionNormals.Add( currentNodePosition.normalized );
            crossProduccts.Add( new Vector3( 0.0f, angleWithRespectTooBull, 0.0f ) );
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
            ++i;
        }
        return currentTarget;
    }
}
