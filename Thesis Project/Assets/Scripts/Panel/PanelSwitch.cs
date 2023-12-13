using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitch : PanelUIElements
{
    public List<float> switchPositionDegree;

    private int currentPosition = 0;

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        // if (currentPosition + 1 < switchPositionDegree.Count)
    }
    // void Update()
    // {
    //     ChangeKnobPosition();
    // }
    // private void ChangeKnobPosition()
    // {
    //     if (Input.GetKeyDown(KeyCode.Q))
    //     {
    //         TerrainManager.TerrainManagerInstance.currentTerrain = TerrainManager.MapTerrain.Normal;
    //         print(TerrainManager.TerrainManagerInstance.currentTerrain);
    //     }
    //
    //     if (Input.GetKeyDown(KeyCode.E))
    //     {
    //         TerrainManager.TerrainManagerInstance.currentTerrain = TerrainManager.MapTerrain.Ice;
    //         print(TerrainManager.TerrainManagerInstance.currentTerrain);
    //     }
    // }
}
