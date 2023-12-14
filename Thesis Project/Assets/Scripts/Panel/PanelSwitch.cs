using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitch : PanelUIElements
{
    public List<float> switchPositionDegree;

    [Range(0, 50)] public float mouseSwitchThreshold;

    private int currentPosition = 0;
    private bool switchLock;

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        // if (currentPosition + 1 < switchPositionDegree.Count)
    }

    protected override void Start()
    {
        base.Start();

        switchLock = false;
    }

    protected override void Update()
    {
        base.Update();

        if (currentPosition >= switchPositionDegree.Count) currentPosition = switchPositionDegree.Count - 1;
        if (currentPosition < 0) currentPosition = 0;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,switchPositionDegree[currentPosition]));

        // print(Input.GetAxis("Mouse Y"));
    }

    protected override void MouseDragAction()
    {
        if (Input.GetAxis("Mouse Y") > mouseSwitchThreshold && !switchLock)
        {
            currentPosition += 1;
            switchLock = true;
            StartCoroutine(ResetSwitchLock());
        }

        if (Input.GetAxis("Mouse Y") < -mouseSwitchThreshold && !switchLock)
        {
            currentPosition -= 1;
            switchLock = true;
            StartCoroutine(ResetSwitchLock());
        }
    }

    IEnumerator ResetSwitchLock()
    {
        yield return new WaitForSeconds(0.25f);
        switchLock = false;
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
