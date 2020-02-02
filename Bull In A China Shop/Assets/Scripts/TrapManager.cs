using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrapManager : MonoBehaviour
{
    // One instance of this class
    public static TrapManager Instance;
    
    // Puts private fields into inspector for easier tweaking through gameplay
    [Header("Gum")]
    [Tooltip("Bull's speed relative to maximum when affected by a gum trap.")]
    [SerializeField] private float gumSpeed = 0.75f;
    
    [Header("Glue")]
    [Tooltip("Bull's speed relative to maximum when affected by a glue trap.")]
    [SerializeField] private float glueSpeed = 0.6f;

    [Header("Banana Peel")]
    [Tooltip("How long the bull is stalled when affected by a banana peel trap.")]
    [SerializeField] private float bananaStall = 5f;
    [Tooltip("Angle that bull is rotated by in either direction when affected by a banana peel trap.")]
    [SerializeField] private string bananaAngle = "left_90";
    [Tooltip("Bull's exhaustion relative to maximum when affected by a banana peel trap.")] 
    [SerializeField] private float bananaExhaustion = 1.1f;

    [Header("Cape")]
    [Tooltip("Angle that bull is rotated by in either direction when affected by a cape trap.")]
    [SerializeField] private string capeAngle = "left_90";
    [Tooltip("Bull's speed relative to maximum when affected by a cape trap.")]
    [SerializeField] private float capeSpeed = 1.5f;
    [Tooltip("Bull's exhaustion relative to maximum when a bull is when affected by a cape trap.")]
    [SerializeField] private float capeExhaustion = 1.2f;

    [Header("Decoy")]
    [Tooltip("Bull's speed relative to maximum when affected by decoy.")]
    [SerializeField] private float decoySpeed = 0.3f;
    [Tooltip("Bull's exhaustion relative to maximum when affected by a decoy.")] 
    [SerializeField] private float decoyExhaustion = 1.5f;
    
    // Enum for actual trap types
    protected enum Trap
    {
        Gum,
        Glue,
        BananaPeel,
        Cape,
        Decoy
    };
    
    // Reference to bull
    public GameObject bull;
    
    //static Action<Trap> TakeEffect = TrapEffect;

    //private Dictionary<Trap, Action<Trap>> TrapsToActions = new Dictionary<Trap, Action<Trap>>();

    // Start is called before the first frame update
    void Start()
    {
        bull = GameObject.Find("Bull");
        // If there is no Instance, makes this the Instance
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        
        // If an instance already exists, destroy this
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    // Actual trap's effect based on Trap enum type
    protected void TrapEffect(Trap trap)
    {
        switch (trap)
        {
            case Trap.Gum:
                // Slow down bull by [gumSpeed] modifier
                // (for [gumTime] amount of time?)
                bull.GetComponent<BullScript>().ChangeSpeed((int) gumSpeed);
                break;
            case Trap.Glue:
                // Slow down bull by [glueSpeed] modifier
                // (for [glueTime] amount of time?)
                bull.GetComponent<BullScript>().ChangeSpeed((int) glueSpeed);
                break;
            case Trap.BananaPeel:
                // Stall the bull for [bananaStall] seconds,
                // redirect bull by [bananaAngle] in either direction,
                // and increase exhaustion by [bananaExhaustion] modifier
                // (for [bananaTime] amount of time?)
                //bull.GetComponent<BullScript>().Stall(bananaStall);
                bull.GetComponent<BullScript>().ChangeRotation((string) bananaAngle);
                bull.GetComponent<BullScript>().ChangeStamina((int)bananaExhaustion);
                break;
            case Trap.Cape:
                // Redirect bull by [capeAngle] in either direction,
                // and speed up bull by [capeSpeed] modifier
                // and increase exhaustion by [capeExhaustion] modifier 
                bull.GetComponent<BullScript>().ChangeRotation((string) capeAngle);
                bull.GetComponent<BullScript>().ChangeSpeed((int) capeSpeed);
                bull.GetComponent<BullScript>().ChangeStamina((int) capeExhaustion);
                break;
            case Trap.Decoy:
                // Slow down bull by [decoySpeed] modifier
                // and increase exhaustion by [decoyExhaustion] modifier
                bull.GetComponent<BullScript>().ChangeSpeed((int)decoySpeed);
                bull.GetComponent<BullScript>().ChangeStamina((int)decoyExhaustion);
                break;
            default:
                // Something went wrong?
                Debug.Log("This shouldn't happen?");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
