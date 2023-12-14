using System.Collections;
using System.Collections.Generic;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public static TerrainManager TerrainManagerInstance;

    public BehaviourTreeOwner behaviourTreeOwner;

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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentTerrain = MapTerrain.Normal;
            behaviourTreeOwner.blackboard.SetVariableValue("isOnIce", false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentTerrain = MapTerrain.Ice;
            behaviourTreeOwner.blackboard.SetVariableValue("isOnIce", true);
        }
    }
}
