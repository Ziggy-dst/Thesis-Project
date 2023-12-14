using System;
using System.Collections;
using System.Collections.Generic;
using Chronos;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public string targetTag;
    public float damage;

    public bool disableAfterDealDamage = false;
    public bool selfDisable = false;

    private Timeline timeline;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (selfDisable) StartCoroutine(SelfDisable());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(targetTag))
        {
            col.gameObject.GetComponent<Health>().TakeDamage(damage);

            if (disableAfterDealDamage)
            {
                GetComponentInParent<BulletParentOccurences>().SetBulletActiveOccurence();
            }
        }
    }

    IEnumerator SelfDisable()
    {
        timeline = GetComponentInParent<Timeline>();
        yield return timeline.WaitForSeconds(8);
        GetComponentInParent<BulletParentOccurences>().SetBulletActiveOccurence();
    }
}
