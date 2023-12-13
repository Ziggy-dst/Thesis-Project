using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knob : MonoBehaviour
{
    void Update()
    {
        ChangeKnobPosition();
    }

    private void ChangeKnobPosition()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TerrainManager.TerrainManagerInstance.currentTerrain = TerrainManager.MapTerrain.Normal;
            print(TerrainManager.TerrainManagerInstance.currentTerrain);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TerrainManager.TerrainManagerInstance.currentTerrain = TerrainManager.MapTerrain.Ice;
            print(TerrainManager.TerrainManagerInstance.currentTerrain);
        }
    }
}
