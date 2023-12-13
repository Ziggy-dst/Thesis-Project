using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public static TerrainManager TerrainManagerInstance;

    public enum MapTerrain
    {
        Normal,
        Ice
    }

    public MapTerrain currentTerrain = MapTerrain.Normal;


    void Awake()
    {
        if (TerrainManagerInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            TerrainManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
