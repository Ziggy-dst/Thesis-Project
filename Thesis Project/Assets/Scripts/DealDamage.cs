using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public string targetTag;
    public float damage;

    public bool disableAfterDealDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
                gameObject.SetActive(false);
            }
        }
    }
}
