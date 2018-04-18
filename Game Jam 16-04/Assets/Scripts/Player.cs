// using, etc
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------
// Enum EJumpState. Used for the current state of jumping.
//--------------------------------------------------------------------------------------
public enum EJumpState
{
    EJUMPSTATE_GROUNDED,
    EJUMPSTATE_SINGLE,
    EJUMPSTATE_DOUBLE
}

//--------------------------------------------------------------------------------------
// Player object. Inheriting from MonoBehaviour. The main player class for player movement
//--------------------------------------------------------------------------------------
public class Player : MonoBehaviour
{
    // PUBLIC VALUES //
    //--------------------------------------------------------------------------------------
    // public int for jumping force
    public int m_nForceConst = 500;

    // public bool for jumping animation
    public bool m_bJumpAni = false;

    //--------------------------------------------------------------------------------------

    // PRIVATE VALUES //
    //--------------------------------------------------------------------------------------
    // private rigidbody
    private Rigidbody m_rbRigidBody;

    // private bool for if the player can jump
    private bool m_bJump;

    // private gameobject for the collider of the player
    private CapsuleCollider m_cPlayerCollider;

    // enum for the jumping state of the player
    private EJumpState m_eJumpState;
    //--------------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------------
    // initialization.
    //--------------------------------------------------------------------------------------
    void Awake()
    {
        // Get the rigidbody component
        m_rbRigidBody = GetComponent<Rigidbody>();

        // Get the capsule collider of the player
        m_cPlayerCollider = GetComponent<CapsuleCollider>();

        // make sure that the animator knows that it shouldn't play death animation
        GetComponent<Animator>().SetBool("Death", ConveyorBelt.m_sbGameEnd);
    }

    //--------------------------------------------------------------------------------------
    // Update: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    void Update()
    {
        // if the game hasnt ended
        if (!ConveyorBelt.m_sbGameEnd)
        {
            
            // if space bar is pressed and the player is grounded
            if (Input.GetMouseButtonDown(0) && IsGrounded())
            {
                // can jump bool is true
                m_bJump = true;
                m_bJumpAni = true;
                GetComponent<Animator>().SetBool("Jump", m_bJumpAni);
            }

            // if space bar is pressed and the player is grounded
            if (Input.GetMouseButtonDown(0) && m_eJumpState == EJumpState.EJUMPSTATE_SINGLE)
            {
                // can jump bool is true
                m_bJump = true;
                m_bJumpAni = true;
                // set jump state to double
                m_eJumpState = EJumpState.EJUMPSTATE_DOUBLE;
            }
        }

        // if jump animation is true
        if (m_bJumpAni)
        {
            // jump animation is false
            m_bJumpAni = false;

            // set animator bool
            GetComponent<Animator>().SetBool("Jump", m_bJumpAni);
        }
    }

    //--------------------------------------------------------------------------------------
    // FixedUpdate: This function is called every fixed framerate frame.
    //--------------------------------------------------------------------------------------
    void FixedUpdate()
    {
        // Can the player jump?
        if (m_bJump)
        {
            // player cant jump
            m_bJump = false;

            // set jump state to single jump if grounded
            if (m_eJumpState == EJumpState.EJUMPSTATE_GROUNDED)
                m_eJumpState = EJumpState.EJUMPSTATE_SINGLE;

            // Add force to the player to jump
            m_rbRigidBody.AddForce(0, m_nForceConst, 0, ForceMode.Impulse);
        }
    }

    //--------------------------------------------------------------------------------------
    // IsGrounded: Check if the player is on the ground.
    //
    // Return:
    //      bool: bool value for if the player is grounded or not.
    //--------------------------------------------------------------------------------------
    bool IsGrounded()
    {
        // Cast a ray down from the player at the ground
        Debug.Log("IsGrounded");
        Ray rRay = new Ray(transform.position - new Vector3(0, m_cPlayerCollider.height * 0.4f, 0), Vector3.down);
        RaycastHit rhHitInfo;

        // Set the layermask
        int nLayerMask = (LayerMask.GetMask("Ground"));

        // Is the ray colliding with the ground?
        if (Physics.Raycast(rRay, out rhHitInfo, 0.5f, nLayerMask))
        {
            // reset jump state
            m_eJumpState = EJumpState.EJUMPSTATE_GROUNDED;

            // Return true and debug log the collider name
            Debug.Log(rhHitInfo.collider.name);
            return true;
        }

        // Draw the ray cast and print ray information in the console
        Debug.DrawRay(rRay.origin, Vector3.down);
        Debug.Log(rRay.origin.ToString() + " " + rRay.direction.ToString());

        // return false if not grounded
        return false;
    }

    //--------------------------------------------------------------------------------------
    // OnTriggerEnter: OnTriggerEnter is called when the Collider "other" enters the trigger.
    //
    // Param:
    //      other: the colliding object of type Collider
    //--------------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        // if the collision is tag obstacle
        if (other.tag == "Obstacle")
        {
            // game over value is true
            ConveyorBelt.m_sbGameEnd = true;
            // play death animation
            GetComponent<Animator>().SetBool("Death", ConveyorBelt.m_sbGameEnd);
        }
    }
}