using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager PanelManagerInstance;
    public bool isAdjusting = false;

    void Awake()
    {
        if (PanelManagerInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            PanelManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
