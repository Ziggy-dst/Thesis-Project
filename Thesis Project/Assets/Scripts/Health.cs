using System.Collections;
using System.Collections.Generic;
using Chronos;
using DG.Tweening;
using Unity.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Basic Setup")]
    public Timeline timeline;
    public SpriteRenderer spriteRenderer;
    
    [Header("Health")]
    public float initialHealthPoint;
    public float healthPoint;
    
    
    [Header("Battle")]
    public bool immuneToDamage;
    public float getHitCD;

    [Header("Respawn")]
    public bool respawnOnDeath;
    public float respawnCD;

    private GameObject spawnPoint;

    private bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        // spawnPoint = GameObject.FindWithTag("Spawn Point");
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
        
        //Dead Function
        

        if (respawnOnDeath) StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return timeline.WaitForSeconds(respawnCD);
        healthPoint = initialHealthPoint;
        immuneToDamage = false;
        // transform.position = spawnPoint.transform.position;
    }

    // void Respawn()
    // {
    //     healthPoint = initialHealthPoint;
    //     immuneToDamage = false;
    //     // transform.position = spawnPoint.transform.position;
    // }

    public void TakeDamage(float dmg)
    {
        if (!immuneToDamage)
        {
            healthPoint -= dmg;
            immuneToDamage = true;
            spriteRenderer.DOColor(Color.red, getHitCD).SetEase(Ease.Flash, 16, 1);
            
            StartCoroutine(ResetHitCoolDown());
        }
    }

    IEnumerator ResetHitCoolDown()
    {
        yield return timeline.WaitForSeconds(getHitCD);
        immuneToDamage = false;
    }
}
