using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isoMetricMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20f;
    //Change in inspector to adjust move speed
	Vector3 forward, right;
    // Keeps track of our relative forward and right vectors

    private IEnumerator bullMovement_coroutine;

    // This is the bull initial direction after entering the shop.
    public float horizontalDir, verticalDir;

    // Start is called before the first frame update
    void Start()
    {
        forward = Camera.main.transform.forward; // Set forward to equal the camera's forward vector
        forward.y = 0; // make sure y is 0
        forward = Vector3.Normalize(forward); // make sure the length of vector is set to a max of 1.0
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; // set the right-facing vector to be facing right relative to the camera's forward vector   

		Vector3 levelSize = GameObject.Find("Terrain").GetComponent<Terrain>().terrainData.size;
		float playingField = (levelSize.x * levelSize.z);
		moveSpeed = (playingField)/60;
        Debug.Log("Calling BullInit");
        BullInit();
    }

	void Update()
	{

	}

    // Whatever starts the level should call 
    public void BullInit() {
        horizontalDir = -0.3f; 
        verticalDir = 0.3f;

        bullMovement_coroutine = MoveBull();
        StartCoroutine(bullMovement_coroutine);

        for (int i = 0; i< 50; i++)
        {
            Move();
        }
       
    }

    public void BullStop() {
        StopCoroutine(bullMovement_coroutine);
    }

    IEnumerator MoveBull()
    {
        for (; ; )
        {
            Move();
            yield return new WaitForSeconds(.1f);
        }
    }

    /// <summary>
    /// This will change the default speed of the object. 
    /// </summary>
    /// <param name="changeSpeedBy">A positive number will incress the spead of the object. a negative number will decrease the speed of the object.</param>
    public void ChangeSpeed(int changeSpeedBy) {
		moveSpeed = moveSpeed + changeSpeedBy;
	}

	public void ChangeStamina(int changeStaminaBy) { }

	public void ChangeRotation(int rotation) { }


	void Move()
	{
        Vector3 direction = new Vector3(horizontalDir, 0, verticalDir); // setup a direction Vector based on keyboard input. GetAxis returns a value between -1.0 and 1.0. If the A key is pressed, GetAxis(HorizontalKey) will return -1.0. If D is pressed, it will return 1.0
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * horizontalDir; // Our right movement is based on the right vector, movement speed, and our GetAxis command. We multiply by Time.deltaTime to make the movement smooth.
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * verticalDir; // Up movement uses the forward vector, movement speed, and the vertical axis inputs.
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement); // This creates our new direction. By combining our right and forward movements and normalizing them, we create a new vector that points in the appropriate direction with a length no greater than 1.0
        transform.forward = heading; // Sets forward direction of our game object to whatever direction we're moving in
        transform.position += rightMovement; // move our transform's position right/left
        transform.position += upMovement; // Move our transform's position up/down
    }
}
