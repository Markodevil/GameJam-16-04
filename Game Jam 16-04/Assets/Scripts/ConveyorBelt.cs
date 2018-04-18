// using, etc
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------
// ConveyorBelt object. Inheriting from MonoBehaviour. This class moves the nodes that 
// make up the map to give the feeling of movement of the player
//--------------------------------------------------------------------------------------
public class ConveyorBelt : MonoBehaviour
{
    // PUBLIC VALUES //
    //--------------------------------------------------------------------------------------
    // public array of gamobjects for nodes in the conveyorbelt.
    public GameObject[] m_agNodes;

    // public array of gameobjects for node chunks.
    public GameObject[] m_agChunks;

    // public float for the conveyorbelt speed.
    public float m_fSpeed;

    // public float for a nodes length.
    public float m_fNodeLength;

    // static float for conveyorbelt length.
    static public float m_sfStaticLength;

    // static float for the conveyorbelt speed.
    static public float m_sfStaticSpeed;

    // static float for the amount of nodes.
    static public float m_sfNodeArraySize;

    // static bool for gameover state 
    static public bool m_sbGameEnd;
    //--------------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------------
    // initialization.
    //--------------------------------------------------------------------------------------
    void Awake()
    {
        // set inital values of conveyorbelt
        m_sbGameEnd = false;
        SetSpeed(m_fSpeed);

        // set static values
        m_sfStaticLength = m_fNodeLength;
        m_sfNodeArraySize = m_agNodes.Length - 1;
        
        // loop through all the nodes in the conveyorbelt
        for (int i = 0; i < m_agNodes.Length; ++i)
        {
            // set the starting postion of the node
            m_agNodes[i].transform.position = new Vector3(0, 0, m_fNodeLength * i);

            // set the chain postion of the node
            m_agNodes[i].GetComponent<Node>().m_nChainPosition = i;

            // Instantiate the node
            Instantiate(m_agNodes[i]);
        }
	}

    //--------------------------------------------------------------------------------------
    // Update: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    void Update()
    {
        // if game over is true
        if (m_sbGameEnd)
        {
            // set conveyorbelt speed to 0
            SetSpeed(0.0f);
        }
	}

    //--------------------------------------------------------------------------------------
    // SetSpeed: Set the static speed value of the conveyor belt.
    //
    // Param:
    //      fSpeed: float value for the speed to be set
    //--------------------------------------------------------------------------------------
    void SetSpeed(float fSpeed)
    {
        // set the static value to the speed param
        m_sfStaticSpeed = fSpeed;
    }
}
