using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public GameObject[] m_agNodes;

    public GameObject[] m_agChunks;

    public float m_fSpeed;

    

    public float m_fNodeLength;

    static public float m_fStaticLength;

    static public float m_fStaticSpeed;

    static public float m_nNodeArraySize;

    static public bool m_bGameEnd;

    // Use this for initialization
    void Awake()
    {
        SetSpeed(m_fSpeed);
        m_fStaticLength = m_fNodeLength;
        m_nNodeArraySize = m_agNodes.Length - 1;
        

        for (int i = 0; i < m_agNodes.Length; ++i)
        {
            m_agNodes[i].transform.position = new Vector3(0, 0, m_fNodeLength * i);
            m_agNodes[i].GetComponent<Node>().m_nChainPosition = i;
            Instantiate(m_agNodes[i]);
            
            
        }
	}
	
	// Update is called once per frame
	void Update()
    {
       if(m_bGameEnd)
        {
            SetSpeed(0.0f);
        }
 
	}

    void SetSpeed(float fSpeed)
    {
        m_fStaticSpeed = fSpeed;
    }
}
