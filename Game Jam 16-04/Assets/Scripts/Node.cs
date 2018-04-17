using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject[] m_agBlueprints;

    public bool m_bSwitchTiles;
    public bool m_bResetNodes;

    public int m_nPoolSize;

    [HideInInspector]
    public int m_nChainPosition;

    private List<GameObject> m_agChunks;

    // Use this for initialization
    void Awake()
    {
        m_bSwitchTiles = true;
        m_bResetNodes = false;
        m_agChunks = new List<GameObject>();

        for (int i = 0; i < m_nPoolSize; ++i)
        {
            GameObject tmp = Instantiate(m_agBlueprints[Random.Range(0, m_agBlueprints.Length)]);
            tmp.SetActive(false);
            tmp.transform.SetParent(gameObject.transform);
            tmp.transform.position = gameObject.transform.position;
            m_agChunks.Add(tmp);
        }
	}
	
	// Update is called once per frame
	void Update()
    {
        transform.position -= new Vector3(0, 0, ConveyorBelt.m_fStaticSpeed) * Time.deltaTime;
        
            if (m_bSwitchTiles)
            {
                for (int i = 0; i < m_nPoolSize; ++i)
                {
                    if (m_agChunks[i].activeInHierarchy)
                    {
                        m_agChunks[i].SetActive(false);
                    }
                }
                m_agChunks[Random.Range(0, m_nPoolSize)].SetActive(true);

                m_bSwitchTiles = false;
            }
            if (m_bResetNodes)
            {
            transform.position = new Vector3(0, 0, ConveyorBelt.m_fStaticLength * ConveyorBelt.m_nNodeArraySize);
            m_bResetNodes = false;
            }        

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Resetter")
        {
            m_bSwitchTiles = true;
            m_bResetNodes = true;
        }
    }
}
