using UnityEngine;
using NodeCanvas.Framework;
using DG.Tweening;
using NodeCanvas.Tasks.Actions;


/// <summary>
/// TODO: after interrupting the charging -> destroy / disable bullet has been shot
/// TODO: maybe leave player node after disabled ?
/// </summary>
public class RangedAttack : ActionTask
{
    public BBParameter<GameObject> attackTarget;
    // the possible deviation from the target
    public float targetRadiusOffset;

    public BBParameter<GameObject> bulletPrefab;
    private GameObject fill;

    private GameObject bulletInstance;

    public float chargeTime = 2f;

    public float bulletSpeed = 10f;

    protected override void OnExecute()
    {
        InitializeWeapon();
        Charge();
    }

    private void InitializeWeapon()
    {
        // instantiate bullet (bar)
        bulletInstance = Object.Instantiate(bulletPrefab.value, agent.transform.Find("Attack Origin"));
        fill = bulletInstance.transform.GetChild(0).gameObject;
    }

    /// <summary>
    /// Charge x secs before shooting
    /// </summary>
    private void Charge()
    {
        // fill the charge bar
        fill.transform.DOScaleY(1, chargeTime)
            .OnComplete(() => { Shoot(); });
    }


    private void Shoot()
    {
        Vector2 targetPos =
            new Vector2(attackTarget.value.transform.position.x, attackTarget.value.transform.position.y);
        // target after random offset
        Vector2 realTarget = Random.insideUnitCircle * targetRadiusOffset + targetPos;

        Vector2 bulletPos = bulletInstance.transform.position;
        float distance = Vector2.Distance(realTarget, bulletPos);

        // Debug.Log(bulletPos);
        // Debug.Log(realTarget);
        // rotate bullet to the target
        Vector2 direction = realTarget - bulletPos;
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // bulletInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        bulletInstance.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        // shoot the bullet
        bulletInstance.transform.DOMove(realTarget, distance / bulletSpeed);
        EndAction(true);
    }
}