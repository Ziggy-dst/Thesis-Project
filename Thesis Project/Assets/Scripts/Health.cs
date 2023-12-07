using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float initialHealthPoint;
    
    public float healthPoint;

    public bool immuneToDamage;

    public bool respawnOnDeath;

    public float respawnCD;

    private GameObject spawnPoint;

    private bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.FindWithTag("Spawn Point");
        healthPoint = initialHealthPoint;
        immuneToDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoint <= 0)
        {
            if (!isDead)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isDead = true;
        healthPoint = 0;
        immuneToDamage = true;
        if(respawnOnDeath) Invoke("Respawn", respawnCD);
    }

    void Respawn()
    {
        healthPoint = initialHealthPoint;
        immuneToDamage = false;
        transform.position = spawnPoint.transform.position;
    }
}
