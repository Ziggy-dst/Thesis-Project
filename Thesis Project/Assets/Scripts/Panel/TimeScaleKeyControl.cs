using System.Collections;
using System.Collections.Generic;
using Chronos;
using UnityEngine;

public class TimeScaleKeyControl : MonoBehaviour
{
    private GlobalClock _globalClock;
    // Start is called before the first frame update
    void Start()
    {
        _globalClock = FindObjectOfType<GlobalClock>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _globalClock.localTimeScale = -1;
            return;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _globalClock.localTimeScale = 5;
            return;
        }

        _globalClock.localTimeScale = 1;
    }
}
