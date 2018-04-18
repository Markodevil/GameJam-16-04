// using, etc
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------
// Node object. Inheriting from MonoBehaviour. The chain piece of the Conveyorbelt class
//--------------------------------------------------------------------------------------
public class Node : MonoBehaviour
{
    // PUBLIC VALUES //
    //--------------------------------------------------------------------------------------
    // public array of gameobjects for the chunk prefabs
    public GameObject[] m_agBlueprints;

    // public bool for when to switch tiles
    public bool m_bSwitchTiles;

    // public bool for when to reset nodes
    public bool m_bResetNodes;

    // public int for the object pool size for chunks
    public int m_nPoolSize;

    // public hidden int for the chain position the node is
    [HideInInspector]
    public int m_nChainPosition;
    //--------------------------------------------------------------------------------------

    // PRIVATE VALUES //
    //--------------------------------------------------------------------------------------
    // private list of gameobjects for chunks
    private List<GameObject> m_agChunks;
    //--------------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------------
    // initialization.
    //--------------------------------------------------------------------------------------
    void Awake()
    {
        // set default bool values
        m_bSwitchTiles = true;
        m_bResetNodes = false;

        // fill the list with empty objects
        m_agChunks = new List<GameObject>();

        // loop through each chunk in the pool
        for (int i = 0; i < m_nPoolSize; ++i)
        {
            // Instantiate a random blueprint
            GameObject tmp = Instantiate(m_agBlueprints[Random.Range(0, m_agBlueprints.Length)]);

            // set active to false
            tmp.SetActive(false);

            // set the parenting, postion and add to object pool
            tmp.transform.SetParent(gameObject.transform);
            tmp.transform.position = gameObject.transform.position;
            m_agChunks.Add(tmp);
        }
	}

    //--------------------------------------------------------------------------------------
    // Update: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    void Update()
    {            
        // if switch tiles is true
        if (m_bSwitchTiles)
        {
            // loop through each chunk in the pool
            for (int i = 0; i < m_nPoolSize; ++i)
            {
                // if the chunk is active
                if (m_agChunks[i].activeInHierarchy)
                {
                    // set active to false
                    m_agChunks[i].SetActive(false);
                }
            }

            // select a random chunk from the pool and set active
            m_agChunks[Random.Range(0, m_nPoolSize)].SetActive(true);

            // set the switch tile back to false
            m_bSwitchTiles = false;
        }
        // if reset node is true
        if (m_bResetNodes)
        {
            // bring back to the end of the conveyorbelt chain
            transform.position += new Vector3(0, 0, ConveyorBelt.m_sfStaticLength * ConveyorBelt.m_sfNodeArraySize);

            // set reset node back to false
            m_bResetNodes = false;
        }

        // move the node
        Move();
    }

    //--------------------------------------------------------------------------------------
    // OnTriggerExit: OnTriggerExit is called when the Collider "other" exits the trigger.
    //
    // Param:
    //      other: the colliding object of type Collider
    //--------------------------------------------------------------------------------------
    private void OnTriggerExit(Collider other)
    {
        // if the colliding object is the resetter
        if (other.tag == "Resetter")
        {
            // reset nodes and switch the tile
            m_bSwitchTiles = true;
            m_bResetNodes = true;
        }
    }

    //--------------------------------------------------------------------------------------
    // Move: Function for moving the node across the conveyorbelt
    //--------------------------------------------------------------------------------------
    public void Move()
    {
        // move the nodes on the conveyorbelt
        transform.position -= new Vector3(0, 0, ConveyorBelt.m_sfStaticSpeed) * Time.deltaTime;
    }
}
