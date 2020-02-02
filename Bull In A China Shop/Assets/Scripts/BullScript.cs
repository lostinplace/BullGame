using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class BullScript : MonoBehaviour
{

    private bool dazed;

    private bool seeking;

    private Quaternion newTargetAngle;
    
    void OnCollisionEnter(Collision collision)
    {

        var collidedWith = collision.gameObject;
        
        if (collidedWith.tag == "Floor") return;
        if (collidedWith.tag == "Wall")
        {
            
            BullStop();
        };
        if (collidedWith.tag == "Shelf") return;
        if (collidedWith.tag == "Trap") return;
    }
    
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float rotationSpeed = 100.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        Vector3 levelSize = GameObject.Find("Terrain").GetComponent<Terrain>().terrainData.size;
		float playingField = (levelSize.x * levelSize.z);
		moveSpeed = (playingField)/60;
    }

    void Update()
    {
        var delta = Time.deltaTime;
        if(dazed) return;
        if (seeking)
        {
            this.Seek(delta);
            return;
        }
        Move(Time.deltaTime);
        this.transform.rotation = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w);
    }

    public void BullStop()
    {
        
        var animator = GetComponent<Animator>();
        this.dazed = true;
        animator.SetBool("Collided", true);
        animator.SetBool("Reset", false);
    }

    public void WakeBull()
    {
        this.dazed = false;
        this.seeking = true;
        var animator = GetComponent<Animator>();
        animator.SetBool("Collided", false);
        
    }

    public void Seek(float deltaTime)
    {
        this.transform.Rotate(Vector3.up, rotationSpeed*deltaTime);
        if (false)
        {
            var animator = GetComponent<Animator>();
            animator.SetBool("Reset", true);
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

	public void ChangeRotation(string turnDirection) { }


    void Move(float timeDelta)
	{
        transform.Translate(this.transform.forward*moveSpeed*timeDelta);
    }
}
