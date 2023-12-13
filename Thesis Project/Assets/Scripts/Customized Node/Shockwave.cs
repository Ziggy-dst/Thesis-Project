using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Framework;
using UnityEngine;

public class Shockwave : ActionTask
{
    public BBParameter<GameObject> circlePrefab;

    public float damage = 10f;

    public int circleCount = 4;
    public float generateInterval = 1f;

    protected override void OnExecute()
    {
        base.OnExecute();
    }
}
