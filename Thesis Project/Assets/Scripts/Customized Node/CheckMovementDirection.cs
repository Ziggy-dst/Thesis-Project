using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

[Description("Check the object normalized movement direction, return true if it is moving, otherwise false")]
public class CheckMovementDirection : ActionTask
{
    private BBParameter<Vector2> moveDirection;
    private float moveDistance;

    private Vector2 initialPosition;
    private Vector2 lastPosition;

    public BBParameter<bool> isSliding = false;

    private Blackboard currentBlackboard;

    protected override string OnInit()
    {
        currentBlackboard = agent.GetComponent<Blackboard>();
        return base.OnInit();
    }

    protected override void OnExecute()
    {
        lastPosition = agent.transform.position;
        StartCoroutine(DelayedCheck());
    }

    private IEnumerator DelayedCheck()
    {
        yield return new WaitForSeconds(0.1f);
        CheckMoveDirection();
    }

    protected override void OnUpdate()
    {
        // blackboard.SetVariableValue("isSliding", true);
        // Debug.Log("blackboard " + blackboard.GetVariableValue<bool>("isSliding"));
        // CheckMoveDirection();
    }

    private void CheckMoveDirection()
    {
        // StartCoroutine(DelayedCheck());
        Vector2 currentPosition = agent.transform.position;
        // Debug.Log("current pos: " + currentPosition);
        // Debug.Log("last pos: " + lastPosition);
        // Debug.Log("is unequal: " + (currentPosition != lastPosition));
        // Debug.Log(blackboard.GetVariableValue<bool>("isSliding"));
        if (currentPosition != lastPosition && !blackboard.GetVariableValue<bool>("isSliding"))
        {
            Vector2 rawDir = currentPosition - initialPosition;
            Debug.Log("Raw Movement direction: " + rawDir);
            rawDir.Normalize();

            moveDirection = rawDir;
            Debug.Log("------------------Direction--------------------");
            // Debug.Log("isSliding: " + blackboard.SetVariableValue("isSliding", true));
            currentBlackboard.SetVariableValue("moveDirection", moveDirection.value);
            // blackboard.SetVariableValue("isSliding", true);

            // Debug.Log("isSliding: " + blackboard.SetVariableValue("isSliding", true));

            Debug.Log("Movement direction: " + moveDirection);
            // 确保只记录第一次移动的方向
             EndAction(true);
             return;
        }

        lastPosition = currentPosition;
        EndAction(false);
    }
}
