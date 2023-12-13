using System.Collections;
using System.Collections.Generic;
using Chronos;
using UnityEngine;

public class BulletParentOccurences : MonoBehaviour
{
    private Timeline timeline;
    // Start is called before the first frame update
    void Start()
    {
        timeline = GetComponent<Timeline>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBulletActiveOccurence()
    {
        timeline.Do(
            false,
            delegate
            {
                transform.GetChild(0).gameObject.SetActive(false);
            },
            delegate
            {
                transform.GetChild(0).gameObject.SetActive(true);
            });
    }
}
