using UnityEngine;
using NodeCanvas.Framework;
 
public class RangedAttack : ActionTask
{
    public GameObject bulletPrefab;
    private GameObject fill;

    protected override string OnInit()
    {
        // fill = bulletPrefab.GetComponentInChildren<>()
        return base.OnInit();
    }

    protected override void OnExecute()
    {
        Debug.Log("My agent is " + agent.name);
        EndAction(true);
    }
}